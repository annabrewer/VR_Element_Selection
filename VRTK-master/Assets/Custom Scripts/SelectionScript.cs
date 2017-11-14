using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScript : MonoBehaviour {

	public OVRInput.Controller controller;

	private bool multiSelectButton;
	private float indexTriggerState = 0;
	private float oldIndexTriggerState = 0;
	private Material otherMat;
	private List<GameObject> selectedObjects;
	private GameObject currentHover = null;
	private Object_Selection_Status currentStatus;
	private int selectionMode;
	private bool validInteraction;

	private int triggerCount = 0;

	//use fixed update to sync w/ ontrigger
	void FixedUpdate () {
		oldIndexTriggerState = indexTriggerState;
		indexTriggerState = OVRInput.Get (OVRInput.Axis1D.PrimaryIndexTrigger, controller);

		selectedObjects = selectedObjectsScript.selectedObjects;
		multiSelectButton = selectedObjectsScript.multiSelectButton;

		//print ("Old trigger state: " + oldIndexTriggerState);
		//print ("New trigger state: " + indexTriggerState);

		selectionMode = MenuModeSelection.selectionMode;

		//print ("Index Trigger State: " + indexTriggerState);
		//print ("Multi Select Button State: " + multiSelectButton);

		//IF TRIGGER IS PRESSED W/O HOLDING DOWN MULTI SELECT BUTTON, CLEAR SELECTED LIST
		if ((indexTriggerState >= 0.8f && oldIndexTriggerState < 0.8f) && !multiSelectButton) {
			//print ("TRIGGERED" + triggerCount);
			triggerCount++;
			foreach (GameObject selectedObject in selectedObjects) {
				Object_Selection_Status objStatus = GameObject.Find (selectedObject.name).GetComponent<Object_Selection_Status> ();
				if (selectedObject != currentHover) {
					objStatus.selectionStatus = false;
				}
			}
		}
		/*
		if (currentHover != null) {
			print ("Currently hovering on: " + currentHover.name);
		}
		*/
	}

	void OnTriggerEnter (Collider other) {

	}

	void OnTriggerStay (Collider other) {
		/* currentHover is the game object that this controller should be giving the hover state to.
		 * Whenever the controller collides with another game object, it might set the new object to be currentHover.
		 * An object should set its hover state to on only if it is the currentHover.
		 */
		//Check Selection mode to ensure valid interaction.
		if ((selectionMode == 0 || selectionMode == 4) && (other.CompareTag("vertex"))) {
			validInteraction = true;
		} else if ((selectionMode == 1 || selectionMode == 4) && (other.CompareTag("edge"))) {
			validInteraction = true;
		} else if ((selectionMode == 2 || selectionMode == 4) && (other.CompareTag("face"))) {
			validInteraction = true;
		} else if ((selectionMode == 3 || selectionMode == 4) && (other.CompareTag("object"))) {
			validInteraction = true; 
		} else {
			validInteraction = false;
		}


		if (validInteraction && 
			((currentHover == null) 
				//|| (other.gameObject.tag == currentHover.tag)
				|| (other.gameObject.tag == "vertex")
				|| (other.gameObject.tag == "edge" && !(currentHover.tag == "vertex" || currentHover.tag == "edge")) 
				|| (other.gameObject.tag == "face" && !(currentHover.tag == "vertex" || currentHover.tag == "edge" || currentHover.tag == "face"))
				|| (other.gameObject.tag == "object" && (currentHover.tag != "vertex" && currentHover.tag != "edge" && currentHover.tag != "face" && currentHover.tag != "object"))))
		{
			currentHover = other.gameObject;
		}

		//print (other.name);
		//print ("test statement");

		if (validInteraction) {

			print ("Valid interaction.");
			
			//IMPORT COLLIDED OBJECT'S STATUS SCRIPT
			Object_Selection_Status objStatus = GameObject.Find (other.name).GetComponent<Object_Selection_Status> ();


			//TURN ON THE HOVER STATE IF THIS OBJECT IS THE CURRENT HOVER OBJECT; ELSE TURN IT OFF
			if (other.gameObject == currentHover) {
				objStatus.hoverStatus = true;
			} else {
				objStatus.hoverStatus = false;
			}


			//TOGGLE SELECTION STATE WHEN TRIGGER PRESSED
			if ((indexTriggerState >= 0.8f && oldIndexTriggerState < 0.8f) && (other.gameObject == currentHover)) {
				if (!objStatus.selectionStatus) {
					print ("Object selected: " + other.name);
					objStatus.selectionStatus = true;
					selectedObjects.Add (other.gameObject);
				} else {
					print ("Object deselected: " + other.name);
					objStatus.selectionStatus = false;
					selectedObjects.Remove (other.gameObject);
				}

			}
		}
	}

	void OnTriggerExit (Collider other) {
		Object_Selection_Status objStatus = GameObject.Find(other.name).GetComponent<Object_Selection_Status>();
		objStatus.hoverStatus = false;
		if (other.gameObject == currentHover) {
			currentHover = null;
		}
	}
		
}
