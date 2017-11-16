using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementFilterDisplay : MonoBehaviour {

	private int selectionMode;
	private GameObject vertexObj;
	private GameObject edgeObj;
	private GameObject faceObj;
	private GameObject objectObj;
	private GameObject allObj;



	// Use this for initialization
	void Start () {
		vertexObj = transform.Find ("Vertex").gameObject;
		edgeObj = transform.Find ("Edge").gameObject;
		faceObj = transform.Find ("Face").gameObject;
		objectObj = transform.Find ("Object").gameObject;
		allObj = transform.Find ("All").gameObject;

	}
	
	// Update is called once per frame
	void Update () {
		selectionMode = MenuModeSelection.selectionMode;

		//Enable and disable child components based on selection mode.
		if (selectionMode == 0) {
			vertexObj.active = true;
			edgeObj.active = false;
			faceObj.active = false;
			objectObj.active = false;
			allObj.active = false;
		} 

		if (selectionMode == 1) {
			vertexObj.active = false;
			edgeObj.active = true;
			faceObj.active = false;
			objectObj.active = false;
			allObj.active = false;
		} 

		if (selectionMode == 2) {
			vertexObj.active = false;
			edgeObj.active = false;
			faceObj.active = true;
			objectObj.active = false;
			allObj.active = false;
		} 

		if (selectionMode == 3) {
			vertexObj.active = false;
			edgeObj.active = false;
			faceObj.active = false;
			objectObj.active = true;
			allObj.active = false;
		} 

		if (selectionMode == 4) {
			vertexObj.active = false;
			edgeObj.active = false;
			faceObj.active = false;
			objectObj.active = false;
			allObj.active = true;
		} 


	}
}

