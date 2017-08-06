using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using UnityEngine;
using System;


	[XmlRoot("dataXmmCollection")]
	public class xmmDataContainer
	{
		[XmlArray("dataXmms"), XmlArrayItem("dataXmm")]
		public List<dataXmm> dataXmms=new List<dataXmm>();
			
		public void addingElements(string path, dataXmm dx1)

		{
		xmmDataContainer xd=xmmDataContainer.LoadFromText(path);

		xd.dataXmms.Add(dx1);

		xd.Save(path);

		}

		public void Save(string path)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(xmmDataContainer));

			using (var stream = new FileStream(path, FileMode.Create))
			{
				serializer.Serialize(stream,this);

			}
		}

		public static xmmDataContainer Load(string path)
		{
			xmmDataContainer xd=new xmmDataContainer();

			var serializer = new XmlSerializer(typeof(xmmDataContainer));
			using (var stream = new FileStream(path, FileMode.Open))
			{
			
				return serializer.Deserialize(stream) as xmmDataContainer;
			}
		}

		//loads the xml directly from the given string. Useful in combination with www.text

		public static xmmDataContainer LoadFromText(string text)
		{
			var serializer = new XmlSerializer(typeof(xmmDataContainer));
			return serializer.Deserialize(new StringReader(text)) as xmmDataContainer;
		}

		public static Vector3 stringToVector3(string text){
		
			//remove the parentheses

			if(text.StartsWith("(") && text.EndsWith(")") ){

				text=text.Substring(1,text.Length-2);
			}

			string[] sArray = text.Split(',');	
			//split the items
					
			if (sArray.Length != 3)
			{
			
			throw new System.FormatException("component count mismatch. Expected 3 components but got " + sArray.Length);
			}

			//store as a vector3
			Vector3 result = new Vector3(float.Parse(sArray[0]),float.Parse(sArray[1]),float.Parse(sArray[2]));
			return result;

		}

		public static List<Vector3> DeserializeVector3Array(string aData)
		{
		string[] vectors = aData.Split('|');

		List<Vector3> result = new List<Vector3>();//[vectors.Length];
		for (int i = 0; i < vectors.Length-1; i++) //to avoid black string
		{
			
			//string[] values = vectors[i].Split(',');
			result.Add(stringToVector3(vectors[i]));

		}

		return result;
		}



		public bool attributeOverloader(dataXmm datXmm1){
			
			foreach(dataXmm xmmdat in this.dataXmms){
				
				if(xmmdat.isEqual(datXmm1))
				return true;
			}
		return false;
		}
		



	}

	



