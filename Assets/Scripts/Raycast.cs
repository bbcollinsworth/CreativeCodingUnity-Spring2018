using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour {

    public float raycastDistance = 10;

    public enum RaycastType
    {
        CAMERA,
        MOUSE
    }

    public RaycastType raycastType;

    private Transform lastHitTransform;
    private Color lastHitOriginalColor;
    private Camera camera;

	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        RaycastFromCamera();
	}

    void RaycastFromCamera()
    {
        Color raycastColor = Color.red;

        //create a special type of vector called a ray for doing our raycast
        Ray ray;

        switch (raycastType)
        {
            case RaycastType.CAMERA:
                Vector3 rayVector = transform.forward * raycastDistance;
                ray = new Ray(transform.position, rayVector);
                //break in a switch means 'break' out of the switch, stop checking more cases, you're done
                break;
            case RaycastType.MOUSE:
            default:
                //get the screen coordinates of the mouse
                Vector3 mousePosition = Input.mousePosition;
                //Debug.Log("Mouse coordinates are: " + mousePosition);

                //create a ray going directly into the screen from those coordinates:
                ray = camera.ScreenPointToRay(mousePosition);
                break;
        }

        //Declare an empty raycastHit variable that will be passed into a raycast function
        //and, if we hit something, filled with hit info and bassed back OUT of the raycast function
        RaycastHit hit = new RaycastHit();

        string hitObject = "nothing";// = hit.transform.name;
        float hitDistance = hit.distance;

        //shoot a ray into the world, and IF it hits something,
        //give us back information about that packed into the 'hit' variable
        if (Physics.Raycast(ray, out hit))
        {
            //if we hit something NEW, store the hit transform in our lastHitTransform variable
            if (hit.transform != lastHitTransform)
            {
                lastHitTransform = hit.transform;
                Material hitMaterial = lastHitTransform.GetComponent<Renderer>().material;
                lastHitOriginalColor = hitMaterial.GetColor("_Color");
                hitMaterial.SetColor("_Color", Color.red);

                if (lastHitTransform.tag == "sphere")
                {
                    Debug.Log("I know I hit the sphere");
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.useGravity = true;
                }  
            }

            hitObject = lastHitTransform.name;
            hitDistance = hit.distance;
            raycastColor = Color.green;
        } else
        {
            //if we've stopped hitting something, change the last thing we hit back to original color
            if (lastHitTransform != null)
            {
                lastHitTransform.GetComponent<Renderer>().material.SetColor("_Color", lastHitOriginalColor);
                lastHitTransform = null;
            }
        }

        //Debug.Log("Raycast hit " + hitObject + " at " + hitDistance + " units away");
        //Debug.DrawRay(ray, raycastColor);
    }
}
