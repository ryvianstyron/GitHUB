using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public GameObject Player;

    private bool IsRunning;
    private bool IsJumping;
    private bool IsGrounded;
    private bool IsFalling;

    private const int LEFT = 0;
    private const int RIGHT = 1;

    private int Speed = 10;
    private GameObject PlayerGameObject;
    public GameObject WeaponMace;

	void Start ()
	{
        PlayerGameObject = GameObject.Find("Player");
        IsJumping = false;
        IsGrounded = true;
        IsRunning = false;
	}
	void Update () 
	{
        // Attach weapon to player
        float PlayerPos = PlayerGameObject.GetComponent<Rigidbody>().position.x;
        //WeaponMace.GetComponent<Rigidbody>().position.Set(PlayerGameObject.GetComponent<Rigidbody>().position.x,
           // PlayerGameObject.GetComponent<Rigidbody>().position.y,
           // PlayerGameObject.GetComponent<Rigidbody>().position.z);

         /*if(Player.GetComponent<Rigidbody>().velocity == new Vector3(0,0,0))
         {
             IsRunning = false;
         }*/
		if (Input.GetKeyDown("up")) 
		{
            if(!IsJumping)
            {
                Jump();
            }
		}
		else if (Input.GetKey ("left") && !IsJumping) 
		{
            Run(LEFT);
		}
		else if (Input.GetKey("right") && !IsJumping) 
		{
            Run(RIGHT);
		}
	}
    // Checking for collisions here?
    // Check for nearest objects for pickup?
    void OnCollisionEnter(Collision Collision)
    {
        PickupItem Potion;
        PlayerActions PlayerActions = (PlayerActions)GameObject.Find("Player").GetComponent(typeof(PlayerActions));
        if (Collision.gameObject == GameObject.Find("LevelGround"))
        {
            Debug.Log("Collision Detected: Ground");
            IsGrounded = true;
            IsJumping = false;
        } 
        else if (Collision.gameObject == GameObject.Find("ManaPotion"))
        {
            Debug.Log("Collision Detected: ManaPotion");
            Potion = GameObject.Find("ManaPotion").GetComponent<PickupItem>();
            PlayerActions.PickUpMana(Potion); 
        }
        else if (Collision.gameObject == GameObject.Find("HealthPotion"))
        {
            Debug.Log("Collision Detected: HealthPotion");
            Potion = GameObject.Find("HealthPotion").GetComponent<PickupItem>();
            PlayerActions.PickUpHealth(Potion); 
        }
    }
	protected void Jump()
	{
        Player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);  
       
        IsJumping = true;
        IsGrounded = false;
	}
    // Which force mode to use?
    // Should I just be translate the player instead?
    protected void Run(int Direction) 
    {
        if(Direction == LEFT)
        {
            Player.GetComponent<Rigidbody>().AddForce(new Vector3(-0.5f, 0, 0), ForceMode.VelocityChange);
            //Player.transform.Translate(-Time.deltaTime * Speed, 0, 0);
        }
        else if(Direction == RIGHT) 
        {
            Player.GetComponent<Rigidbody>().AddForce(new Vector3(0.5f, 0, 0), ForceMode.VelocityChange);
            //Player.transform.Translate(Time.deltaTime * Speed, 0, 0);
        }
        IsRunning = true;
    }
}
