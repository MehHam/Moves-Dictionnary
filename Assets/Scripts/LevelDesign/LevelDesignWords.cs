using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LevelDesignWords : MonoBehaviour {

	public string wordWrite;
	public string letter; //lettre qui vient d'être entrée correctement
	private bool letterValidated; //la lettre est validée on peut passer à la suivante
	public InputField inputWord; //Le mot à ecrire
	private string[] charsList;   //tableau de char du mot
	public static int currentLetter; //indice de la lettre courante
	public Text dbg3;
	public Text dbg4;

	public Slider sld;
	public InputField inpuf;

	public string pathLoad;

	public static xmmDataContainer xd = new xmmDataContainer();


	public GameObject mainInstanceGO;


	// Use this for initialization
	void Start () {
		pathLoad = Application.persistentDataPath+"/dataStore/"; 

		letterValidated = false;
		letter= "" ;
		wordWrite="";
		currentLetter=0;
		inputWord.text="";
		wordWrite="";//inputWord.text;
		charsList = stringToarray(wordWrite);

		clearer();

		sld.onValueChanged.AddListener(delegate {reloaderSequence(); });
		inputWord.onValidateInput += delegate(string input, int charIndex, char addedChar) { return MyValidate(addedChar); };

		mainInstanceGO = GameObject.Find("hhmmGO");

		}
	
	// Update is called once per frame
	void Update () {
		string s ="";
		dbg4.text = "";
		//for (int i =0;i<xd.dataXmms.Count;i++)
		//	s+=xd.dataXmms[i].label +"\t";
		
		StartCoroutine(wordEvaluator(wordWrite));

		// dbg4.text = xd.dataXmms[0].label + "\t" + xd.dataXmms[1].label + "\t"+xd.dataXmms[2].label + "\t" ; 
	}

	//Checks if a ':' sign is entered....
	private char MyValidate(char charToValidate){
		//xd.dataXmms.Clear();
		if(wordWrite!=""){ // a word isn't 
			
			if (charToValidate == ':'){


				wordWrite=inputWord.text;
				charsList = stringToarray(wordWrite);
				//Debug.Log(charsList.Length);

				currentLetter=1;

				addLetterToSequence("NoN",dbg4);
				xmmProcessingBridge(xd,"NoN");

					for(int i=0;i < charsList.Length;i++){
					
						addLetterToSequence(charsList[i],dbg4);
						}


					//addLetterToSequence(charsList[0],dbg4);
					xmmProcessingBridge(xd,xd.dataXmms.Find(x=> x.label==charsList[0]).label);
					
					dbg3.text=  "Ecris : " + wordWrite + " en ecrivant la lettre " + charsList[currentLetter];

				}else{	//wordWrite="";
				dbg3.text= "tjrs pas de mot opérationel, finis par un : ";
				//reloaderSequence();
				//clearer();
				}

		}else{

			//reloaderSequence();

		}
		return charToValidate;
	}


	public IEnumerator wordEvaluator(string wordWrite)
	{
		//inpuf.text=letter;
		yield return new WaitForEndOfFrame();
		if(charsList.Length!=0 ){

			if(currentLetter >= charsList.Length+1 && currentLetter!=0){
					
					clearer();
					//reloaderSequence();
					letterValidated=false;
					
					//reloaderSequence();
					dbg3.text= "bravo...essaye un nouveau mot";

				}else{						
				if(letter==charsList[currentLetter-1] && currentLetter <= charsList.Length) //<=
						{
							//xd.dataXmms.Remove(xd.dataXmms[xd.dataXmms.Count-1]);	// xd is only consituted of NoN label and the current letter
							
							mainInstanceGO.GetComponent<xmmProcessing>().ts.Clear();
							mainInstanceGO.GetComponent<xmmProcessing>().hhmm.Clear();
							mainInstanceGO.GetComponent<xmmProcessing>().hhmm.Reset();
							currentLetter++ ;		

						xmmProcessingBridge(xd, "NoN");
						xmmProcessingBridge(xd, xd.dataXmms.Find(x=> x.label==charsList[currentLetter]).label);
							
							
							

							

				//	mainInstanceGO.GetComponent<xmmProcessing>().hhmm.Train(mainInstanceGO.GetComponent<xmmProcessing>().ts);					
							
							
							letterValidated=true;
							
							}else{
							letterValidated=false;
							letter = "";						
							dbg3.text = "Ecris : " + wordWrite + " en ecrivant la lettre " + charsList[currentLetter-1];//+ charsList[charsList.Length+2];
					dbg4.text = charsList.Length +"\t"+ currentLetter + "\t" + charsList[currentLetter-1] + "\t" + letter; 
						
						}
				}
		}
	}


	public void addLetterToSequence(string lettre, Text dbg){
		
	if(!xd.dataXmms.Exists(x=> x.label == lettre)){
			
				if(File.Exists(pathLoad + lettre+".xml")){
					if(xmmDataContainer.Load(pathLoad + lettre+".xml").dataXmms.Exists(x=> x.modeRecord == dataStreamer.modeLabel)){
						
						xd.dataXmms.Add(xmmDataContainer.Load(pathLoad + lettre +".xml").dataXmms.FindAll(x=> x.modeRecord== dataStreamer.modeLabel)[0]);

						dbg.text= "\t" + xd.dataXmms[xd.dataXmms.Count-1].label + "\t with " +xd.dataXmms[xd.dataXmms.Count-1].modeRecord+ "\t loaded";
				//Debug.Log("loaded\t" + lettre);
					}
			}else{
				dbg3.text= "\t" + lettre/*xd.dataXmms[xd.dataXmms.Count-1].label*/ + "\t with " + /*xd.dataXmms[xd.dataXmms.Count-1].modeRecord +*/  " doesn't exist...consider recording the lebel first!";
				Debug.Log("\t" + lettre +  "doesn't exist...consider recording the label first!");
				clearer();
			}
	}else{

			Debug.Log(xd.dataXmms.Find(x=> x.label == lettre).label + "\tAlready Exists\t" +xd.dataXmms.FindAll(x=> x.label == lettre).Count);

	}

	}
		


	public void completeWord(){
		if(!inputWord.text.Contains(":"))
			wordWrite=inputWord.text;
		
	}

	//utility 
	private string[] stringToarray(string tex){
		string[] charsList= new string[tex.Length];

		for(int i=0;i<tex.Length;i++){
			charsList[i] = tex[i].ToString();
		}
		return charsList; 
	}


	//command to the xmmProcession class 
	void xmmProcessingBridge(xmmDataContainer xd, string letter){

		dataXmm dtXmm = xd.dataXmms.Find(x=> x.label==letter);
		List<Vector3> lsv = xmmDataContainer.DeserializeVector3Array(dtXmm.rawData);

		mainInstanceGO.GetComponent<xmmProcessing>().Recording(dtXmm.label,mainInstanceGO.GetComponent<xmmProcessing>().dataProcessing(lsv),dataStreamer.modeLabel);
		//processing the loaded data and train the hhmm set
			//mainInstanceGO.GetComponent<xmmProcessing>().dd.options.Add(new Dropdown.OptionData() {text=""+dtXmm.label});
		mainInstanceGO.GetComponent<xmmProcessing>().filter=true;

		//dbg4.text = "regressions : " + mainInstanceGO.GetComponent<xmmProcessing>().hhmm.GetTimeProgressions()[0] + "\t" + mainInstanceGO.GetComponent<xmmProcessing>().hhmm.GetTimeProgressions()[1] ;
	
	}


	void reloaderSequence(){

		/*currentLetter=0;
		charsList = stringToarray(wordWrite);


		dbg3.text="";*/

		currentLetter=0;
		inputWord.text="";
		letterValidated = false;
		charsList = stringToarray(wordWrite);
		xd.dataXmms.Clear();
		mainInstanceGO.GetComponent<xmmProcessing>().ts.Clear();
		mainInstanceGO.GetComponent<xmmProcessing>().hhmm.Reset();

		//dbg3.text="";
		//dbg4.text="";

	}

	//add to the dropdown
	void optionLetterAdder(Dropdown drp,char letter, int index){

		drp.options[index]= new Dropdown.OptionData() {text=""+letter};

		drp.value=0;
	}

	//clean all instances
	public void clearer(){

		currentLetter=0;
		inputWord.text="";
		letterValidated = false;
		charsList = stringToarray(wordWrite);
		xd.dataXmms.Clear();
		mainInstanceGO.GetComponent<xmmProcessing>().ts.Clear();
		mainInstanceGO.GetComponent<xmmProcessing>().hhmm.Reset();

		//dbg3.text="";
		//dbg4.text="";
//		
	}
}
