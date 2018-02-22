using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS CAUSES OUR CODE TO RUN EVEN IN EDIT MODE (WHEN PLAY IS *NOT* PRESSED... USE WITH CAUTION!!)
[ExecuteInEditMode]
public class VectorVisualizer : MonoBehaviour {

    //this is where you declare variables to be used anywhere in this script / class
    //examples:
    int myInt;
    float myFloat;
    string myString;
    bool myBoolean;

    //ADDING 'PUBLIC' LETS US EDIT THESE VARIABLES IN THE INSPECTOR BEFORE PRESSING PLAY
    public Vector3 VectorA;
    public Vector3 VectorB;

    //struct Vector3
    //{
    //    float x;
    //    float y;
    //    float z;
    //}

	void Start () {
        //MyInt = 1;
        //Debug.Log(VectorA);
        //VectorA = new Vector3(1, 2, -2);
        
        
	}
	
	void Update () {
        //myInt = myInt + 1;
        //VectorA += new Vector3(0, 1, 0);

        //THE LENGTH OF VECTOR A
        Debug.Log(VectorA.magnitude);

        //DRAW VECTOR A
        //Debug.DrawRay(Vector3.zero, VectorA, Color.cyan);
        //DRAW VECTOR B
        //Debug.DrawRay(Vector3.zero, VectorB, Color.blue);
        //DRAW THE SUM OF VECTOR A AND VECTOR B
        //Debug.DrawRay(Vector3.zero, VectorA + VectorB, Color.green);
        //DRAW VECTOR B, OFFSET TO START FROM VECTOR A (WILL END AT SAME POINT AS SUM VECTOR ABOVE)
        //Debug.DrawRay(VectorA, VectorB, Color.yellow);
        //DRAW THE DIFFERENCE BETWEEN VECTOR B AND A (WILL POINT FROM A TO B)
        //Debug.DrawRay(Vector3.zero, VectorB-VectorA, Color.red);

        //DRAW THE WORLDSPACE GIZMO VECTORS
        //Debug.DrawRay(Vector3.zero, Vector3.up, Color.green);
        //Debug.DrawRay(Vector3.zero, Vector3.right, Color.red);
        //Debug.DrawRay(Vector3.zero, Vector3.forward, Color.blue);

        //DRAW THE GIZMO VECTORS OF THIS OBJECT IN ITS LOCAL SPACE
        Debug.DrawRay(transform.position, transform.up, Color.green);
        Debug.DrawRay(transform.position, transform.right, Color.red);
        Debug.DrawRay(transform.position, transform.forward, Color.blue);
    }
}
