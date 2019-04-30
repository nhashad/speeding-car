using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//css_import PlayerScript;

public class RadarScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider c){
		PlayerScript.ChangeScore (-50);
		if (!PlayerScript.IsMuted())
			GetComponent<AudioSource> ().Play ();
	}
}
