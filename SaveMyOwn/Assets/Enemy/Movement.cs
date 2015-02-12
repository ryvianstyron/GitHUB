using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    //public GameObject Enemy;
    public Rigidbody EnemyRigidBody;

    private const int Up = 0;
    private const int Down = 1;
   
   // private GameObject EnemyGameObject;
    private float JumpSpeed = 25;

    bool IsJumping;
    bool IsFalling;
    bool IsGrounded;
    void Start()
    {
        IsJumping = false;
        IsFalling = false;
        IsGrounded = true;

        //spawn = (Transform)Instantiate(Enemy, transform.position, transform.rotation);
        //Debug.Log(Enemy.name + "is spawned at" + transform.position);
       // EnemyGameObject = Rigidbody.Find("EnemyRigidBody");
       //  EnemyRigidBody.GetComponent<Rigidbody>();

        StartCoroutine(WaitAndJump());
    }
    IEnumerator WaitAndJump()
    {
        yield return new WaitForSeconds(2);
        Jump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsGrounded)
        {
            IsJumping = false;
            IsFalling = false;
            //Debug.Log("Enemy Grounded");
            //HUD.OnScreenDebugLine("Grounded");
        }
        if (IsJumping)
        {
            IsGrounded = false;
            IsFalling = true;
            //Debug.Log("Enemy Jumping");
        }

        if (IsFalling)
        {
            IsGrounded = false;
            IsJumping = true;
            //Debug.Log("Enemy Falling");
        }
    }
    protected void Jump()
    {

        EnemyRigidBody.AddForce(new Vector3(0, 10, 0) * JumpSpeed);
        IsJumping = true;
        IsFalling = true;
        IsGrounded = false;
    }
    void OnCollisionEnter(Collision Collision)
    {
        if (Collision.gameObject.name.Contains("LevelGround")) // Here's the problem
        {
            //GroundTouchCount++;
            //HUD.OnScreenDebugLine("Collision Detected: Ground " + GroundTouchCount);
            IsGrounded = true;
            IsJumping = false;
            IsFalling = false;
            Jump();
        }
    }
}
