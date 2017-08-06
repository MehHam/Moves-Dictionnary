using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class labelsGestion : MonoBehaviour {
	private string recPath;
	public Text dbgText;
	// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame

		public void labelErazer(){

			recPath= inputClass.recPath;
			File.Delete(recPath + dataStreamer.nameLabel+".xml");
			if(!File.Exists(recPath + dataStreamer.nameLabel+".xml"))
				dbgText.text=dataStreamer.nameLabel+".xml has been deleted";
			else
				dbgText.text=dataStreamer.nameLabel+".xml still there";

		}

		public void erazerAll(){
		recPath= inputClass.recPath;
				
				var hi = Directory.GetFiles(recPath);
				foreach(string f in hi)
				{

					File.Delete(f);

				}		
				dbgText.text=" All labels erazed ";
			//}
		}

}
