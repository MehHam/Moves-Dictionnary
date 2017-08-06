using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;



public class Main : MonoBehaviour {

  InputProcessingChain proc = new InputProcessingChain(128, 16);
  XmmTrainingSet trainingSet = new XmmTrainingSet();
  XmmModel gmm = new XmmModel("gmm");

  public static bool recording = false;
	public  bool newPhrase = false;
  public float[] desc = new float[3];
  string currentLabel = "";
  string likeliest = "UNKNOWN";
  public static string likest;

	//public List<Vector3> phraseList = new List<Vector3>();
  public List<float> phraseList = new List<float>();
  //string[] colNames = new string[] { "magnitude", "frequency", "periodicity" };

	string[] colNames3D = new string[] {"x","y","z","magnitude"};
	string[] colNames2D = new string[] {"x","y","magnitude"};
	//string[] colNames1D = new string[] {"x","y","z","magnitude"};

  // REC Button's ColorBlock
  ColorBlock recButtonColorBlock = new ColorBlock();
  public Text displayText;
  public Text displayText2;
	public Text displayText3;
//	public 	static AudioSource aud = new AudioSource();
	//public Dropdown dd;
	public Dropdown dd;
	public Slider slideMode;

	public static string labelStatic="";



  //============================= Mono Behaviour =============================//

	// Use this for initialization
	void Start() {

	currentLabel= dd.captionText.text;
	
   // gmm.SetLikelihoodWindow(20);

    if (SystemInfo.supportsGyroscope) {
      Input.gyro.enabled = true;
    } else  {
      likeliest = "NO GYRO ON THIS DEVICE";
    }

    // Set REC Button's ColorBlock constant values
    /*recButtonColorBlock.colorMultiplier = 1f;
    recButtonColorBlock.normalColor = Color.white;
    recButtonColorBlock.pressedColor = Color.white;*/

    // Get displayText Component
    	
	}
	
	// Update is called once per frame
	void Update() {}
	/*labelStatic=currentLabel;	
    if (!SystemInfo.supportsGyroscope) return;

    gmm.Filter(desc);
    likeliest = gmm.GetLikeliest();
	Debug.Log("likeliest : " + likeliest);
	
	}

  	void FixedUpdate() {
		
		displayText.text= "likelihhoods is : " +likeliest;
		//displayText2.text = "likelihoods result is :"+ gmm.GetLikelihoods();

		displayText2.text= " ";
		displayText3.text= " ";

		for (int i=0;i<gmm.GetLikelihoods().Length;i++)
		{
			/*if(gmm.GetLikelihoods()[i]<0.01f)
				displayText2.text += 0.0f + "\t";
			else
			displayText2.text += " " + gmm.GetLikelihoods()[i];//.ToString("F2")+ "\t";
		}

		Vector3 rr=inputClass.data ; 
		//float rr=1.0f;
		//rr=AudioVisualizer.spectrumData;
		//displayText3.text = ""+ inputClass.modeInput +"\t"+ inputClass.data.x + "\t"+ inputClass.data.y+"\t" + inputClass.data.z;
		proc.feed(rr);
	   
		if(proc.hasNewFrame()) {
	      desc = proc.getLastFrame();
			//Debug.Log(GameDevWare.Serialization.Json.SerializeToString(desc));

	     /* if(recording) {
	        foreach(float f in desc) {
	         phraseList.Add(f);
	        }
      	}
	
    }*/

	/*public void RecPhase() {
		displayText.text = "recording";

		if (recording) {
			// START REC
			phraseList = new List<float>();
			newPhrase = false;
		} else {
			// STOP REC
			if (phraseList.Count > 0) {
				newPhrase = true;
			}
		}

	}

	public void AddLabelToTrainingSet(List<Vector3> phraseLi) {
		//if (!SystemInfo.supportsGyroscope) return;

		//if (newPhrase) {
			displayText.text = "adding label";
				
			List<float> phrase = threeFloatLists(phraseLi,0).ToArray();
			
			trainingSet.AddPhraseFromData(labelStatic, colNames, phrase, 4, 0);
			
			phrase = threeFloatLists(phraseLi,1).ToArray();
			trainingSet.AddPhraseFromData(labelStatic, colNames, phrase, 3, 0);

			
			
			displayText2.text=  " phrases for " +	labelStatic ; 

			gmm.Train(trainingSet);
			gmm.Reset();

		//}
	}

	public void ClearLabel() {
		if (!SystemInfo.supportsGyroscope) return;

		trainingSet.RemovePhrasesOfLabel(currentLabel);
		gmm.Train(trainingSet);
		gmm.Reset();
	}

	public void ClearAllLabels() {
		if (!SystemInfo.supportsGyroscope) return;

		trainingSet.Clear();
		gmm.Train(trainingSet);
		gmm.Reset();
	}


	public List<float> threeFloatLists(List<float> fl,int w){
		List<float> result=new List<float>();
		for(int i=0;i<fl.Count;i++)
			result.Add(fl[i][w]);
		return result;
	}*/

	/*
  //=============================== UI Events ================================//

  public void OnRecButtonEvent(Button b) {
    recording = !recording;

    if (recording) {
      // START REC
      recButtonColorBlock.highlightedColor = Color.red;
      phraseList = new List<float>();
      newPhrase = false;
    } else {
      // STOP REC
      recButtonColorBlock.highlightedColor = Color.white;
      if (phraseList.Count > 0) {
        newPhrase = true;
      }
    }
    
    b.colors = recButtonColorBlock;

  }

  public void OnLabelDropDownEvent(Dropdown dd) {
   // Debug.Log(dd.captionText.text);
    currentLabel = dd.captionText.text;
  }

  public void OnAddButtonEvent(Button b) {
    if (!SystemInfo.supportsGyroscope) return;

    if (newPhrase) {
			
      float[] phrase = phraseList.ToArray();
      trainingSet.AddPhraseFromData(currentLabel, colNames, phrase, 3, 0);
      gmm.Train(trainingSet);
      gmm.Reset();
	
    }
  }

  public void OnClearLabelButtonEvent(Button b) {
    if (!SystemInfo.supportsGyroscope) return;

    trainingSet.RemovePhrasesOfLabel(currentLabel);
    gmm.Train(trainingSet);
    gmm.Reset();
  }

  public void OnClearAllButtonEvent(Button b) {
    if (!SystemInfo.supportsGyroscope) return;

    trainingSet.Clear();
    gmm.Train(trainingSet);
    gmm.Reset();
  }*/
}
