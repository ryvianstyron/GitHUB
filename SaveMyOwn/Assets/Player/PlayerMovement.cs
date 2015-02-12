using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private const int LEFT = 0;
    private const int RIGHT = 1;

    private int LastPlayerDirection;

    public Vector3 MovementSpeedMax;
    public int MovementSpeed; //10

    public float JumpSpeed; //600
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

    private float MidAirTimer = 0.0f;
    void Awake()
    {
        IsInMidair = true;
        IsGrounded = false;

        HUD = GameObject.Find("Camera").GetComponent<HUDManager>();

        MovementSpeedMax = gameObject.rigidbody.transform.forward * MovementSpeed;
        PlayerScript = gameObject.GetComponent<Player>();

        CheckpointsHolder = GameObject.Find("Camera").GetComponent<CheckpointsHolder>();
    }
    void FixedUpdate()
    {
        if(IsInMidair)
        {
            MidAirTimer += Time.deltaTime;
            //Debug.Log("MidAirTimer:" + MidAirTimer);
            if(MidAirTimer > 0.5f)
            {
                //Debug.Log("MASS CHANGE!");
                gameObject.rigidbody.mass = 100;
            }
        }
        // JUMP
        if (Input.GetKeyDown("up") && IsGrounded)
        {
            Jump();
            IsInMidair = true;
            IsGrounded = false;
        }
        // MOVE
        if (Input.GetKey("left") && !IsInMidair && IsGrounded)
        {
            Run(LEFT);
        }
        
        if(Input.GetKey("right") && !IsInMidair && IsGrounded)
        {
            Run(RIGHT);
        }
		/*// STOP SLIDING WHEN PLAYER LETS GO
		if (Input.GetKeyUp("left") || Input.GetKeyUp("right"))
		{
			StopSliding();
		}*/
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

        PlayerActions PlayerActions = PlayerGO.GetComponent<PlayerActions>();
        PickUp Potion;
        if (Collider.gameObject.name.Contains("ManaPotion"))
        {
            HUD.Message.text = "Picked Up Mana Potion";
            Potion = GameObject.Find("ManaPotion").GetComponent<PickUp>();
            PlayerActions.PickUpMana(Potion);
        }
        else if (Collider.gameObject.name.Contains("HealthPotion"))
        {
            HUD.Message.text = "Picked Up Health Potion";
            Potion = GameObject.Find("HealthPotion").GetComponent<PickUp>();
            PlayerActions.PickUpHealth(Potion);
        }
    }
    void OnCollisionEnter(Collision Collision)
    {
        Earth Earth;
		Fire Fire;
        PlayerActions PlayerActions = PlayerGO.GetComponent<PlayerActions>();
		Enemy EnemyScript;

        // HITTING FROM SIDE AND TOP LEVELGROUND
        if (Collision.gameObject.tag.Contains("LevelGround"))
        {
            IsGrounded = true;
            IsInMidair = false;
            MidAirTimer = 0.0f;
            gameObject.rigidbody.mass = 1;
        }
		if(Collision.gameObject.name.Contains ("Enemy"))
		{
			HUD.Message.text = "Enemy Collision!";
			EnemyScript = (Enemy) Collision.gameObject.GetComponent<Enemy>();
			PlayerScript.SetHealth(PlayerScript.GetHealth() - EnemyScript.GetDamageInflicted());
			Destroy(Collision.gameObject);
		}
        if (Collision.gameObject.name.Contains("Earth") && !IsEarthPickedUp)
        {
			HUD.Message.text = "Collected Earth Power!";
            Earth = (Earth)GameObject.Find(Collision.gameObject.name).GetComponent<Earth>();
            PlayerActions.PickUpEarth(Earth);
            IsEarthPickedUp = true;
        }
        if (Collision.gameObject.name.Contains("Fire") && !IsFirePickedUp)
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
    }
    protected void Jump()
    {
        //gameObject.rigidbody.AddForce(Vector3.up * JumpSpeed,ForceMode.Impulse);
        gameObject.rigidbody.AddForce(Vector3.up * JumpSpeed, ForceMode.VelocityChange);
    }
    protected void Run(int Direction)
    {
        // Make movementSpeedMax a scalar  - Use as a multiplier
        //gameObject.rigidbody.velocity = MovementSpeedMax; // MovementSpeedMaxText is a Vector3
        //Debug.Log ("Horizontal Input Axis: " + Input.GetAxis ("Horizontal"));

        Vector3 desiredVelocity = Vector3.zero;

        if (Direction == LEFT)
        {
            //       gameObject.rigidbody.AddForce(Vector3.left * MovementSpeed, ForceMode.VelocityChange); // Use Force Mode Velocity
            desiredVelocity = Vector3.left * MovementSpeed;
            LastPlayerDirection = LEFT;
        }
        else if (Direction == RIGHT)
        {
            //			gameObject.rigidbody.AddForce(Vector3.right * MovementSpeed, ForceMode.VelocityChange);
            desiredVelocity = Vector3.right * MovementSpeed;
            LastPlayerDirection = RIGHT;
        }
        gameObject.rigidbody.AddForce(desiredVelocity - rigidbody.velocity, ForceMode.VelocityChange);
    }

    public int GetPlayerDirection()
    {
        return LastPlayerDirection;
    }
}