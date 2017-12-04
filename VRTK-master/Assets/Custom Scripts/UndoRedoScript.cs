
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoRedoScript : MonoBehaviour
{

    public OVRInput.Controller controller;
	public Vector2 thumbstickPos = new Vector2(0f, 0f);
    public Stack<List<GameObject>> undoStack; //changed from list of strings
    public Stack<List<GameObject>> redoStack;
    public GameObject model;
	private Vector2 oldThumbstickPos = new Vector2(0f, 0f);


    void Start ()
    {
        model = GameObject.Find("model"); //any advantage to using tag here?
		undoStack = new Stack<List<GameObject>>();
		redoStack = new Stack<List<GameObject>>();

		/*if (GameObject.Find ("LeftController").GetComponent <UndoRedoScript> ().undoStack.Count == 0) {
			print ("BEFORE: Undo stack is empty");
		} else {
			print ("BEFORE: Undo stack: " + GameObject.Find ("LeftController").GetComponent <UndoRedoScript> ().undoStack.Peek ());
		}

		if (GameObject.Find ("LeftController").GetComponent <UndoRedoScript> ().redoStack.Count == 0) {
			print ("BEFORE: Redo stack is empty");
		} else {
			print ("BEFORE: Redo stack: " + GameObject.Find ("LeftController").GetComponent <UndoRedoScript> ().redoStack.Peek ());
		}*/

    }

    void Update ()
    {

		oldThumbstickPos = thumbstickPos;
		thumbstickPos = OVRInput.Get (OVRInput.Axis2D.PrimaryThumbstick, controller);
		if (thumbstickPos.x < -0.5f && oldThumbstickPos.x >= -0.5f)
        {
			print ("UNDO");
			if (redoStack.Count == 0) {
				print ("BEFORE: Redo stack is empty");
			} else {
				print ("BEFORE: Redo stack: " + redoStack.Peek ());
			}
				
			if (undoStack.Count == 0) {
				print ("BEFORE: Undo stack is empty");
			} else {
				print ("BEFORE: Undo stack: "+ undoStack.Peek());
				Undo ();
			}

			if (undoStack.Count == 0) {
				print ("AFTER: Undo stack is empty");
			} else {
				print ("AFTER: Undo stack: " + undoStack.Peek ());
			}

			if (redoStack.Count == 0) {
				print ("AFTER: Redo stack is empty");
			} else {
				print ("AFTER: Redo stack: " + redoStack.Peek ());
			}

        }
		else if (thumbstickPos.x > 0.5f && oldThumbstickPos.x <= 0.5f)
        {
			print ("REDO");
			if (undoStack.Count == 0) {
				print ("BEFORE: Undo stack is empty");
			} else {
				print ("BEFORE: Undo stack: " + undoStack.Peek ());
			}

			if (redoStack.Count == 0) {
				print ("BEFORE: Redo stack is empty");
			} else {
				print ("BEFORE: Redo stack: "+ redoStack.Peek());
				Redo ();
			}

			if (undoStack.Count == 0) {
				print ("AFTER: Undo stack is empty");
			} else {
				print ("AFTER: Undo stack: " + undoStack.Peek ());
			}

			if (redoStack.Count == 0) {
				print ("AFTER: Redo stack is empty");
			} else {
				print ("AFTER: Redo stack: " + redoStack.Peek ());
			}
        }


    }

    //dominant hand controller:
    //toggle selection status: push one-item list containing its name onto undo stack
    //deselect all: push list of all objects that were selected onto undo stack

    void Undo()
    {
        List<GameObject> items = undoStack.Pop();
        redoStack.Push(items);
        foreach (GameObject g in items)
        {
			print (g.name);
			g.GetComponent<Object_Selection_Status> ().selectionStatus = !(g.GetComponent<Object_Selection_Status>().selectionStatus); //GameObject.Find(name)
        }

    }

    //for later: performing an action (ie selecting an object or deselecting all) should clear redo stack
    void Redo()
    {
        List<GameObject> items = redoStack.Pop();
        undoStack.Push(items);
        foreach (GameObject g in items)
        {
			g.GetComponent<Object_Selection_Status>().selectionStatus = !(g.GetComponent<Object_Selection_Status>().selectionStatus);
        }
    }

}    

