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


	private GameObject mainInstanceGO;


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

		for (int i =0;i<xd.dataXmms.Count;i++)
			s+=xd.dataXmms[i].label +"\t";
		
		wordEvaluator(wordWrite);
	}

	//Checks if a ':' sign is entered....
	private char MyValidate(char charToValidate){
		//xd.dataXmms.Clear();
		if(wordWrite!=""){ // a word isn't 
			if (charToValidate == ':'){
				
				wordWrite=inputWord.text;
				charsList = stringToarray(wordWrite);

				currentLetter=0;

				//xmmProcessingBridge(xd,"NoN");

				for(int i=0;i < charsList.Length;i++){
					
					addLetterToSequence(charsList[i],dbg3);
					xmmProcessingBridge(xd,charsList[i]);

					}
				addLetterToSequence("NoN",dbg4);
				xmmProcessingBridge(xd,charsList[0]);

				}else{	//wordWrite="";
				dbg3.text= "tjrs pas de mot opérationel, finis par \" : ";
				reloaderSequence();
				//clearer();
			}

		}
		return charToValidate;
	}


	public void wordEvaluator(string wordWrite)
	{
		inpuf.text=letter;

		if(charsList.Length!=0){

			if(currentLetter == charsList.Length && currentLetter!=0){
					
					reloaderSequence();
					letterValidated=false;
					dbg4.text= "bravo...essaye un nouveau mot";
					//reloaderSequence();
					

				}else{						
					if(letter==charsList[currentLetter] && charsList[currentLetter] != ":" && currentLetter <= charsList.Length) //<=
						{
							currentLetter++;
							letterValidated=true;
							
						}else{
							letterValidated=false;
							dbg3.text = "Ecris : " + wordWrite + " en ecrivant la lettre " + charsList[currentLetter];
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
			}}else{
				dbg.text= "\t" + xd.dataXmms[xd.dataXmms.Count-1].label + "\t with " +xd.dataXmms[xd.dataXmms.Count-1].modeRecord+  " doesn't exist...consider recording the lebel first!";
				Debug.Log("\t" + lettre +  "doesn't exist...consider recording the lebel first!");
				clearer();
			}
		}else{

		//	Debug.Log(xd.dataXmms.Find(x=> x.label == lettre).label + "\tAlready Exists\t" +xd.dataXmms.FindAll(x=> x.label == lettre).Count);

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
		//processing the loaded date and train the hhmm set
		//mainInstanceGO.GetComponent<xmmProcessing>().dd.options.Add(new Dropdown.OptionData() {text=""+dtXmm.label});
		//mainInstanceGO.GetComponent<xmmProcessing>().filter=true;

		dbg4.text = letter + " label has been RECORDED" + mainInstanceGO.GetComponent<xmmProcessing>().name ;
	
	}


	void reloaderSequence(){

		currentLetter=0;
		charsList = stringToarray(wordWrite);


		dbg4.text="";
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
		charsList = stringToarray(wordWrite);
		xd.dataXmms.Clear();
		//dbg3.text="";
		dbg4.text="";
//		
	}
}
