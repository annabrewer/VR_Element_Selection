using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //using Contains method

public class Blocking_Object_Transparency : MonoBehaviour {

	public GameObject CenterEyeAnchor;
	public GameObject leftHandAnchor;
	public GameObject rightHandAnchor;

	private float rightDist;
	private float leftDist;
	private List<GameObject> blockingObjects = new List<GameObject>();

	// Update is called once per frame
	void Update () {
		//SET CAMERA & CONTROLLER POSITIONS
		Vector3 cameraPos = CenterEyeAnchor.transform.position;
		Vector3 rPos = rightHandAnchor.transform.position;
		Vector3 lPos = leftHandAnchor.transform.position;
		//Vector3 lPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch); 
		//Vector3 rPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

		//SET RAYS FROM CAMERA TO CONTROLLERS
		Ray camToRight = new Ray (cameraPos, rPos - cameraPos);
		Ray camToLeft = new Ray (cameraPos, lPos - cameraPos);

		//CALCULATE DISTANCE BETWEEN CAMERA AND CONTROLLERS
		rightDist = (rPos - cameraPos).magnitude;
		leftDist = (lPos - cameraPos).magnitude;

		//RAYCAST PREP
		RaycastHit[] hitsRight;
		RaycastHit[] hitsLeft;
		hitsRight = Physics.RaycastAll (camToRight, rightDist);
		hitsLeft = Physics.RaycastAll (camToLeft, leftDist);
		RaycastHit[] hitsBoth = hitsRight.Concat (hitsLeft).ToArray ();

		//List<GameObject> blockingObjects = new List<GameObject>();
		List<GameObject> hitObjects = new List<GameObject> ();

		//ITERATE THROUGH HIT OBJECTS
		for (int i = 0; i < hitsBoth.Length; i++) {
			RaycastHit hit = hitsBoth [i];
			hitObjects.Add(hit.collider.gameObject);
			if (hit.collider.tag == "object") {
				//print ("Object collided.");
				Material hitMat = hit.collider.GetComponent<Renderer>().material;
				GameObject hitObject = hit.collider.gameObject;

				//CHECK IF IT'S ALREADY IN BLOCKING OBJECTS
				bool alreadyListed = false;
				foreach (GameObject blockingObject in blockingObjects) {
					if (hitObject == blockingObject) {
						alreadyListed = true;
					} 
				}

				//IF IT'S NOT, ADD IT TO BLOCKING OBJECTS AND TURN IT TRANSPARENT
				if (!alreadyListed) {
					ChangeAlpha (hitMat, 0.5f);
					blockingObjects.Add (hitObject);
					print ("Object added.");
					print ("Blocking objects: " + blockingObjects);
				}
			}
		}

		//IF AN OBJECT IN BLOCKING OBJECTS IS NOT IN HIT OBJECTS, RESTORE ITS ALPHA VALUE TO 1 AND REMOVE IT FROM BLOCKING OBJECTS
		print("Hit Objects: " + hitObjects.Count());
		foreach (GameObject hitObject in hitObjects) {
			print (hitObject.name);
		}
		print ("Blocking Objects: " + blockingObjects.Count ());
		foreach (GameObject blockingObject in blockingObjects) {
			print (blockingObject.name);
		}

		foreach (GameObject blockingObject in blockingObjects) {
			bool stillHit = false;
			foreach (GameObject hitObject in hitObjects) {
				if (blockingObject == hitObject) {
					stillHit = true;
				}
			}
			if (!stillHit) {
				ChangeAlpha (blockingObject.GetComponent<Renderer> ().material, 1f);
				blockingObjects.Remove (blockingObject);
			}
		}

	}

	private static void ChangeAlpha(Material mat, float alphaValue) {
		Color oldColor = mat.color;
		Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);
		mat.SetColor("_Color", newColor);
	}
}
