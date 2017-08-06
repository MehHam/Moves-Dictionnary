using System.Xml;
using System.Collections;
using System.Xml.Serialization; 
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class LabelsSequence {

	// Use this for initialization

	[XmlAttribute("nameOfSequence")]
	public string nameOfSequence;
	[XmlAttribute("modeInput")]
	public string modeInput;
	[XmlAttribute("labelsSequence")]
	public string labelsSequence;
	//[XmlAttribute("xmmPhrase")]
	//public float[] xmmPhrase;

	public LabelsSequence(){}

	public LabelsSequence(string nameOfSequence, string modeInput, string labelsSequence){

		this.nameOfSequence=nameOfSequence;
		this.labelsSequence=labelsSequence;
		this.modeInput=modeInput;

	}

	public bool labelSequenceEqualiser(LabelsSequence ls){

		if(this.modeInput==ls.modeInput && this.labelsSequence==ls.labelsSequence)
			return true;
		else
			return false;

	}
		

}
