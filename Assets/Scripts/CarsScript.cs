using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//css_import PlayerScript;

public class CarsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision c){
		if (!PlayerScript.IsMuted())
			GetComponent<AudioSource> ().Play ();
		PlayerScript.GameIsover ();
	}
}
