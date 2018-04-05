using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float maxSpeed = 0.1f;
    public float jumpSpeed = 1;
    public float maxRotation = 3;
    public bool useMyOwnGravity = false;

    public Transform forceVectorSource;

    private bool canJump = true;

    //Declare a variable to store our rigidbody component
    Rigidbody rigidbody;

    void Start () {
        //find our RigidBody component and store it in a variable
        //so we can use/manipulate it, in this script
        rigidbody = GetComponent<Rigidbody>();
	}
	

	void Update () {
        //MOVES ALONG A VECTOR CREATED BY COMBINING LEFT/RIGHT AND UP/DOWN AXIS INPUTS
        //MoveWithKeys();

        //ROTATES WITH LEFT AND RIGHT KEYS, MOVES WITH UP AND DOWN
        //MoveAndRotateWithKeys();

        MoveWithPhysicsForces();

        if (useMyOwnGravity == true)
        {
            AddMyOwnGravity();
        }
        

        //LOG THE VALUES WE GET WHEN WE PRESS KEYS
        //Debug.Log("Horizontal: " + Input.GetAxis("Horizontal"));
        //Debug.Log("Vertical: " + Input.GetAxis("Vertical"));
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OH NO I COLLIDED with " + collision.transform.name);
        if (collision.transform.name == "Floor")
        {
            canJump = true;
        }
    }

    void MoveWithPhysicsForces()
    {

        //CREATE FORCE VECTORS BY READING FROM THE ARROW KEYS
        Vector3 horizontalAxis = Vector3.right;//world x axis
        float horizontalInputValue = Input.GetAxis("Horizontal"); //1 to -1 based on L/R keys
        Vector3 verticalAxis = Vector3.forward; //world z axis
        float verticalInputValue = Input.GetAxis("Vertical"); //1 to -1 based on U/D keys

        //COMBINE DIRECTION (VECTOR), KEY INPUT (FLOAT), AND SPEED MULTIPLIER (FLOAT)
        Vector3 xAxisForce = horizontalAxis * horizontalInputValue * maxSpeed;
       // Debug.DrawRay(transform.position, xAxisForce, Color.red);

        Vector3 zAxisForce = verticalAxis * verticalInputValue * maxSpeed;
        //Debug.DrawRay(transform.position, zAxisForce, Color.blue);

        //ADD THESE FORCES TO THE RIGIDBODY!
        rigidbody.AddForce(xAxisForce);
        rigidbody.AddForce(zAxisForce);

        //IF JUMP HAS JUST BEEN PRESSED AND WE'RE ALLOWED TO JUMP
        if (Input.GetButtonDown("Jump") && canJump == true)
        {
            //CREATE A JUMP VECTOR AND SCALE IT BY SPEED
            Vector3 jumpAxis = Vector3.up;
            Vector3 jumpForce = jumpAxis * jumpSpeed;

            //ADD OUR JUMP FORCE
            rigidbody.AddForce(jumpForce);

            //BLOCK US FROM JUMPING AGAIN UNTIL SOMETHING ELSE
            //(LIKE GROUND COLLISION) RESETS THIS TO TRUE
            canJump = false;
        }

        //CREATE A FORCE VECTOR BY FINDING THE DIRECTION OUR CUBE IS "FACING"
        //AND MULTIPLYING BY MAX SPEED TO SCALE THE FORCE
        //Vector3 forceVector = transform.forward*maxSpeed;

        ////DO THE SAME THING, BUT USE A DIFFERENT OBJECT'S TRANSFORM.FORWARD AS THE DIRECTION OF FORCE
        //Vector3 directionToPush = forceVectorSource.forward;
        //float speedToPush = maxSpeed;
        //Vector3 forceVector = directionToPush * speedToPush;
        ////DRAW THE VECTOR WE'RE USING TO APPLY FORCE FOR DEBUGGING
        //Debug.DrawRay(Vector3.zero, forceVector, Color.red);
        //rigidbody.AddForce(forceVector);

    }

    void AddMyOwnGravity()
    {
        Vector3 gravityVector = Vector3.up*-1;
        float gravitySpeed = 9.8f;
        Vector3 gravityForce = gravityVector * gravitySpeed;
        rigidbody.AddForce(gravityForce);

        Debug.DrawRay(Vector3.zero, gravityForce, Color.cyan);

    }

    void MoveAndRotateWithKeys()
    {
        //ROTATE THE TRANSFORM AROUND THE UP AXIS BY DEGREES BASED ON HORIZONTAL AXIS INPUTS (L/R ARROW KEYS)
        transform.Rotate(transform.up, Input.GetAxis("Horizontal")*maxRotation);

        //CREATE A MOVE VECTOR THAT ALWAYS POINTS IN THE FORWARD DIRECTION OF THIS OBJECT,
        //SCALED BY OUR VERTICAL (UP/DOWN KEYS) AXIS INPUTS AND MAX SPEED VARIABLE
        Vector3 moveVector = transform.forward * Input.GetAxis("Vertical") * maxSpeed;
        //ADD THAT MOVE VECTOR TO OUR CURRENT TRANSFORM (WITHOUT THIS LINE IT WON'T MOVE)
        transform.position += moveVector;

        //DEBUG DRAW THE MOVE VECTOR
        Debug.DrawRay(transform.position, moveVector, Color.red, 0.5f);
        
    }

    void MoveWithKeys()
    {
        //CREATE A MOVE VECTOR BY COMBINING HORIZONTAL AND VERTICAL INPUT VALUES
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //NORMALIZE THE MOVEVECTOR SO IT'S ALWAYS A LENGTH OF ONE NO MATTER WHAT DIRECTION IT'S POINTING IN...
        //(PRESSING TWO KEYS AT THE SAME TIME CAN MAKE THE VECTOR POINT DIAGONALLY)
        //...AND MULTIPLY BY MAX SPEED TO SCALE IT
        moveVector = moveVector.normalized * maxSpeed;
        //ADD THAT MOVE VECTOR TO OUR CURRENT TRANSFORM (WITHOUT THIS LINE IT WON'T MOVE)
        transform.position += moveVector;

        //ANOTHER WAY OF DOING THE ABOVE LINE ...EITHER SHOULD WORK THE SAME
        //transform.Translate(moveVector);

        //LOG THE LENGTH OF OUR MOVEVECTOR AND VISUALIZE IT
        Debug.Log(moveVector.magnitude);
        Debug.DrawRay(transform.position, moveVector, Color.red, 0.5f);
    }
}
