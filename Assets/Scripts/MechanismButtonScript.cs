using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MechanismButtonScript : MonoBehaviour {

	public GameObject mechanismBtn;
	public GameObject hideBtn;
	public GameObject mechanism;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	public void ButtonPressedDown(BaseEventData e){
		mechanismBtn.SetActive(false);
		mechanism.SetActive (true);
		hideBtn.SetActive (true);
	}
}
