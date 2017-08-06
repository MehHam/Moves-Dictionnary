using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class xmmProcessing : MonoBehaviour {

  float gyroDistanceThreshold = 2;
  
	float[] prevGyroCoords=new float[3];
	float[] gyroCoords=new float[3];
	float[] gyroDelta=new float[3];

  List<float> phrase;
  bool recordEnabled = false;
  //bool record = false;
  public bool filter = false;
  //string label = "";
  string likeliest = "NONE";
  float[] likelihoods = new float[0];
  private XmmTrainingSet ts = new XmmTrainingSet();
  private XmmModel hhmm = new XmmModel("hhmm");


	string[] colNames3D = new string[] {"x","y","z"};
	string[] colNames2D = new string[] {"x","y","magnitude"};
	string[] colNames1D = new string[] {"magnitude"} ;

	//private List<string> modesInputs= new List<string> {"Audio", "Gyro", "Compass", "Accelero", "Tactile"};

	float[] thresholds = new float[]{10,10,10,10,2};//gyro, compass, accelro, tactile mic is separated

	public Text displayText;
	public Text displayText2;
	public Text displayText3;
	//	public 	static AudioSource aud = new AudioSource();
	//public Dropdown dd;
	//public Dropdown dd;
	// Use this for initialization

	void Start () {
		//ts.Clear();
    hhmm.SetStates(10);
    hhmm.SetLikelihoodWindow(5);
    hhmm.SetRelativeRegularization(0.01f);
    hhmm.SetGaussians(1);
	}
	
	// Update is called once per frame
	void Update () {
		displayText2.text="Filtering is : " + filter +" labels are : " ;//+ts.GetNbOfLabels() + "\t label is :" + ts.GetLabels().Length; 
		//displayText2.text=" Likelihood is: " ;//likelihoodsString(ts); 

    if (filter) {
			gyroDistanceThreshold =	thresholds[dataStreamer.modeValue];	//threshold as a function of the recording mode
     
			gyroCoords[0] = dataStreamer.data[0];
			gyroCoords[1] = dataStreamer.data[1];
			gyroCoords[2] = dataStreamer.data[2];

		if (distance(gyroCoords, prevGyroCoords) > gyroDistanceThreshold) {
			//not over the threshold
				prevGyroCoords[0] = dataStreamer.data[0];
				prevGyroCoords[1] = dataStreamer.data[1];
				prevGyroCoords[2] = dataStreamer.data[2];

			}else { //filter if there s a motion
		  hhmm.Filter(gyroDelta);
          likeliest = hhmm.GetLikeliest();
          likelihoods = hhmm.GetLikelihoods();
        }
      }
    }    

	private string likelihoodsString(XmmTrainingSet ts){
		string s ="";
		for (int i=0;i<ts.GetLabels().Length;i++)
			s+= hhmm.GetTimeProgressions().Length + "\t" ;	
		return s;
	}

	public List<float> dataProcessing(List<Vector3> rawData){
		//calculate the difference between positions for a list of Vector data types Vector1, 2 or 3 
		List<float> fl=new List<float>();

		for(int j=1;j<rawData.Count;j++){
			for(int k=0;k<3;k++)
			fl.Add(rawData[j][k]-rawData[j-1][k]);
		}
		return fl;
	}

	public void Recording(string label, List<float> phrase,string type) {
   // record = false;

    	float[] p = phrase.ToArray();
		int inDim=0;
		int outDim=0;
		string[] colNames= new string[0];
	
		inDim=3;
		outDim=0;
		colNames=new string[3];
		colNames=colNames3D;


		ts.AddPhraseFromData(label, colNames, p, inDim, outDim);
    	hhmm.Train(ts);
    	hhmm.Reset();

		displayText.text="record";
		displayText2.text= "label being recorded is " + label; 
  }

 	private void startFiltering() {
    filter = true;
		prevGyroCoords[0] =  dataStreamer.data[0];
		prevGyroCoords[1] =  dataStreamer.data[1];
		prevGyroCoords[2] =  dataStreamer.data[2];
  }

	private void stopFiltering() {
    filter = false;
    hhmm.Reset();
  }

  	private float distance(float[] newPos, float[] prevPos) {
		gyroDelta[0] = newPos[0] - prevPos[0];
		gyroDelta[1] = newPos[1] - prevPos[1];
		gyroDelta[2] = newPos[2] - prevPos[2];

		return (float)Math.Sqrt(gyroDelta[0] * gyroDelta[0] + gyroDelta[1] * gyroDelta[1] + gyroDelta[2] + gyroDelta[2]);
  }
		
 	private void logLabels() {
    string[] labels = ts.GetLabels();
    Debug.Log("nb of labels : " + labels.Length);
    for (int i = 0; i < labels.Length; ++i) {
      Debug.Log("label " + i + " : " + labels[i]);
    }
  }
}
