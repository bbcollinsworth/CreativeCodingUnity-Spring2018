using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour {

    public float forceScale = 10;

    public Transform target;

    private Rigidbody rigidbody;
    private Animator animator;

    private float fishSpeed;
    private bool isSwimming = false;

	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        fishSpeed = rigidbody.velocity.magnitude;

        //(for moving with Spacebar...)
  //      if (Input.GetButtonUp("Jump"))
        if (fishSpeed < 0.1f && isSwimming) //isSwimming == true
        {
            //Set FishSwimming parameter in animator component to "false" and return from the update loop
            isSwimming = false;
            animator.SetBool("FishSwimming", isSwimming);
        }

        //(for moving with Spacebar...)
        //      if (Input.GetButtonDown("Jump"))
        if (fishSpeed >= 0.1f && !isSwimming) // isSwimming == false
        {
            //Set FishSwimming parameter to "true"
            isSwimming = true;
            animator.SetBool("FishSwimming", isSwimming);
        }

        //(for moving with Spacebar...)
        //if (Input.GetButton("Jump"))
        //      {
        //          MoveFishWithPhysics();
        //      }

        MoveFishTowardTarget();
        RotateFishToMovementDirection();
        SetTailWagFromFishSpeed();
	}

    void MoveFishWithPhysics()
    {
        //create movement acceleration force
        Vector3 forceVector = transform.forward * forceScale * Time.deltaTime;
        rigidbody.AddForce(forceVector);
    }

    void MoveFishTowardTarget()
    {
        Vector3 moveVector = target.position - transform.position;
        rigidbody.AddForce(moveVector);
    }

    void RotateFishToMovementDirection()
    {
        Quaternion lookRotation = Quaternion.LookRotation(rigidbody.velocity);
        transform.rotation = lookRotation;
    }

    void SetTailWagFromFishSpeed()
    {
        //since we are storing this variable in update, we don't need to recalculate here anymore
        //Vector3 velocity = rigidbody.velocity;
        //Debug.DrawRay(transform.position, velocity,Color.red);
        //float speed = velocity.magnitude;

        animator.SetFloat("FishSpeed", fishSpeed);// speed);
    }
}
