using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {

	float faceThickness = 0.01F;
	float edgeToSizeRatio = 0.05F;

    void Start ()
    {
        this.gameObject.name = "model";
        //this.gameObject.tag = "model"; - probably don't need a tag
        InstantiateCubeWithinCube();
	}
	
	void Update ()
    {
		// :^)
	}

	//makes a big cube containing a smaller cube at a 45 degree angle
	void InstantiateCubeWithinCube () {
		float size = 1;

		GameObject innerCube = new GameObject("innerCube");
		GameObject outerCube = new GameObject("outerCube");

		innerCube.transform.parent = this.gameObject.transform;
		outerCube.transform.parent = this.gameObject.transform;

		innerCube.transform.position = new Vector3(0, 1, size * 2);
		outerCube.transform.position = new Vector3(0, 1, size * 2);

		innerCube.transform.eulerAngles = new Vector3(45, 45, 45);

		InstantiateCube (innerCube, size/3, color:Color.green);
		InstantiateCube (outerCube, size, color:Color.blue);

		/*
		foreach (Transform t in innerCube.transform) {
			GameObject g = t.gameObject;
			if (g.name.Contains ("face")) {
				g.GetComponent<Renderer> ().material.EnableKeyword ("_EMISSION");
				g.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.blue);
			}
		}
		*/

		GameObject lightGameObject = new GameObject ("The Light");
		Light lightComp = lightGameObject.AddComponent<Light> ();
		lightComp.color = Color.white;
		lightGameObject.transform.position = new Vector3 (0, 1 + size * 2 / 3, size * 2);

		GameObject lightGameObject2 = new GameObject ("The Second Light");
		Light lightComp2 = lightGameObject2.AddComponent<Light> ();
		lightComp2.color = Color.white;
		lightGameObject2.transform.position = new Vector3 (0, 1 - size * 2 / 3, size * 2);
	}

    //makes a cube around a parent game object
	void InstantiateCube (GameObject center, float size, Color color)
    {
		//edge radius and vertex radius are equal
        
        List<Vector3> axes = new List<Vector3> { Vector3.up, Vector3.right, Vector3.forward, Vector3.down, Vector3.left, Vector3.back };

        //can change naming scheme, for example: "face001"

        //instantiate faces
        foreach (Vector3 v in axes)
        {
			string x = v.x.ToString();
            string y = v.y.ToString();
            string z = v.z.ToString();
			string faceName = "face" + x + y + z + " " + center.name;
            //super jank TT^TT
			Vector3 dir;
			if (v.y == -1) {
				dir = new Vector3 (180, 0, 0);
			} else {
				dir = 90 * (Vector3.down - v);
			}
			InstantiateFace(center, faceName, v*size / 2, dir, size, size, color);
        }

        //instantiate edges
        for (float vx = -1; vx <= 1; vx++)
        {
            for (float vy = -1; vy <= 1; vy++)
            {
                for (float vz = -1; vz <= 1; vz++)
                {
                    if (Mathf.Abs(vx) + Mathf.Abs(vy) + Mathf.Abs(vz) == 2)
                    {
                        Vector3 v = new Vector3(vx, vy, vz);
                        string x = v.x.ToString();
                        string y = v.y.ToString();
                        string z = v.z.ToString();
						string edgeName = "edge" + x + y + z + " " + center.name;

                        Vector3 dir = 90 * (Vector3.one - new Vector3(v.z, v.y, v.x));
						InstantiateEdge(center, edgeName, v * size / 2, dir, size/2, size*edgeToSizeRatio, color);
                    }
                }
            }
        }

        //instantiate vertices

        for (float vx = -1; vx <= 1; vx += 2)
        {
            for (float vy = -1; vy <= 1; vy += 2)
            {
                for (float vz = -1; vz <= 1; vz += 2)
                {
                    Vector3 v = new Vector3(vx, vy, vz);
                    string x = v.x.ToString();
                    string y = v.y.ToString();
                    string z = v.z.ToString();
					string vertexName = "vertex" + x + y + z + " " + center.name;
					InstantiateVertex(center, vertexName, v * size / 2, size * edgeToSizeRatio, color);
                }
            }
        }
    }

	//in the future, maybe restructure so don't need to pass in parent parameter
    void InstantiateFace (GameObject parent, string name, Vector3 position, Vector3 rotation, float width, float height, Color color)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.name = name;
        obj.transform.parent = parent.transform;
        obj.transform.localPosition = position;
        obj.transform.localEulerAngles = rotation; //LocalEulerAngles takes Vector3, LocalRotation takes Quaternion
		obj.transform.localScale = new Vector3 (width, faceThickness, height);
        obj.tag = "face";
		//obj.AddComponent<BoxCollider> ();
		obj.GetComponent<BoxCollider>().isTrigger = true;
		obj.AddComponent<Rigidbody> ();
		obj.GetComponent<Rigidbody>().useGravity = false;
		obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.AddComponent<Object_Selection_Status>();
		Renderer myRend = obj.GetComponent<Renderer>();
		myRend.material.color = color;
		//myRend.material.SetFloat ("_Mode", 0.0f);
    }

	void InstantiateEdge (GameObject parent, string name, Vector3 position, Vector3 rotation, float length, float radius, Color color)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        obj.name = name;
        obj.transform.parent = parent.transform;
        obj.transform.localPosition = position;
        obj.transform.localEulerAngles = rotation; //LocalEulerAngles takes Vector3, LocalRotation takes Quaternion
		obj.transform.localScale = new Vector3(radius, length, radius);
        obj.tag = "edge";
		//obj.AddComponent<CapsuleCollider> ();
		obj.GetComponent<CapsuleCollider>().isTrigger = true;
		obj.AddComponent<Rigidbody> ();
		obj.GetComponent<Rigidbody>().useGravity = false;
		obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.AddComponent<Object_Selection_Status>();
		Renderer myRend = obj.GetComponent<Renderer>();
		myRend.material.color = color;
		//myRend.material.SetFloat ("_Mode", 0.0f);
    }

	void InstantiateVertex (GameObject parent, string name, Vector3 position, float radius, Color color)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.name = name;
        obj.transform.parent = parent.transform;
        obj.transform.localPosition = position;
		obj.transform.localScale = Vector3.one * radius;
        obj.tag = "vertex";
		//obj.AddComponent<SphereCollider> ();
		obj.GetComponent<SphereCollider>().isTrigger = true;
		obj.AddComponent<Rigidbody> ();
		obj.GetComponent<Rigidbody>().useGravity = false;
		obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.AddComponent<Object_Selection_Status>();
		Renderer myRend = obj.GetComponent<Renderer>();
		myRend.material.color = color;
		//myRend.material.SetFloat ("_Mode", 0.0f);
    }

}
