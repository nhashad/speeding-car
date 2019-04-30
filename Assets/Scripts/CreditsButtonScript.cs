using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreditsButtonScript : MonoBehaviour {

	public GameObject creditsBtn;
	public GameObject hideBtn;
	public GameObject credits;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ButtonPressedDown(BaseEventData e){
		creditsBtn.SetActive(false);
		credits.SetActive (true);
		hideBtn.SetActive (true);
	}
}
