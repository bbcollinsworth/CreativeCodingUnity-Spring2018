using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightController : MonoBehaviour {

    public Transform target;
    [Range(0,1)]
    public float catchTargetSpeed = 0.9f;
    [Range(0,180)]
    public float maxCatchAngle = 30;
    public Color normalLightColor = Color.white;
    public Color caughtLightColor = Color.red;

    private Vector3 vectorToTarget;
    private Color currentLightColor;
    private Light lightComponent;

	// Use this for initialization
	void Start () {
        lightComponent = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        GetVectorFromLightToTarget();
        //RotateLightToPointAtTarget();
        RotateLightToTargetWithSlerp();
        SetLightColorByAlignmentToTarget();
	}

    void GetVectorFromLightToTarget()
    {
        vectorToTarget = target.position - transform.position;
        //Debug.DrawRay(transform.position, vectorToTarget, Color.red);
    }

    void RotateLightToPointAtTarget()
    {
        transform.rotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
    }

    void RotateLightToTargetWithSlerp()
    {
        Quaternion currentLightRotation = transform.rotation;
        Quaternion desiredLightRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        //LERP BETWEEN LIGHT'S CURRENT ROTATION AND DESIRED ROTATION, UPDATING THOSE VALUES EVERY FRAME
        transform.rotation = Quaternion.Slerp(currentLightRotation, desiredLightRotation, catchTargetSpeed);
    }

    void SetLightColorByAlignmentToTarget()
    {
        float angleBetweenLightForwardAndVectorToTarget = Vector3.Angle(transform.forward, vectorToTarget);
        //Debug.Log(angleBetweenLightForwardAndVectorToTarget);
        currentLightColor = Color.Lerp(caughtLightColor, normalLightColor, angleBetweenLightForwardAndVectorToTarget/maxCatchAngle);
        lightComponent.color = currentLightColor;
    }
}
