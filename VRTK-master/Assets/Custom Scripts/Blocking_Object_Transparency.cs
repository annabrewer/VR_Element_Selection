using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking_Object_Transparency : MonoBehaviour {

	public GameObject OVRCameraRig;
	public GameObject leftHandAnchor;
	public GameObject rightHandAnchor;

	private float rightDist;
	private float leftDist;
	
	// Update is called once per frame
	void Update () {
		Vector3 cameraPos = OVRCameraRig.transform.localPosition;
		Vector3 rPos = rightHandAnchor.transform.localPosition;
		Vector3 lPos = leftHandAnchor.transform.localPosition;

		RaycastHit hit;
		Ray camToRight = new Ray (cameraPos, rPos);
		Ray camToLeft = new Ray (cameraPos, lPos);

		rightDist = (rPos - cameraPos).magnitude;
		leftDist = (lPos - cameraPos).magnitude;

		if (Physics.Raycast (camToRight, out hit, rightDist)) {
			if (hit.collider.tag == "object") {
				//print ("Object hit.");
				//GameObject other = hit.collider;
				Material otherMat = hit.collider.GetComponent<Renderer>().material;
				ChangeAlpha (otherMat, 0.5f);
				//Now how to change it back when you cease contact?
			}
		}
	}

	private static void ChangeAlpha(Material mat, float alphaValue) {
		Color oldColor = mat.color;
		Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);
		mat.SetColor("_Color", newColor);
	}
}
