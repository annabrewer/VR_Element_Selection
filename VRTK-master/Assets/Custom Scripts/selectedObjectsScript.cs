using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectedObjectsScript : MonoBehaviour {

	public static List<GameObject> selectedObjects = new List<GameObject> ();
	public static bool multiSelectButton = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		multiSelectButton = (OVRInput.Get (OVRInput.Button.One, OVRInput.Controller.LTouch) || OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch));

	}
}
