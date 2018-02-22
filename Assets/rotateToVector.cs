using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateToVector : MonoBehaviour {

    public Transform targetToPointAt;
    Quaternion rotationToLookAtTarget;

	void Update () {
        //Get the vector from this transform to our target...
        //Remember, this is ALWAYS endpoint minus startpoint (target position minus start position)
        Vector3 directionFromLightToTarget = targetToPointAt.position - transform.position;
        //Debug.DrawRay(transform.position, directionFromLightToTarget, Color.cyan);

        //create a quaternion that will rotate to face z-axis in exactly the direction of this vector
        rotationToLookAtTarget = Quaternion.LookRotation(directionFromLightToTarget);

        //set our rotation to the quaternion we've created
        transform.rotation = rotationToLookAtTarget;
	}
}
