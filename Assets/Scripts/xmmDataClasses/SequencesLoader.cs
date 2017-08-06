using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using System;


public class SequencesLoader : MonoBehaviour {

	public Dropdown dd;

	public Text dbgtxt;

	private string path;
	public static string labelListe;

	public static labelsSequenceContainer lsc= new labelsSequenceContainer ();


	// Use this for initialization
	void Start () {
		optionsChanger();


	}



	public void optionsChanger(){

		List<string> listeSequences=new List<string>();
		listeSequences.Clear();
		reinit();

		lsc = labelsSequenceContainer.Load(inputClass.recPath+ "00_Sequences.xml");
		foreach(LabelsSequence ls in lsc.LabelsSequences){
			if(ls.modeInput== dataStreamer.modeLabel){
				
				listeSequences.Add(ls.nameOfSequence);
				dd.options.Add(new Dropdown.OptionData() {text=ls.nameOfSequence});
			}
		}
			
	}

	public void changedValue(){
		int indexOfsequence = lsc.findWithName(dd.options[dd.value].text);
		labelListe= lsc.LabelsSequences[indexOfsequence].labelsSequence;
		dbgtxt.text= "Sequence is : "+lsc.LabelsSequences[indexOfsequence].nameOfSequence + "has mode : " + lsc.LabelsSequences[indexOfsequence].modeInput +" and its sequence is : " + lsc.LabelsSequences[indexOfsequence].labelsSequence ;//  " or " + labelListe works fine too;
			

	}

	public void reinit(){
		dd.options.Clear();
		dd.GetComponentInChildren<Text>().text="";
		dbgtxt.text="";
	}

	// Update is called once per frame


}
