using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class nouveSequence : MonoBehaviour {

	public static LabelsSequence labS;
	public static labelsSequenceContainer labCont=new labelsSequenceContainer();
	public static string newLabelInSequence;
	public static string pathSequences;
	public Text dbgTxt;
	public Text dbg2;
	public GameObject pref;
	private GameObject instantiatedGO;




	void Start(){
		pathSequences=inputClass.recPath+"00_Sequences.xml";
		newLabelInSequence="";

		labS=new LabelsSequence("","","");

	}
	// Use this for initialization

	void Update(){
		labS.modeInput=dataStreamer.modeLabel;
		labS.labelsSequence=newLabelInSequence;

	}

	public void newSequenceCreate(){
		newLabelInSequence="";
		erazeSequence();
		dbg2.text="";

		if(!File.Exists(pathSequences)){
			dbg2.text="No sequences until here...File Will be created";
			File.Create(pathSequences);
			//labCont.Save(pathSequences);
		}else{
			labelsSequenceContainer labo=(labelsSequenceContainer)labelsSequenceContainer.Load(pathSequences);

			if(labo!=null){
				labCont=labo;
				dbgTxt.text="file includes " + labo.LabelsSequences.Count + " sequences!" ;


			}

		}
	}

	public void erazeSequence(){
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("seqElement")){
			GameObject.DestroyImmediate(go);
		}
	}

	public void sequencesSavior(){ //save a sequence as respect to its content checks all components and add it to previous file
		bool isPresent=false;

		foreach (LabelsSequence lseq in labCont.LabelsSequences){ //checks if the present sequence is already recorded

			if(labS.labelSequenceEqualiser(lseq))
				//Debug.Log("ici");
				isPresent=true;
			}

		if(!isPresent && labS.labelsSequence!=""){
			labS.nameOfSequence=labCont.LabelsSequences.Count+1+"";
			labCont.LabelsSequences.Add(labS);
			labCont.Save(pathSequences);
			dbgTxt.text= labCont.LabelsSequences.Count + " sequences saved!" ;
		
		}else{
			if(isPresent){
				dbg2.text = " Sequence is already included " ;
				erazeSequence();

			}
			if(labS.labelsSequence==""){
				dbg2.text = " Sequence is empty " ;

			}

			}
	}

	/*
	public void sequenceUpdater(string s){
	
		if(s!= null && s!= "" && s!=" ")
			labS.labelsSequence+=s;
			

	}*/
	/*
	public string[] DeserialStringArray(string s)
	{
		string[] labLoad = s.Split('|');

		//Vector3[] result = new Vector3[vectors.Length];
		for (int i = 0; i < labLoad.Length; i++)
		{
			Debug.Log(labLoad[i]);
		}

		return labLoad ;
	}
*/

}
