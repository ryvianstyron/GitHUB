using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private int LastPlayerDirection;
    public GameObject Player;
    private Rigidbody PlayerRigidBody;

    private const int LEFT = 0;
    private const int RIGHT = 1;
    private const int DEATH_DISTANCE = -25;

    public float MovementSpeedMax;
    private Vector3 MovementSpeedMaxTest;
    public int MovementSpeed; // 250

    public float JumpSpeed = 20;
    private GameObject PlayerGameObject;
    public GameObject WeaponMace;

    private HUDManager HUD;

   public bool IsJumping = false;
   public bool IsFalling = false;
   public bool IsGrounded = true;

    public int GetPlayerDirection()
    {
        return LastPlayerDirection;
    }
    void Start()
    {
        HUD = GameObject.Find("Camera").GetComponent<HUDManager>();

        PlayerGameObject = GameObject.Find("Player");
        PlayerRigidBody = PlayerGameObject.GetComponent<Rigidbody>();

        MovementSpeedMaxTest = PlayerRigidBody.transform.forward * MovementSpeed;
    }
    void FixedUpdate()
    {
        if (PlayerRigidBody.transform.position.y < DEATH_DISTANCE)
        {
            HUD.OnScreenDebugLine("Player Should totally be dead right now");
        }
        if (Input.GetKeyDown("up") && IsGrounded)
        {
            Jump();
        }
        else if (Input.GetKey("left") && !IsJumping && !IsFalling)
        {
            Run(LEFT);
        }
        else if (Input.GetKey("right") && !IsJumping && !IsFalling)
        {
            Run(RIGHT);
        }
    }
    void OnCollisionExit(Collision Collision)
    {
        if (Collision.gameObject.name.Contains("LevelGround") && !IsJumping) 
        {
            //Debug.Log("Exit Level Ground");
            IsFalling = true;
        }
       
        if(Collision.gameObject.name.Contains("Earth_Block") && IsGrounded)
        {
            //Debug.Log("Exit Earth Block & Grounded");
            IsGrounded = true;
            IsFalling = false;
            IsJumping = false;
        }
        if (Collision.gameObject.name.Contains("Earth_Block") && !IsJumping)
        {
            //Debug.Log("Exit Earth Block & !Jumping");
            IsFalling = true;
        }
    }
    void OnCollisionEnter(Collision Collision)
    {
        PickUp Potion;
        Power Power;
        PlayerActions PlayerActions = (PlayerActions)GameObject.Find("Player").GetComponent(typeof(PlayerActions));

        if (Collision.gameObject.name.Contains("LevelGround")) 
        {
            //Debug.Log("Collision level Ground");
            IsFalling = false;
            IsGrounded = true;
            IsJumping = false;
        }
        if (Collision.gameObject.name.Contains("Earth_Block"))
        {
            //Debug.Log("Collision Earth Block");
            IsFalling = false;
            IsGrounded = true;
            IsJumping = false;
        }
        if (Collision.gameObject.name.Contains("ManaPotion"))
        {
            Potion = GameObject.Find("ManaPotion").GetComponent<PickUp>();
            PlayerActions.PickUpMana(Potion);
        }
        else if (Collision.gameObject.name.Contains("HealthPotion"))
        {
            Potion = GameObject.Find("HealthPotion").GetComponent<PickUp>();
            PlayerActions.PickUpHealth(Potion);
        }
        else if (Collision.gameObject.name.Contains("Power"))
        {
            Power = GameObject.Find(Collision.gameObject.name).GetComponent<Power>();
            PlayerActions.PickupPower(Power);
            HUD.OnScreenDebugLine(Player.GetComponent(typeof(Player)).ToString());
        }
    }
    protected void Jump()
    {
        PlayerRigidBody.AddForce(new Vector3(0, 10, 0) * JumpSpeed);
        IsJumping = true;
        IsFalling = true;
        IsGrounded = false;
    }
    protected void Run(int Direction)
    {
        PlayerRigidBody.velocity = MovementSpeedMaxTest; // MovementSpeedMaxText is a Vector3
        if (Direction == LEFT)
        {
            PlayerRigidBody.AddForce(Vector3.left * MovementSpeed);
            LastPlayerDirection = LEFT;
        }
        else if (Direction == RIGHT)
        {
            PlayerRigidBody.AddForce(Vector3.right * MovementSpeed);
            LastPlayerDirection = RIGHT;
        }
    }
}