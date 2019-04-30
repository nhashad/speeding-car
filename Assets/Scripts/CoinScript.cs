using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//css_import PlayerScript;

public class CoinScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider c){
		PlayerScript.ChangeScore (10);
		PlayerScript.playScoreUp();
		Destroy (this.gameObject);	
	}
}
