using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTips : MonoBehaviour {
	public bool leftToolTipButton_Pressed;
	public bool rightToolTipButton_Pressed;
	// Use this for initialization
	public GameObject rct; //right controller tips
	public GameObject lct; //left controller tips

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		/*
		toolTipButton_Pressed = (OVRInput.Get (OVRInput.Button.Two, OVRInput.Controller.RTouch) || OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.LTouch));
		if (toolTipButton_Pressed == true) {
			rct.SetActive (true);
			lct.SetActive (true);
		} else {
			rct.SetActive (false);
			lct.SetActive (false);
		}
		*/

		rightToolTipButton_Pressed = (OVRInput.Get (OVRInput.Button.Two, OVRInput.Controller.RTouch));
		if (rightToolTipButton_Pressed) {
			rct.SetActive (true);
		} else {
			rct.SetActive (false);
		}

		leftToolTipButton_Pressed = (OVRInput.Get (OVRInput.Button.Two, OVRInput.Controller.LTouch));
		if (leftToolTipButton_Pressed) {
			lct.SetActive (true);
		} else {
			lct.SetActive (false);
		}


	}
}
