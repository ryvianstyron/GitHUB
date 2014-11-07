using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour 
{
    const int FORWARD = 0;
    const int BACKWARD = 1;
    const int LEFT = 2;
    const int RIGHT = 3;

    private Vector3 MovementSpeedMax;
    //public GameObject Catapult;
    
    public float MovementSpeed = 5;
    public int RotationSpeed = 10;

    bool Turning = false; // Later
    bool Moving = false; // Later

	void Update () 
    {
        if(Input.GetKey("w")) // Move Catapault Forward
        {
            MovementSpeedMax = transform.forward  * MovementSpeed;
            Move(FORWARD);
        }
        else if (Input.GetKey("s")) // Move Catapault Backward
        {
            MovementSpeedMax = transform.forward * -1 * MovementSpeed;
            Move(BACKWARD);
        }
        else if (Input.GetKey("a")) // Turn Catapault Left 
        {
            Turn(LEFT);
        }
        else if (Input.GetKey("d")) // Turn Catapault Right
        {            
            Turn(RIGHT);
        }
	}
    public void Move(int Direction)
    {
        rigidbody.velocity = MovementSpeedMax;
        switch(Direction)
        {
            case FORWARD:
                rigidbody.AddForce(transform.forward * MovementSpeed);
                break;
            case BACKWARD:
                rigidbody.AddForce(transform.forward * -1 * MovementSpeed);
                break;
        }
    }
    public void Turn(int Direction)
    {
        float RotateValueInYAxis = transform.rotation.y; //Current Rotation of Object 
        switch (Direction)
        {
            case LEFT:
                if (RotateValueInYAxis >= -0.3)
                {
                    transform.Rotate(Vector3.down * (RotationSpeed * Time.deltaTime));
                }
                break;
            case RIGHT:
                if (RotateValueInYAxis <= 0.3)
                {
                    transform.Rotate(Vector3.up * (RotationSpeed * Time.deltaTime));
                }
                break;
        }
    }
}
