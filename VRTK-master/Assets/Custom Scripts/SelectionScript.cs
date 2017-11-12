using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionScript : MonoBehaviour {

	public OVRInput.Controller controller;


	private float indexTriggerState = 0;
	private float oldIndexTriggerState = 0;
	private Material otherMat;


	// Update is called once per frame
	void Update () {
		oldIndexTriggerState = indexTriggerState;
		indexTriggerState = OVRInput.Get (OVRInput.Axis1D.PrimaryIndexTrigger, controller);

	}

	void OnTriggerStay (Collider other) {

		//print (other.name);
		//print ("test statement");

		if (other.CompareTag ("object")) {

			//IMPORT COLLIDED OBJECT'S STATUS SCRIPT
			Object_Selection_Status objStatus = GameObject.Find (other.name).GetComponent<Object_Selection_Status> ();

			//CHANGE TO HOVER STATE ON HOVER
			objStatus.hoverStatus = true;
			

			//TOGGLE SELECTION STATE WHEN TRIGGER PRESSED
			if (indexTriggerState >= 0.9f && oldIndexTriggerState < 0.9f) {
				if (!objStatus.selectionStatus) {
					print ("Object selected: " + other.name);
					objStatus.selectionStatus = true;
				} else {
					print ("Object deselected: " + other.name);
					objStatus.selectionStatus = false;
				}

			}
		}
	}

	void OnTriggerExit (Collider other) {
		Object_Selection_Status objStatus = GameObject.Find(other.name).GetComponent<Object_Selection_Status>();
		objStatus.hoverStatus = false;
	}

	private static void ChangeAlpha(Material mat, float alphaValue) {
		//Changes the alpha value of a material.
		Color oldColor = mat.color;
		Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);
		mat.SetColor("_Color", newColor);
	}

	private static void ChangeColor(Material mat, float rVal, float gVal, float bVal) {
		//Changes the RGB values of a material.
		Color oldColor = mat.color;
		Color newColor = new Color (rVal, gVal, bVal, oldColor.a);
		mat.SetColor ("_Color", newColor);
	}
}
