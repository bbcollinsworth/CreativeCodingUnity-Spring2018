using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float maxSpeed = 0.1f;
    public float maxRotation = 3;

	void Start () {
		
	}
	

	void Update () {
        //MOVES ALONG A VECTOR CREATED BY COMBINING LEFT/RIGHT AND UP/DOWN AXIS INPUTS
        //MoveWithKeys();

        //ROTATES WITH LEFT AND RIGHT KEYS, MOVES WITH UP AND DOWN
        MoveAndRotateWithKeys();

        //LOG THE VALUES WE GET WHEN WE PRESS KEYS
        //Debug.Log("Horizontal: " + Input.GetAxis("Horizontal"));
        //Debug.Log("Vertical: " + Input.GetAxis("Vertical"));
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
