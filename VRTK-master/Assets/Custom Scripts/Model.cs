using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {

    float edgeRadius = 0.4F;
    float vertexRadius = 0.4F;

    void Start ()
    {
        this.gameObject.name = "model";
        //this.gameObject.tag = "model"; - probably don't need a tag
        InstantiateCube();
	}
	
	void Update ()
    {
		// :^)
	}

    //to create a different type of object, make a different method
    void InstantiateCube ()
    {
        float size = 1;
        this.gameObject.transform.position = new Vector3(0, size / 2, 0);
        this.gameObject.transform.eulerAngles = new Vector3(45, 45, 45);
        
        List<Vector3> axes = new List<Vector3> { Vector3.up, Vector3.right, Vector3.forward, Vector3.down, Vector3.left, Vector3.back };

        //can change naming scheme, for example: "face001"

        //instantiate faces
        foreach (Vector3 v in axes)
        {
            string x = v.x.ToString();
            string y = v.y.ToString();
            string z = v.z.ToString();
            string faceName = "face" + x + y + z;
            //bug here - bottom face is facing up instead of down!!
            Vector3 dir = 90 * (Vector3.down - v);
            //Vector3 dir = 90 * new Vector3(v.z, v.y, v.x);
            InstantiateFace(faceName, v*size*5, dir, Vector3.one * size);
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
                        string edgeName = "edge" + x + y + z;

                        Vector3 dir = 90 * (Vector3.one - new Vector3(v.z, v.y, v.x));
                        InstantiateEdge(edgeName, v * size * 5, dir, size * 5);
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
                    string vertexName = "vertex" + x + y + z;
                    InstantiateVertex(vertexName, v * size * 5);
                }
            }
        }
    }

    void InstantiateFace (string name, Vector3 position, Vector3 rotation, Vector3 scale)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
        obj.name = name;
        obj.transform.parent = this.gameObject.transform;
        obj.transform.localPosition = position;
        obj.transform.localEulerAngles = rotation; //LocalEulerAngles takes Vector3, LocalRotation takes Quaternion
        obj.transform.localScale = scale;
        obj.tag = "face";
        //obj.AddComponent<Object_Selection_Status>();
    }

    void InstantiateEdge (string name, Vector3 position, Vector3 rotation, float length)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        obj.name = name;
        obj.transform.parent = this.gameObject.transform;
        obj.transform.localPosition = position;
        obj.transform.localEulerAngles = rotation; //LocalEulerAngles takes Vector3, LocalRotation takes Quaternion
        obj.transform.localScale = new Vector3(edgeRadius, length, edgeRadius);
        obj.tag = "edge";
        //obj.AddComponent<Object_Selection_Status>();
    }

    void InstantiateVertex (string name, Vector3 position)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.name = name;
        obj.transform.parent = this.gameObject.transform;
        obj.transform.localPosition = position;
        obj.transform.localScale = Vector3.one * vertexRadius;
        obj.tag = "vertex";
        //obj.AddComponent<Object_Selection_Status>();
    }

}
