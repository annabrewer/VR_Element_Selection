using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour {

	public GameObject OVRCameraRig;
	public float scaleFactor = 1f;

	private float prevDist = 0f; //Check previous distance of controllers

	private float handTriggerStateLeft = 0;
	private float oldHandTriggerStateLeft = 0;

	private float handTriggerStateRight = 0;
	private float oldHandTriggerStateRight = 0;

	private bool leftIsPressed = false;
	private bool rightIsPressed = false;

	private Vector3 referencePosLeft;
	private Vector3 referencePosRight;

	// Update is called once per frame
	void Update () {

		//Take in hand positions.
		Vector3 lpos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch); 
		Vector3 rpos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

		/*
		//Check both triggers are pressed
		float leftTriggerPress = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
		float rightTriggerPress = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);
		//print ("Left Trigger Press: " + leftTriggerPress);
		//print ("Right Trigger Press: " /+ rightTriggerPress);

		//Scale at constant rate as long as controllers are moving towards/away
		if (leftTriggerPress >= 0.8 && rightTriggerPress >= 0.8) {
			float dist = (lpos - rpos).magnitude;
			float delta = dist - prevDist;
			if (Mathf.Abs(delta) > 0.01f) {
				OVRCameraRig.transform.localScale += (Vector3.one * Time.deltaTime * Mathf.Sign (delta) * scaleFactor);
			}

			prevDist = dist;
		}
		*/

		//Adjust scale based on difference between controller positions
			//Take position when triggers are pressed
			//If user brings controllers together by 1 unit, shrink camera by half
			//If user pushes controllers apart by 1 unit, multiply camera by 2
			//For as long as controllers are still pressed, keep adjusting


		//Take in trigger positions, comparing to previous positions.
		oldHandTriggerStateLeft = handTriggerStateLeft;
		oldHandTriggerStateRight = handTriggerStateRight;

		handTriggerStateLeft = OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
		handTriggerStateRight = OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);


		//Set booleans that are true if the triggers are pressed.
		if (handTriggerStateLeft > 0.9f) {
			leftIsPressed = true;
		} else {
			leftIsPressed = false;
		}

		if (handTriggerStateRight > 0.9f) {
			rightIsPressed = true;
		} else {
			rightIsPressed = false;
		}


		//When the triggers are pressed, set the controller's current location as a reference position
		if (handTriggerStateLeft > 0.9f && oldHandTriggerStateLeft < 0.9f) {
			referencePosLeft = lpos;
		}

		if (handTriggerStateRight > 0.9f && oldHandTriggerStateRight < 0.9f) {
			referencePosRight = rpos;
		}


		//If both triggers are pressed...
		if (leftIsPressed && rightIsPressed) {
			//Compare reference distance against current distance
			float referenceDistance = (referencePosLeft - referencePosRight).magnitude;
			float currentDistance = (lpos - rpos).magnitude;
			float delta = currentDistance - referenceDistance;

			//If new distance is larger than previous distance, grow camera a proportional amount
			//BUT ONLY UNTIL its scale reaches the desired size
			OVRCameraRig.transform.localScale += (Vector3.one * Time.deltaTime * delta * scaleFactor);
		}

	}
}
