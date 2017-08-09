using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class inputClass : MonoBehaviour {

	// data access

	public Compass compas;
	public Gyroscope gyro;

	//recordable parameters

	private string modeTxt;
	public InputField inputF;


	// Usable UI 
	//public Dropdown dd ;

	public Text dbgText;
	public Text dbg2;


	// recording sessions
	public int TimeStamp;
	public int recDuration=5;
	public bool recorder=false;
	//public static bool rec;
	private string inputText;
	public static string recPath;
	public string filePath;
	public static string nullMovementname;
	public Slider sldr;
	public int animTime;

	//private xmmDataContainer xmmCollection;
	//public static xmmDataContainer xd= new xmmDataContainer();//return to public static!!
	public static xmmDataContainer xd= new xmmDataContainer();//return to public static!!
	private static dataXmm dataXmm1;//return to public static!!

	// Use this for initialization
	void Start () {
		
		/*compas= Input.compass;
		compas.enabled=true;

		gyro=Input.gyro;
		gyro.enabled=true;*/

		recPath = Application.persistentDataPath+"/dataStore/"; //modifies also in labelsGestion
		Directory.CreateDirectory(recPath);
		nullMovementname = "NoN";
		animTime=3;
		//Adds a listener to the main slider and invokes a method when the value changes.
		//sldr.onValueChanged.AddListener(delegate {reinitializeRec(); });
		//sldr.onValueChanged.AddListener(delegate {onSldrChange(); });

		//inputF.onValueChanged.AddListener(delegate {ValueChangeCheckTxtInput();});

		//xd.dataXmms.Add
		dataXmm1=new dataXmm();
		//Debug.Log(recPath);
			
	}

	// Update is called once per frame
	void FixedUpdate () {
		
		if(Time.time > animTime){
		dbg2.text="";
		recordCanceler(recorder);
		
			if(recorder){
				recSessionExists(recPath+dataStreamer.nameLabel+".xml");
				recordTimeStamps(recorder);
			}
	
		}else{

			dbg2.text="wait a few secs";
			reinitializeRec();
		}

	}

	//checks if a file exists, if there are fake name labels in it, and if there a "doucble labels" records
	public void recSessionExists(string path){ 

		if(File.Exists(path)){	//rebuilds the whole sequence to add elements

			dbgText.text="Already exists, data will be added for \t"+ dataStreamer.modeLabel + "\t on \t" + dataStreamer.nameLabel + "\tLabel" ;
			xd.dataXmms.Clear();
			xd=xmmDataContainer.Load(path);	
		
			if(xd.dataXmms.Exists(x=> x.modeRecord == dataStreamer.modeLabel)){ 

				//checks if a label have false labels record names
				List<dataXmm> dtx = xd.dataXmms.FindAll(x=> x.label != dataStreamer.nameLabel);
				for(int i=0; i< dtx.Count;i++ ){
					xd.dataXmms.Remove(dtx[i]);
					Debug.Log(dtx[i].label);
				}
				dtx.Clear();

				dtx = xd.dataXmms.FindAll(x=> x.modeRecord== dataStreamer.modeLabel);//checks if a label have already been recorded with this mode reocord
				for(int i=0; i< dtx.Count;i++ ){
					xd.dataXmms.Remove(dtx[i]);


				}
				dtx.Clear();

				dtx = xd.dataXmms.FindAll(x=> x.modeRecord== dataStreamer.modeLabel);//checks if a label have already been recorded with this mode reocord
				for(int i=0; i< dtx.Count;i++ ){
					xd.dataXmms.Remove(dtx[i]);


				}
				dtx.Clear();

				dbg2.text= dataStreamer.modeLabel + "\t on \t" + dataStreamer.nameLabel + "\t will be replaced" ;

			} else{
				dbg2.text= dataStreamer.modeLabel + "\t on \t" + dataStreamer.nameLabel + "\t Label not recorded yet" ;

			}


		}

		else{

			dbgText.text= dataStreamer.nameLabel + " Label doesn't exist, it will be created for first time"  ;


		}

	}

	//record timer
	public void recordTimeStamps(bool rec){

		switch (rec)
		{

		case (true):
				
				if(timerFunction(TimeStamp) <0f){
					TimeStamp= recDuration + (int)Time.time;
					dataXmm1.rawData="";

				}
				
				if(timerFunction(TimeStamp)<0.15f){
					reinitializeRec();
					xd.dataXmms.Add(dataXmm1);
					xd.Save(recPath+dataXmm1.label+".xml");
					dbgText.text = "Saved \t" + xd.dataXmms.Count+ "\t elements" +"\t @ "+ dataXmm1.label+".xml" + "\t"+ "\t file";
				}

				if(timerFunction(TimeStamp) >=0f){
				int varC = (int)timerFunction(TimeStamp)+1;
				dataXmm1.dataRecap(dataStreamer.nameLabel, dataStreamer.modeLabel, dataStreamer.data);
				dbgText.text ="Label is : \t" + dataStreamer.nameLabel + "\t and Mode is : \t" + dataStreamer.modeLabel +"is being recorded for still " + varC +" seconds";

				}
			break;
		
		case (false):
			reinitializeRec();
			break;
			
	
		}

	}

	//non valid record session
	public void recordCanceler(bool rec)
	{
		if(rec)
		{
			if(dataStreamer.nameLabel=="" || dataStreamer.nameLabel==" " || dataStreamer.nameLabel==null || dataStreamer.nameLabel=="Respectez la casse des labels"){
				dbg2.text ="No Label specified, no possible recording"; //empty input field
				reinitializeRec();

			}
		}

	}

	public float timerFunction(int time){
		//if( time-TimeStamp >0f)
		return time-Time.time;
	}

	public void record(){
		
		recorder=!recorder;
	}

	//non gesture record
	public void NoNrecorder()
	{
		inputF.text=nullMovementname;
		recorder=true;

	}

	/*public List<string> pathSelectionner(string path, string fileName){

		if(!File.Exists(path+fileName+".xml")){
			File.Create(path+fileName+".xml");
			dbgText.text="File "+path+fileName+".xml" + "doesn't exist";
		}else{
			dbgText.text="File "+path+fileName+".xml" + "already exists";

		}

		return new List<string>{fileName+"", path+fileName+".xml"};
	}*/

	//reinitialize record
	private void reinitializeRec(){
		recorder=false;
		TimeStamp= /*recDuration +*/ (int)Time.time;
		//dbgText.text=" ";
		//dbg2.text="";
		//inputF.text= " Respecter La casse des Labels"; 
	}

	private void onSldrChange(){
		//dataXmm1.dataRecap(dataStreamer.nameLabel, dataStreamer.modeLabel,);
		recorder=false;
		TimeStamp= /*recDuration +*/ (int)Time.time;
	}


}
