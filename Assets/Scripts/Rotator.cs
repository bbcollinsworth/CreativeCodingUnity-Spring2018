using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    //axis to rotate around
    public Vector3 axis;

    //angle to rotate...
    [Range(0,360)]
    public float angle;

    //Don't edit this directly...it's just so you can see what a quaternion looks like
    public Quaternion rotation;

    //how long we wait between moving
    public float timeBetweenMoves = 1;

    public bool isPendulum = false;

    //the next time (in seconds) we want to move
    private float nextTime = 0;

	void Start () {
        

    }
	
	void Update () {

        //Draw the axis we're rotating around (if you uncomment the below line...)
        //Debug.DrawRay(transform.position, axis, Color.yellow);

        if (isPendulum)
        {
            //call this function
            MakePendulumSwing();

            //stop our update loop here (return from it) and skip everything beneath
            return;
        }
        
        //Compare current time to next move time, if you want to...
        //Debug.Log("Current time is " + Time.time + ", and Next Time to Move is " + nextTime);
        
        //if the current time is equal to or greater than the next time we should move
        if (Time.time >= nextTime)
        {
            
            //set our next time target to be one increment bigger
            nextTime += timeBetweenMoves;

            //AND THEN DO THE ROTATION:
            AddRotationIncrement();
        }
    }

    void AddRotationIncrement()
    {
        //this is how we add to our position to move something in space
        //transform.position = transform.position + Vector3 whereWereMovingTo;

        //set up the angle we want to move this frame
        float angleThisFrame = angle;

        if (timeBetweenMoves <= 0)
        {
            //if we want to move every frame, smooth out our motion by multiplying by the fraction of a second since last update()
            angleThisFrame = angle * Time.deltaTime;
        }

        //create a rotation from the angle to rotate this frame, and the axis we want to rotate around
        rotation = Quaternion.AngleAxis(angleThisFrame, axis);

        //store our transform's current rotation in a variable
        Quaternion ourLastRotation = transform.rotation;

        //This just sets our rotation to the new rotation:
        //transform.rotation = rotation;

        //this adds a rotation increment to our object's current transform.rotation
        transform.rotation = rotation * ourLastRotation;
    }

    void MakePendulumSwing()
    {
        //Get the sine of current time... this value will change as time progresses
        float sineOfTime = Mathf.Sin(Time.time);
        //Debug.Log(sineOfTime);

        //multiplying our angle float by sine of time will give us a value that 
        //moves between -1 * angle and 1 * angle as sineOfTime changes...
        float pendulumAngle = angle*sineOfTime;
        //create a rotiation of that angle around our axis
        Quaternion newPendulumRotation = Quaternion.AngleAxis(pendulumAngle,axis);

        //set our rotation to this new rotation
        transform.rotation = newPendulumRotation;
    }
}
