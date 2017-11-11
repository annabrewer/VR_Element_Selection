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
		if (other.CompareTag ("object")) {
			//print ("Object encountered.");

			if (indexTriggerState >= 0.9f && oldIndexTriggerState < 0.9f) {
				print ("Object selected.");
				Material otherMat = other.GetComponent<Renderer>().material;
				//Color newColor = Color.white;
				//Color newColor = new Vector4(otherMat.color.r, otherMat.color.g, otherMat.color.b, otherMat.color.a / 2);
				//otherMat.color = newColor;
				//otherMat.color.a = 0.5f;
				//other.renderer.material.color = otherMat.ChangeAlpha(0.5);
				if (otherMat.color.a >= 0.1)
					ChangeAlpha (otherMat, otherMat.color.a / 2);
				else
					ChangeAlpha (otherMat, 1f);

				print ("otherMat.color.r: " + otherMat.color.r);
				print ("otherMat.color.g: " + otherMat.color.g);
				print ("otherMat.color.b: " + otherMat.color.b);
				print ("otherMat.color.a: " + otherMat.color.a);
			}

		}
	}

	private static void ChangeAlpha(Material mat, float alphaValue) {
		Color oldColor = mat.color;
		Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);
		mat.SetColor("_Color", newColor);
	}
}
