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

    public FlockController controller;
    private Animator animator;

    private float fishSpeed;
    private bool isSwimming = false;

    private Vector3 velocity = Vector3.zero;

	void Start () {
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
        //reset our cumulative vectors to zero at the beginning of each Update()
        Vector3 attractVector = Vector3.zero;
        Vector3 avoidVector = Vector3.zero;
        Vector3 alignVector = Vector3.zero;

        int otherFishInRadius = 0;

        //loop through all fish stored in our controller
        for (int i = 0; i < controller.fishArray.Length; ++i)
        {
            Transform otherFish = controller.fishArray[i].transform;

            //make sure the other fish we're comparing to is NOT ourself!!
            if (otherFish == transform)
            {
                continue;
            }

            //get vector and distance to other fish
            Vector3 vectorToOtherFish = otherFish.position - transform.position;
            float distanceToOtherFish = vectorToOtherFish.magnitude;
            //add to our cumulative attract vector
            attractVector += vectorToOtherFish;

            //if the other fish is within our avoid / align radius...
            if (distanceToOtherFish < controller.avoidRadius)
            {
                //add to our cumulative avoid vector and align vector
                avoidVector += (vectorToOtherFish * -1) / distanceToOtherFish;
                alignVector += otherFish.forward;
                //add to the count of other fish within this radius (we'll use it to average later)
                otherFishInRadius++;
            }
        }
        //...done with the loop through other fish

        //divide all our vectors by the total number of other fish included in them, to get averages for each vector
        attractVector = attractVector / controller.fishArray.Length;
        //since there could be no other fish within our avoid radius, make sure we aren't dividing by zero!
        if (otherFishInRadius > 0)
        {
            avoidVector = avoidVector / otherFishInRadius;
            alignVector = alignVector / otherFishInRadius;
        }

        //add all our vectors into final desired velocity for this fish, and multiply by the strength factors
        Vector3 desiredVelocity = attractVector*controller.attractStrength + avoidVector*controller.avoidStrength + alignVector*controller.alignStrength;
        
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
        animator.SetFloat("FishSpeed", fishSpeed*tailSpeedMultiplier);
}
