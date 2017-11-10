using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScript : MonoBehaviour {

	public OVRInput.Controller controller;

	private float indexTriggerState = 0;
	private float oldIndexTriggerState = 0;
	
	// Update is called once per frame
	void Update () {
		oldIndexTriggerState = indexTriggerState;
		indexTriggerState = OVRInput.Get (OVRInput.Axis1D.PrimaryIndexTrigger, controller);

		print ("Index State: " + indexTriggerState);

	}
}
