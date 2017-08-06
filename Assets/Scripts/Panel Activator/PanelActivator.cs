using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelActivator : MonoBehaviour {
	public GameObject settingsPanel;
	public GameObject openButton;

	private bool panelActive = false;

	public void close (){
		panelActive = !panelActive;
		settingsPanel.GetComponent<Animator> ().SetBool ("PanelActive",panelActive);
	}

	public void OpenSettings(){
		panelActive = !panelActive;
		settingsPanel.GetComponent<Animator> ().SetBool ("PanelActive",panelActive);
	}

	public void TogglePanel(){
		if (!panelActive) {
			OpenSettings ();
		} else {
			close ();
		}
	}
}
