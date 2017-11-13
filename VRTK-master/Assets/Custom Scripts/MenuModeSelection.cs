using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuModeSelection : MonoBehaviour {
	//Controls what the selection mode is based on what the user chooses from the radial menu. This is to be attatched to the scriptmanager object. Also, attatch the radial menu object to the left controller,
	//and if the menu does not pop up upon clicking the joystick, try assigning left and right controllers to the script aliases slot in the VRTK SDK object.

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
