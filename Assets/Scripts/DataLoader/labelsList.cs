using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
//using UnityEditor;

public class labelsList : MonoBehaviour {
	// Use this for initialization
	public List<string> labelsArrayString;
	public GameObject pref;
	private GameObject instantiatedGO;
	public Text dbg4;

	void Start () {
		labelsArrayString= new List<string>();
		dbg4.text="";
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log(labelsArrayString[labelsArrayString.Count-1]);

	}

	//finds available labes per mode 
	public /*List<string>*/void labelsListing () {
		//List<string> s=new List<string>();
		labelsArrayString.Clear();
		reinit ();
		DirectoryInfo dir = new DirectoryInfo(inputClass.recPath); //load from the global variabe path value
		FileInfo[] info = dir.GetFiles("*.*");
		string name;
		foreach (FileInfo f in info){
			name= f.Name.Split('.')[0];
			//Debug.Log(s);
			if(f.Name.Split('.')[0]!="00_Sequences" && xmmDataContainer.Load(inputClass.recPath+ name+".xml").dataXmms.Exists(x=> x.modeRecord == dataStreamer.modeLabel)){
				//dbg4.text+= name;
				labelsArrayString.Add(name);
				//Debug.Log();
			}
		}
		//return s;
	}

	public void dropBoxesLoading(){
		reinit();

		GameObject go = GameObject.Find("PanelContainer");

		foreach(string s in labelsArrayString){
			instantiatedGO = Instantiate (pref, transform.position, Quaternion.identity);
			instantiatedGO.transform.parent=go.transform;

			instantiatedGO.transform.localPosition=new Vector3(transform.parent.GetComponent<RectTransform>().rect.width/2 - (labelsArrayString.IndexOf(s) * instantiatedGO.transform.GetComponent<RectTransform>().rect.width/2), 0,0);//transform.parent.position;
			//intiantiatedGO.transform.local ; 

			instantiatedGO.transform.name= s;
			instantiatedGO.GetComponentInChildren<Text>().text=s;
		}
	}

	public void reinit (){
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("LabelDrop")){
			GameObject.Destroy(go);
		}

	}

}
