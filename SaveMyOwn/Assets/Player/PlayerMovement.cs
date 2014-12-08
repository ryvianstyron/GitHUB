using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody PlayerRigidBody;

    private const int LEFT = 0;
    private const int RIGHT = 1;
    private const int DEATH_DISTANCE = -25;

    private int LastPlayerDirection = RIGHT;

    public Vector3 MovementSpeedMax;
    private int MovementSpeed; // 250

    private float JumpSpeed;
    private GameObject PlayerGameObject;
    public GameObject WeaponMace;

    private HUDManager HUD;

    public bool IsInMidair = false;
    public bool IsGrounded = true;

    private bool IsEarthPickedUp = false;
    private bool IsFirePickedUp = false;

    private Player PlayerScript;
    private CheckpointsHolder CheckpointsHolder;

    private bool Entered;

    public GameObject PlayerGO;
    void Start()
    {
        MovementSpeed = 8;
        JumpSpeed = 55;
        HUD = GameObject.Find("Camera").GetComponent<HUDManager>();

        PlayerGameObject = GameObject.Find("Player");
        PlayerRigidBody = PlayerGameObject.GetComponent<Rigidbody>();

        MovementSpeedMax = PlayerRigidBody.transform.forward * MovementSpeed;
        PlayerScript = PlayerGameObject.GetComponent<Player>();

        CheckpointsHolder = GameObject.Find("Camera").GetComponent<CheckpointsHolder>();
    }
    public int GetPlayerDirection()
    {
        return LastPlayerDirection;
    }
    void FixedUpdate()
    {
        if(IsGrounded)
        {
            DrawJumpPoint(LastPlayerDirection);
        }
        if (Input.GetKeyDown("up") && IsGrounded)
        {
            Jump();
        }
        else if (Input.GetKey("left") && !IsInMidair)
        {
            Run(LEFT);
        }
        else if (Input.GetKey("right") && !IsInMidair)
        {
            Run(RIGHT);
        }
    }
    void OnCollisionStay(Collision Collision)
    {
        if(Collision.gameObject.tag.Contains("LevelGround"))
        {
            Debug.Log("OnCollisonStay: " + Collision.gameObject.tag);
            IsInMidair = false;
            IsGrounded = true;
        }
    }
    void OnCollisionExit(Collision Collision)
    {
        if (Collision.gameObject.tag.Contains("Ledge"))
        {
            Debug.Log("OnCollisionExit: " + Collision.gameObject.tag);
            IsInMidair = true;
            IsGrounded = false;
        }     
    }
    void OnCollisionEnter(Collision Collision)
    {
        PickUp Potion;
        Earth Earth;
		Fire Fire;
        PlayerActions PlayerActions = PlayerGO.GetComponent<PlayerActions>();

        if (Collision.gameObject.tag.Contains("Ledge")) 
        {
            Debug.Log("OnCollisionEnter: " + Collision.gameObject.tag);
            IsGrounded = true;
            IsInMidair = false;
        }
        else if(Collision.gameObject.name.Contains("ManaPotion"))
        {
            Potion = GameObject.Find("ManaPotion").GetComponent<PickUp>();
            PlayerActions.PickUpMana(Potion);
        }
        else if (Collision.gameObject.name.Contains("HealthPotion"))
        {
            Potion = GameObject.Find("HealthPotion").GetComponent<PickUp>();
            PlayerActions.PickUpHealth(Potion);
        }
        else if (Collision.gameObject.name.Contains("Earth") && !IsEarthPickedUp)
        {
            Earth = (Earth)GameObject.Find(Collision.gameObject.name).GetComponent<Earth>();
            PlayerActions.PickUpEarth(Earth);
            IsEarthPickedUp = true;
        }
        else if (Collision.gameObject.name.Contains("Fire") && !IsFirePickedUp)
        {
            Fire = (Fire)GameObject.Find(Collision.gameObject.name).GetComponent<Fire>();
            PlayerActions.PickUpFire(Fire);
            IsFirePickedUp = true;
        }
    }
    void OnTriggerEnter(Collider Collider)
    {
        if(Collider.gameObject.tag.Contains("DeathTrigger"))
        {
            PlayerScript.SetHealth(0);
        }
        else if(Collider.gameObject.name.Contains("Checkpoint"))
        {
            CheckpointBehavior CheckpointBehavior = Collider.gameObject.GetComponent<CheckpointBehavior>();
            CheckpointsHolder.ActivateCheckPointWithNumber(CheckpointBehavior.GetCheckpointNumber());
        }
    }
    protected void Jump()
    {
        if (LastPlayerDirection == RIGHT)
        {
            PlayerRigidBody.AddForce(new Vector3(3f, 6f, 0) * JumpSpeed);
        }
        else if (LastPlayerDirection == LEFT)
        {
            PlayerRigidBody.AddForce(new Vector3(-3f, 6f, 0) * JumpSpeed);
        }
        IsInMidair = true;
        IsGrounded = false;
    }
    protected void Run(int Direction)
    {
        PlayerRigidBody.velocity = MovementSpeedMax; // MovementSpeedMaxText is a Vector3
        if (Direction == LEFT)
        {
            //gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            float moveLeft = MovementSpeed * Time.smoothDeltaTime * Input.GetAxis("Horizontal");
            transform.Translate(-Vector3.left * moveLeft);
            
            //PlayerRigidBody.AddForce(Vector3.left * MovementSpeed);
            LastPlayerDirection = LEFT;
        }
        else if (Direction == RIGHT)
        {
            //gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            float moveRight = MovementSpeed * Time.smoothDeltaTime * Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * moveRight);

            //PlayerRigidBody.AddForce(Vector3.right * MovementSpeed);
            LastPlayerDirection = RIGHT;
        }
    }
    private void DrawJumpPoint(int Direction)
    {
        Vector3 JumpPointPosition = Vector3.zero;
        if(IsGrounded)
        {
            if(Direction == LEFT)
            {
                JumpPointPosition = new Vector3(transform.position.x - 2.8f, transform.position.y, transform.position.z);
            }
            else if(Direction == RIGHT)
            {
                JumpPointPosition = new Vector3(transform.position.x + 2.8f, transform.position.y, transform.position.z);
            }
        }
        GameObject.Find("JumpPoint").transform.position = JumpPointPosition;
    }
}