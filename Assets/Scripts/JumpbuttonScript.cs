using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//css_import PlayerScript;

public class JumpbuttonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ButtonPressedDown(BaseEventData e){
		PlayerScript.jump ();
	}
}
