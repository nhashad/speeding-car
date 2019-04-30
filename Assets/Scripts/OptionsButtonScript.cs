using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//css_import PlayerScript;

public class OptionsButtonScript : MonoBehaviour {

	public GameObject toggle;
	public GameObject hidebtn1;

	public GameObject creditsBtn;
	public GameObject hidebtn2;

	public GameObject mechanismBtn;
	public GameObject mechanismtxt;
	public GameObject creditstxt;

	bool clicked;

	// Use this for initialization
	void Start () {
		clicked = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ButtonPressedDown(BaseEventData e){
		if (!clicked) {
			toggle.SetActive (true);
			creditsBtn.SetActive (true);
			mechanismBtn.SetActive (true);
			clicked = true;
		} else {
			toggle.SetActive (false);
			creditsBtn.SetActive (false);
			mechanismBtn.SetActive (false);
			creditstxt.SetActive (false);
			mechanismtxt.SetActive (false);
			hidebtn1.SetActive (false);
			hidebtn2.SetActive (false);
			clicked = false;
		}

	}


}
