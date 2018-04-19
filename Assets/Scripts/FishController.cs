using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour {

    public float maxSpeed = 0.1f;
    public float maxTurn = 0.01f;
    public float tailSpeedMultiplier = 10;
    public float attractStrength = 1;
    public float avoidRadius = 1;
    public float avoidStrength = 5;
    public float alignStrength = 0.1f;
   // public float forceScale = 10;

    public Transform target;

    private Rigidbody rigidbody;
    private Animator animator;

    private float fishSpeed;
    private bool isSwimming = false;

    private Vector3 velocity = Vector3.zero;

	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        fishSpeed = velocity.magnitude;

        //(for moving with Spacebar...)
  //      if (Input.GetButtonUp("Jump"))
        if (fishSpeed < 0.1f && isSwimming) //isSwimming == true
        {
            //Set FishSwimming parameter in animator component to "false" and return from the update loop
            isSwimming = false;
            animator.SetBool("FishSwimming", isSwimming);
        }

        if (fishSpeed >= 0.1f && !isSwimming) // isSwimming == false
        {
            //Set FishSwimming parameter to "true"
            isSwimming = true;
            animator.SetBool("FishSwimming", isSwimming);
        }

        MoveFishTowardTarget();
        RotateFishToMovementDirection();
        SetTailWagFromFishSpeed();
	}

    void MoveFishTowardTarget()
    {
        //get the vector to our target (that would move us all the way there in one frame)
        Vector3 attractVector = target.position - transform.position;
        //get the distance to our target - the magnitude of that vector
        float distanceToTarget = attractVector.magnitude;
        
        Vector3 avoidVector = Vector3.zero;
        //if the distance to target is less than a radius, start adding avoid force
        if (distanceToTarget < avoidRadius)
        {
            avoidVector = (attractVector * -1)/distanceToTarget;
        }

        Vector3 alignVector = target.forward;

        Vector3 desiredVelocity = attractVector*attractStrength + avoidVector*avoidStrength + alignVector*alignStrength;
        
        //clamp the length of the vector -- the distance that we move in that direction per frame -- below max speed
        desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, maxSpeed * Time.deltaTime);
        //Lerp between our current velocity and desired velocity
        velocity = Vector3.Lerp(velocity,desiredVelocity,maxTurn);
        //add the clamped vector to our position
        transform.position += velocity;
    }

    void RotateFishToMovementDirection()
    {
        Quaternion lookRotation = Quaternion.LookRotation(velocity);
        transform.rotation = lookRotation;
    }

    void SetTailWagFromFishSpeed()
    {
        //since we are storing this variable in update, we don't need to recalculate here anymore
        //Vector3 velocity = rigidbody.velocity;
        //Debug.DrawRay(transform.position, velocity,Color.red);
        //float speed = velocity.magnitude;

        animator.SetFloat("FishSpeed", fishSpeed*tailSpeedMultiplier);// speed);
    }
}
