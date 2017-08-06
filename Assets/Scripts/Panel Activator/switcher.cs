using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class switcher : MonoBehaviour {

	private List<string> scences= new List<string> {"Record", "Level1", "Level2"};

	// Use this for initialization
	void Start () {


	}
	
	public void OnButton(){

		switch (this.name){

		case ("first"):
			SceneManager.LoadScene("Record",LoadSceneMode.Single);
		break;

		case ("previous"):
			SceneManager.LoadScene(scences.IndexOf(SceneManager.GetActiveScene().name)-1,LoadSceneMode.Single);
		break;

		case ("next"):
			SceneManager.LoadScene(scences.IndexOf(SceneManager.GetActiveScene().name)+1,LoadSceneMode.Single);
			break;

		
		}


		}


}
