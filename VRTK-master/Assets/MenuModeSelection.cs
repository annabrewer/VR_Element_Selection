using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuModeSelection : MonoBehaviour {

	public static int selectionMode = 4; //0 - vertex, 1 - edge, 2 - face, 3 - object, 4 - all
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	public void ChangeMode(int newMode){
		selectionMode = newMode;
		print ("Selection Mode: " + selectionMode);
	}
}
