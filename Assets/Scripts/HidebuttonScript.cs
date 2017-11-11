using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HidebuttonScript : MonoBehaviour {

	public GameObject Btn;
	public GameObject hideBtn;
	public GameObject text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ButtonPressedDown(BaseEventData e){
		Btn.SetActive(true);
		text.SetActive (false);
		hideBtn.SetActive (false);
	}
}
