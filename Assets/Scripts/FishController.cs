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
        Vector3 attractVector = Vector3.zero;
        Vector3 avoidVector = Vector3.zero;
        Vector3 alignVector = Vector3.zero;

        int otherFishInRadius = 0;

        for (int i = 0; i < controller.fishArray.Length; ++i)
        {
            Transform otherFish = controller.fishArray[i].transform;

            if (otherFish == transform)
            {
                continue;
            }

            Vector3 vectorToOtherFish = otherFish.position - transform.position;
            float distanceToOtherFish = vectorToOtherFish.magnitude;

            attractVector += vectorToOtherFish;

            if (distanceToOtherFish < controller.avoidRadius)
            {
                avoidVector += (vectorToOtherFish * -1) / distanceToOtherFish;
                alignVector += otherFish.forward;
                otherFishInRadius++;
            }
        }

        attractVector = attractVector / controller.fishArray.Length;
        if (otherFishInRadius > 0)
        {
            avoidVector = avoidVector / otherFishInRadius;
            alignVector = alignVector / otherFishInRadius;
        }


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
        //since we are storing this variable in update, we don't need to recalculate here anymore
        //Vector3 velocity = rigidbody.velocity;
        //Debug.DrawRay(transform.position, velocity,Color.red);
        //float speed = velocity.magnitude;

        animator.SetFloat("FishSpeed", fishSpeed*tailSpeedMultiplier);// speed);
    }
}
