using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeDisplay : MonoBehaviour {
	public Text txt;
	public Text txt2;
	public Slider sldr;

	// On value change 

	public void Update(){

		txt.text=dataStreamer.modeLabel;
		txt2.text=dataStreamer.data+"";

	}

}
