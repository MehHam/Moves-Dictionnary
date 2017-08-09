using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class viewer : MonoBehaviour {

	public  Slider	sldr;
	public GameObject GOview;
	private GameObject go;
	public static Vector3 posFixe;
	private Transform tran; 
	private bool recValue;
	//public Text tx4;
	// Use this for initialization

	void Awake(){

		DontDestroyOnLoad(transform.gameObject);
	}


	void Start () {
		
		//objectsHide();


		go = GameObject.Find("Canvas");

		posFixe= new Vector3(-2,-2f,12.67f);
		objectsMotion();
		this.transform.position=posFixe;


		GOview.transform.GetChild((int)(sldr.value)).gameObject.SetActive(true);
		//Debug.Log(GOview.transform.GetChild((int)(sldr.value)).gameObject.name);

		tran = GOview.transform.GetChild((int)(sldr.value));

		sldr.onValueChanged.AddListener(delegate {objectsHide(); });


	}

	void Update(){
		tran = GOview.transform.GetChild((int)(sldr.value));
		//objectsHide();
		hiderGO();
		objectsMotion();
		//tx4.text = dataStreamer.data+"";
	}


	// Update is called once per frame
	private void objectsHide(){
		
		Transform tr= GOview.transform;
		Transform tr2 = go.transform ;

		string s="";
		Transform t;


		foreach( Transform child in tr){
			child.gameObject.SetActive(false);

		}

		switch((int)sldr.value){


		case 4:
			s="Tactile";
			tr2.FindChild("SettingsButton").gameObject.SetActive(false);

			break;

		case 3:
			s="CubeAccelero";
			tr2.FindChild("SettingsButton").gameObject.SetActive(false);
			break;

		case 2:
			s="CubeMagnet";// chooose the labels...positions varies as respect to the position
			tr2.FindChild("SettingsButton").gameObject.SetActive(false);
			break;

		case 1:
			s="SphereGyro";
			tr2.FindChild("SettingsButton").gameObject.SetActive(false);
			break;

		case 0:
			s="MicGO";

			tr2.FindChild("SettingsButton").gameObject.SetActive(true);
			//tr.Find(s).GetComponentInChildren<MicrophoneInput>().enabled = true;
			//tr.Find("Microphone").gameObject.SetActive(true);
			//tr.Find("OptionsController").gameObject.SetActive(true);
			break;
		}

		tr.Find(s).gameObject.SetActive(true);
		t=tr.Find(s).gameObject.transform;
		t.position=posFixe;

		tran = t;

	}

public void hiderGO(){

	if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name=="Record" ){
			
			if(tran.name == "Tactile"){
					foreach (Transform child in go.transform){

					if(child.name!="LabelRecord"){
						if(child.name!="TactileSurface")
							child.gameObject.SetActive(!GameObject.Find("rec2").GetComponent<inputClass>().recorder);
						else{
							child.gameObject.SetActive(GameObject.Find("rec2").GetComponent<inputClass>().recorder);
							child.Find("Timer").GetComponent<Text>().text = GameObject.Find("infotextLabel").GetComponent<Text>().text ;
						}	
					}
				}
				GameObject.Find("Canvas").transform.FindChild("SettingsButton").gameObject.SetActive(false);
			}
			
				if(tran.name == "MicGO"){
				
				}
	}else{
		


	}

}

	public void objectsMotion(){

		float coef=1f;

		switch(dataStreamer.modeValue){

		case(4):
			Vector3 datai = Input.mousePosition;//dataStreamer.data;
			datai.z=posFixe.z;
			tran.position = Camera.main.ScreenToWorldPoint(datai) ;
			break; 

		case(3):
			coef =0 ;
			tran.localPosition = Vector3.zero;
		
			break;

		case(2):
			coef=0;
			tran.localPosition = Vector3.zero;//coef*dataStreamer.data;
			break;

		case(1):
			tran.localPosition = Vector3.zero;
			coef = 10;
			Quaternion rott=tran.localRotation;
			rott.eulerAngles = coef*dataStreamer.data;
			break;

	}
	}


}
