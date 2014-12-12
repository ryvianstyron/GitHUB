using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private const int LEFT = 0;
    private const int RIGHT = 1;

    private int LastPlayerDirection;

    public Vector3 MovementSpeedMax;
    private int MovementSpeed; // 250

    private float JumpSpeed;
    public GameObject WeaponMace;

    private HUDManager HUD;

    public bool IsInMidair; 
    public bool IsGrounded;
    public bool LedgeTriggerActivated;

    private bool IsEarthPickedUp = false;
    private bool IsFirePickedUp = false;

    private Player PlayerScript;
    private CheckpointsHolder CheckpointsHolder;

    public GameObject PlayerGO;
    void Awake()
    {
        IsInMidair = true;
        IsGrounded = false;

        MovementSpeed = 250;
        JumpSpeed = 250f;

        HUD = GameObject.Find("Camera").GetComponent<HUDManager>();

        MovementSpeedMax = gameObject.rigidbody.transform.forward * MovementSpeed;
        PlayerScript = gameObject.GetComponent<Player>();

        CheckpointsHolder = GameObject.Find("Camera").GetComponent<CheckpointsHolder>();
    }
    void Update()
    {
        if (IsGrounded)
        {
            DrawJumpPoint(LastPlayerDirection);
        }
        // JUMP
        if (IsGrounded && Input.GetKeyDown("up"))
        {
            Jump();
            IsInMidair = true;
            IsGrounded = false;
        }
        // MOVE
        if (!IsInMidair && IsGrounded && Input.GetKey("left"))
        {
            Run(LEFT);
        }
        else if(!IsInMidair && IsGrounded && Input.GetKey("right"))
        {
            Run(RIGHT);
        }
		// STOP SLIDING WHEN PLAYER LETS GO
		if (Input.GetKeyUp("left") || Input.GetKeyUp("right"))
		{
			StopSliding();
		}
    }
	void StopSliding()
	{
		if(LastPlayerDirection == RIGHT)
		{
			gameObject.rigidbody.AddForce(Vector3.right * (MovementSpeed/100));
		}
		else if(LastPlayerDirection == LEFT)
		{
			gameObject.rigidbody.AddForce(Vector3.left * (MovementSpeed/100));
		}
	}
    void OnTriggerEnter(Collider Collider)
    {
        // ON TOP OF LEDGE
        if (Collider.gameObject.tag.Contains("Ledge"))
        {
            LedgeTriggerActivated = true;
            IsGrounded = true;
            IsInMidair = false;
        }
        // ON TOP OF DEATH TRIGGER
        if (Collider.gameObject.tag.Contains("DeathTrigger"))
        {
            PlayerScript.SetHealth(0);
        }
        // FULL TRIGGER CHECKPOINT
        else if (Collider.gameObject.name.Contains("Checkpoint"))
        {
            CheckpointBehavior CheckpointBehavior = Collider.gameObject.GetComponent<CheckpointBehavior>();
            CheckpointsHolder.ActivateCheckPointWithNumber(CheckpointBehavior.GetCheckpointNumber());
			HUD.Message.text = "Checkpoint " + CheckpointBehavior.GetCheckpointNumber() + " activated!";
        }
    }
    public void OnTriggerExit(Collider Collider)
    {
        // EXIT FROM TOP
        if (Collider.gameObject.tag.Contains("Ledge"))
        {
            LedgeTriggerActivated = false;
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
		Enemy EnemyScript;

        // HITTING LEDGE FROM SIDE
        if (!LedgeTriggerActivated && Collision.gameObject.tag.Contains("Ledge"))
        {
            if (LastPlayerDirection == RIGHT)
            {
                gameObject.rigidbody.AddForce(Vector3.left * (MovementSpeed / 3));
            }
            else if (LastPlayerDirection == LEFT)
            {
                gameObject.rigidbody.AddForce(Vector3.right * (MovementSpeed / 3));
            }
            IsGrounded = true;
            IsInMidair = false;
        }
        // HITTING FROM SIDE AND TOP LEVELGROUND
        if (Collision.gameObject.tag.Contains("LevelGround"))
        {
            IsGrounded = true;
            IsInMidair = false;
        }
		else if(Collision.gameObject.name.Contains ("Enemy"))
		{
			HUD.Message.text = "Enemy Collision!";
			EnemyScript = (Enemy) Collision.gameObject.GetComponent<Enemy>();
			PlayerScript.SetHealth(PlayerScript.GetHealth() - EnemyScript.GetDamageInflicted());
			Destroy(Collision.gameObject);
		}
        else if(Collision.gameObject.name.Contains("ManaPotion"))
        {
			HUD.Message.text = "Picked Up Mana Potion";
            Potion = GameObject.Find("ManaPotion").GetComponent<PickUp>();
            PlayerActions.PickUpMana(Potion);
        }
        else if (Collision.gameObject.name.Contains("HealthPotion"))
        {
			HUD.Message.text = "Picked Up Health Potion";
            Potion = GameObject.Find("HealthPotion").GetComponent<PickUp>();
            PlayerActions.PickUpHealth(Potion);
        }
        else if (Collision.gameObject.name.Contains("Earth") && !IsEarthPickedUp)
        {
			HUD.Message.text = "Collected Earth Power!";
            Earth = (Earth)GameObject.Find(Collision.gameObject.name).GetComponent<Earth>();
            PlayerActions.PickUpEarth(Earth);
            IsEarthPickedUp = true;
        }
        else if (Collision.gameObject.name.Contains("Fire") && !IsFirePickedUp)
        {
			HUD.Message.text = "Collected Fire Power!";
            Fire = (Fire)GameObject.Find(Collision.gameObject.name).GetComponent<Fire>();
            PlayerActions.PickUpFire(Fire);
            IsFirePickedUp = true;
        }
    }
    public void OnCollisionExit(Collision Collision)
    {
        if(Collision.gameObject.tag.Contains("LevelGround"))
        {
            IsInMidair = true;
            IsGrounded = false;
        }
        if(Collision.gameObject.tag.Contains("Ledge") && IsGrounded)
        {
            IsInMidair = false;
            IsGrounded = true;
        }
    }
    protected void Jump()
    {
        gameObject.rigidbody.AddForce(Vector3.up * JumpSpeed);
    }
    protected void Run(int Direction)
    {
        gameObject.rigidbody.velocity = MovementSpeedMax; // MovementSpeedMaxText is a Vector3
        if (Direction == LEFT)
        {
            //gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            float moveLeft = MovementSpeed * Time.smoothDeltaTime * Input.GetAxis("Horizontal");
            //transform.Translate(-Vector3.left * moveLeft);
            gameObject.rigidbody.AddForce(Vector3.left * MovementSpeed);
            LastPlayerDirection = LEFT;
        }
        else if (Direction == RIGHT)
        {
            //gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            float moveRight = MovementSpeed * Time.smoothDeltaTime * Input.GetAxis("Horizontal");
            //transform.Translate(Vector3.right * moveRight);
            gameObject.rigidbody.AddForce(Vector3.right * MovementSpeed);
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
    public int GetPlayerDirection()
    {
        return LastPlayerDirection;
    }
}