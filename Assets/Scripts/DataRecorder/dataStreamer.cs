using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dataStreamer : MonoBehaviour {

	// data access
	public Compass compas;
	public Gyroscope gyro;

	public Slider sldr;
	public InputField IField;
	public static int modeValue;
	public static List<string> modesInputs= new List<string> {"Audio", "Gyro", "Compass", "Accelero", "Tactile", /*"Movuino"*/};
	public static string modeLabel;
	public static string nameLabel;

	public static Vector3 data;


	// Use this for initialization
	void Start () {
		
		compas= Input.compass;
		compas.enabled=true;

		gyro=Input.gyro;
		gyro.enabled=true;

		//sldr.onValueChanged.AddListener(delegate {onSldrChange(); });

	}
	
	// Update is called once per frame
	void Update () {
		nameLabel= IField.text;
		modeValue = (int)sldr.value ;
		modeLabel= modesInputs[modeValue];

		data = dataStream(modeValue);

	}

	public Vector3 dataStream(int valeur){
		
		Vector3 dat=new Vector3();

		switch (valeur){

		case 4:
			dat= new Vector3(Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.z);//Input.touches[0].position;
			break;

		case 3:
			dat= gyro.gravity;
			break;

		case 2:
			dat=compas.rawVector;

			break;
		case 1:
			dat=gyro.rotationRate;

			break;
		case 0:
			dat=new Vector3(1000000*AudioVisualizer.spectrumData,1000000* AudioVisualizer.spectrumData,10000000*AudioVisualizer.spectrumData);
			break;
		}
		return dat;
	}


	public void ontextchanged(){
	nameLabel= IField.text;
	}

	public void onSldrChange(){

		nameLabel= IField.text;
		modeValue = (int)sldr.value ;
		modeLabel= modesInputs[modeValue];

		data = dataStream(modeValue);
	}
}
