using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public GameObject Player;
    private Rigidbody PlayerRigidBody;

    private const int LEFT = 0;
    private const int RIGHT = 1;

    private Vector3 MovementSpeedMax;
    private float MaxSpeedInMetersPerSecond = 10;
    private int MovementSpeed = 300;

    private float JumpSpeed = 25; 
    private GameObject PlayerGameObject;
    public GameObject WeaponMace;

    bool IsJumping = false;
    bool IsFalling = false;

	void Start ()
	{
        PlayerGameObject = GameObject.Find("Player");
        PlayerRigidBody = PlayerGameObject.GetComponent<Rigidbody>();
        MovementSpeedMax = (PlayerRigidBody.transform.forward) * MaxSpeedInMetersPerSecond;
	}
	void Update () 
	{
		if (Input.GetKeyDown("up")) 
		{
            if (!IsJumping && !IsFalling)
            {
                Jump();
            }
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
    // Checking for collisions here?
    // Check for nearest objects for pickup?
    void OnCollisionEnter(Collision Collision)
    {
        PickUp Potion;
        PlayerActions PlayerActions = (PlayerActions)GameObject.Find("Player").GetComponent(typeof(PlayerActions));

        if (Collision.gameObject.name.Contains("LevelGround"))
        {
            Debug.Log("Collision Detected: Ground");
            IsJumping = false;
            IsFalling = false;
        }
        if (Collision.gameObject == GameObject.Find("ManaPotion"))
        {
            Debug.Log("Collision Detected: ManaPotion");
            Potion = GameObject.Find("ManaPotion").GetComponent<PickUp>();
            PlayerActions.PickUpMana(Potion); 
        }
        else if (Collision.gameObject == GameObject.Find("HealthPotion"))
        {
            Debug.Log("Collision Detected: HealthPotion");
            Potion = GameObject.Find("HealthPotion").GetComponent<PickUp>();
            PlayerActions.PickUpHealth(Potion); 
        }
    }
	protected void Jump()
	{
        PlayerRigidBody.AddForce(new Vector3(0, 10, 0) * JumpSpeed);
        Debug.Log("Jump");
        IsJumping = true;
        IsFalling = true;
	}
    // Which force mode to use? Ali has never actually used it
    // Forces in general are good for sudden movement changes, like shooting for example
    protected void Run(int Direction) 
    {
        PlayerRigidBody.velocity = MovementSpeedMax; // Make sure you never go too fast
        if(Direction == LEFT)
        {
            Debug.Log("Run Left");
            PlayerRigidBody.AddForce(Vector3.left * MovementSpeed);
            //Player.transform.Translate(-Time.deltaTime * Speed, 0, 0);
        }
        else if(Direction == RIGHT) 
        {
            Debug.Log("Run Right");
            PlayerRigidBody.AddForce(Vector3.right * MovementSpeed);
            //Player.transform.Translate(Time.deltaTime * Speed, 0, 0);
        }
    }
}
