using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePosition : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //SETS THE TRANSFORM TO THIS VALUE ON START/PLAY (IF UNCOMMENTED):
        //transform.position = new Vector3(1, 1.18f, 1);

        //SETS THE SCALE TO THIS VALUE ON START/PLAY:
        transform.localScale = new Vector3(2, 2.5f, 2);

        Debug.Log("Start Function called at " + Time.time);
	}
	
	// Update is called once per frame
	void Update () {
        //SETS THE SAME POSITION (0,1,0) EVERY FRAME (IF UNCOMMENTED):
        //transform.position = new Vector3(0, 1, 0);

        //ADDS THIS VECTOR TO THE CURRENT POSITION EVERY FRAME:
        transform.position += new Vector3(0, 1, 0);

        //SETS THE SCALE TO THE SAME NEW VALUE EVERY FRAME (IF UNCOMMENTED):
        //transform.localScale = new Vector3(2, 2.5f, 2);
        Debug.Log("Update Function called at " + Time.time);
	}
}

//A PSEUDO-CODE EXAMPLE OF WHAT THE TRANSFORM COMPONENT SCRIPT MIGHT LOOK LIKE:
//class Transform
//{
//    Vector3 position;
//    Quaternion rotation;
//    Vector3 scale;
//}
