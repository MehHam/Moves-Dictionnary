using System.Xml;
using System.Collections;
using System.Xml.Serialization; 
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class dataXmm {

	// Use this for initialization 
		[XmlAttribute("label")]
		public string label;
		[XmlAttribute("modeRecord")]
		public string modeRecord;
		[XmlAttribute("rawData")]
		public string rawData;
		//[XmlAttribute("xmmPhrase")]
		//public float[] xmmPhrase;

		public dataXmm(){}

		public dataXmm(string modeRec, string lab,string rawDat){

		this.modeRecord=modeRec;
		this.label=lab;
		this.rawData="";

		}

	//change the values of attirubutes , and extends the data string to the new value 	

	public void dataRecap(string labelTxt, string modeLabel, Vector3 data){		
			this.label= labelTxt;
			this.modeRecord=modeLabel;
		this.rawData += data+"|";//this.vector3ArrToXmlString(data);
	}


	public string vector3ArrToXmlString(Vector3 data){

		this.rawData+=data+"|";

		return this.rawData;
	}

		public bool isEqual(dataXmm x1){

		return (this.modeRecord==x1.modeRecord && this.label==x1.label);
		}



}
