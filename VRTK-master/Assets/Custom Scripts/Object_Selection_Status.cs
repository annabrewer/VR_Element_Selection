using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Selection_Status : MonoBehaviour {

	public bool hoverStatus;
	public bool selectionStatus;
	public float hoverBrightness = 0.2f;
	private Color originalColor;
	private Color originalColorHover;

	// Use this for initialization
	void Start () {
		originalColor = this.GetComponent<Renderer> ().material.color;
		originalColorHover = new Color (originalColor.r + hoverBrightness, originalColor.g + hoverBrightness, originalColor.b + hoverBrightness, originalColor.a);
		hoverStatus = false;
		selectionStatus = false;
	}
	
	// Update is called once per frame
	void Update () {
		Material myMat = this.GetComponent<Renderer> ().material;

		if (selectionStatus && hoverStatus)
			ChangeColor (myMat, 1f, 0f, 0f);
		else if (selectionStatus)
			ChangeColor (myMat, 0.8f, 0f, 0f);
		else if (!selectionStatus && hoverStatus)
			ChangeColor (myMat, originalColorHover.r, originalColorHover.g, originalColorHover.b);
		else
			ChangeColor (myMat, originalColor.r, originalColor.g, originalColor.b);
	}

	void LateUpdate() {
		//hoverStatus = false;
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

	private static void ChangeBrightness (Material mat, float brightness) {
		Color oldColor = mat.color;
		Color newColor = new Color (oldColor.r + brightness, oldColor.g + brightness, oldColor.b + brightness, oldColor.a);
		mat.SetColor ("_Color", newColor);
}
}