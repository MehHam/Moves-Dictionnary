using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using UnityEngine;
using System;

	[XmlRoot("labelsSequenceCollection")]
	public class labelsSequenceContainer
	{

	[XmlArray("LabelsSequences"), XmlArrayItem("LabelsSequence")]
	public List<LabelsSequence> LabelsSequences = new List<LabelsSequence>();


	public void Save(string path)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(labelsSequenceContainer));

		using (var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream,this);

		}
	}

	public static labelsSequenceContainer Load(string path)
	{
		labelsSequenceContainer xd=new labelsSequenceContainer();

		var serializer = new XmlSerializer(typeof(labelsSequenceContainer));
		using (var stream = new FileStream(path, FileMode.Open))
		{

			return serializer.Deserialize(stream) as labelsSequenceContainer;
		}
	}

	public void sequencesErazer(){
		this.LabelsSequences.Clear();
	}

	public int findWithName(string name){
	
		for(int i=0;i<this.LabelsSequences.Count;i++){
			if(this.LabelsSequences[i].nameOfSequence==name)
				return i;
		
		}

		return -1;
	}





	}
