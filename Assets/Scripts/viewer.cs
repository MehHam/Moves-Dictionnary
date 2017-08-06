using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class viewer : MonoBehaviour {

	public  Slider	sldr;
	public GameObject GOview;
	public static Vector3 posFixe;
	private Transform tran; 
	//public Text tx4;
	// Use this for initialization
	void Start () {
		sldr.value=4;
		sldr.onValueChanged.AddListener(delegate {objectsHide(); });
		posFixe= new Vector3(-2,-2f,12.67f);
		objectsMotion();
		this.transform.position=posFixe;

	}

	void Update(){

		objectsHide();
		objectsMotion();
		//tx4.text = dataStreamer.data+"";
	}


	// Update is called once per frame
	private void objectsHide(){
		Transform tr= GOview.transform;
		string s="";
		Transform t;

		foreach (Transform child in tr)
			child.gameObject.SetActive(false);

		switch(dataStreamer.modeValue){

		case 4:
			s="Tactile";

			break;

		case 3:
			s="CubeAccelero";
			break;

		case 2:
			s="CubeMagnet";// chooose the labels...positions varies as respect to the position
			break;

		case 1:
			s="SphereGyro";

			break;

		case 0:
			
			s="SpectrumObjects";
			//tr.Find("Microphone").gameObject.SetActive(true);
			//tr.Find("OptionsController").gameObject.SetActive(true);
			break;
		}
		tr.Find(s).gameObject.SetActive(true);
		t=tr.Find(s).gameObject.transform;
		t.position=posFixe;

		tran = t;

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
