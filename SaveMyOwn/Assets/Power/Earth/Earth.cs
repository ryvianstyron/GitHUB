using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Earth : Power 
{
    public GameObject EarthBullet;
    List<GameObject> EarthBlocksOnScreen;

    private bool CanCreateFloatingBlock;
    private bool CanCreateFallingBlock;
    private bool CanShootProjectile;

    //private int NumberOfBlocksOnScreen = 0;
    private int RangeInDistance;
    private int FacingDirection = -1;

    private const int LEFT = 0;
    private const int RIGHT = 1;

    private const float MIN_FLOATING_BLOCK_TIME = 2.0f;
    private const float MIN_FALLING_BLOCK_TIME = 7.0f;
    private const float MIN_PROJECTILE = 1.0f;

    private float RangeInAngleDegrees;
    private float RangeinAngleRadians;

    private float BLOCK_OFFSET_X = 8.0f;
    private float BLOCK_OFFSET_Y;
    private float BLOCK_OFFSET_Y_PLUS;

    private double TimeBetweenEarthProjectiles = MIN_PROJECTILE;
    private float TimeBetweenFloatingBlockCreations = MIN_FLOATING_BLOCK_TIME;
    private float TimeBetweenFallingBlockCreations = MIN_FALLING_BLOCK_TIME;
   
    private PlayerMovement Movement;
    private Player PlayerScript;

    public GameObject Player;
    public GameObject Earth_Block_Floating;
    public GameObject Earth_Block_Falling;
    
    private Vector3 PlayerCurrentPosition;
    private Vector3 BlockSpawnPoint;

	public HUDManager HUD;
    void Start()
    {
        BLOCK_OFFSET_Y = GameObject.FindGameObjectWithTag("Player").transform.localScale.y * 0.4f;
        BLOCK_OFFSET_Y_PLUS = GameObject.FindGameObjectWithTag("Player").transform.localScale.y * 2.0f;
		HUD = (HUDManager)GameObject.Find("Camera").GetComponent<HUDManager>();
        EarthBlocksOnScreen = new List<GameObject>();
		Movement = (PlayerMovement)Player.GetComponent(typeof(PlayerMovement));
        PlayerScript = Player.GetComponent<Player>();
    }
    // Probably can combine into one method
    public void CreateFallingBlock()
    {
        PlayerCurrentPosition = Player.transform.position;
        FacingDirection = Movement.GetPlayerDirection();
        if(CanCreateFallingBlock && PlayerScript.GetMana() > 10)
        {
            switch (FacingDirection)
            {
                case LEFT:
                    BlockSpawnPoint = new Vector3(PlayerCurrentPosition.x - BLOCK_OFFSET_X , PlayerCurrentPosition.y + BLOCK_OFFSET_Y_PLUS, PlayerCurrentPosition.z);
                    GameObject FallingLeftBlock = (GameObject)Instantiate(Earth_Block_Falling, BlockSpawnPoint, Quaternion.identity);
                    EarthBlocksOnScreen.Add(FallingLeftBlock);
                    TimeBetweenFallingBlockCreations = 0;
                    break;
                case RIGHT:
                    BlockSpawnPoint = new Vector3(PlayerCurrentPosition.x + BLOCK_OFFSET_X, PlayerCurrentPosition.y + BLOCK_OFFSET_Y_PLUS, PlayerCurrentPosition.z);
                    GameObject FallingRightBlock = (GameObject)Instantiate(Earth_Block_Falling, BlockSpawnPoint, Quaternion.identity);
                    EarthBlocksOnScreen.Add(FallingRightBlock);
                    TimeBetweenFallingBlockCreations = 0;
                    break;
            }
            PlayerScript.SetMana(PlayerScript.GetMana() - 10);
            HUD.StartPowerCooldown(CoolDownIcon.Type.EarthFallingBIcon, MIN_FALLING_BLOCK_TIME);
        }
    }
    public void CreateFloatingBlock()
    {
        PlayerCurrentPosition = Player.transform.position;
        FacingDirection = Movement.GetPlayerDirection();
        Vector3 BlockSpawnPoint;
        if (CanCreateFloatingBlock && PlayerScript.GetMana() > 5)
        {
            switch (FacingDirection)
            {
                case LEFT:
                    BlockSpawnPoint = new Vector3(PlayerCurrentPosition.x - (BLOCK_OFFSET_X + 3), PlayerCurrentPosition.y + BLOCK_OFFSET_Y, PlayerCurrentPosition.z);
                    GameObject FloatingLeftBlock = (GameObject)Instantiate(Earth_Block_Floating, BlockSpawnPoint, Quaternion.identity);
                    EarthBlocksOnScreen.Add(FloatingLeftBlock);
                    TimeBetweenFloatingBlockCreations = 0;
                    break;
                case RIGHT:
                    BlockSpawnPoint = new Vector3(PlayerCurrentPosition.x + (BLOCK_OFFSET_X + 3), PlayerCurrentPosition.y + BLOCK_OFFSET_Y, PlayerCurrentPosition.z);
                    GameObject FloatingRightBlock = (GameObject)Instantiate(Earth_Block_Floating, BlockSpawnPoint, Quaternion.identity);
                    EarthBlocksOnScreen.Add(FloatingRightBlock);
                    TimeBetweenFloatingBlockCreations = 0;
                    break;
            }
            PlayerScript.SetMana(PlayerScript.GetMana() - 5);
            HUD.StartPowerCooldown(CoolDownIcon.Type.EarthGFloatingIcon, MIN_FLOATING_BLOCK_TIME);
        }
    }
	public override void PickUp()
	{
		base.PickUp();
		HUD.UpdatePowers();
	}
    public void ShootEarthProjecile()
    {
        if(CanShootProjectile && PlayerScript.GetMana() > 3)
        {
            GameObject InstantiateBullet;
            Vector3 BulletSpawn;
            if (Player.GetComponent<PlayerMovement>().GetPlayerDirection() == RIGHT)
            {
                BulletSpawn = new Vector3(Player.transform.position.x + 1f, Player.transform.position.y + 1f, Player.transform.position.z);
                InstantiateBullet = (GameObject)Instantiate(EarthBullet, BulletSpawn, Quaternion.identity);
                InstantiateBullet.rigidbody.AddForce(EarthBullet.transform.up * 1500);
            }
            else if (Player.GetComponent<PlayerMovement>().GetPlayerDirection() == LEFT)
            {
                BulletSpawn = new Vector3(Player.transform.position.x - 1f, Player.transform.position.y + 1f, Player.transform.position.z);
                InstantiateBullet = (GameObject)Instantiate(EarthBullet, BulletSpawn, Quaternion.identity);
                InstantiateBullet.rigidbody.AddForce(-EarthBullet.transform.up * 1500);
            }
            TimeBetweenEarthProjectiles = 0;
            PlayerScript.SetMana(PlayerScript.GetMana() - 3);
            HUD.StartPowerCooldown(CoolDownIcon.Type.EarthBulletIcon, MIN_PROJECTILE);
        }
    }
    public void DestroyAllBlocks()
    {
        if(EarthBlocksOnScreen != null && EarthBlocksOnScreen.Count > 0)
        {
            foreach(GameObject EB in EarthBlocksOnScreen)
            {
                Destroy(EB);
            }
            EarthBlocksOnScreen.Clear();
        }
    }
    void Update()
    {
		TimeBetweenFloatingBlockCreations += Time.deltaTime;
        TimeBetweenFallingBlockCreations += Time.deltaTime;
        TimeBetweenEarthProjectiles += Time.deltaTime;

        // Floating Block Resets
		if (TimeBetweenFloatingBlockCreations >= MIN_FLOATING_BLOCK_TIME)
		{
			CanCreateFloatingBlock = true;
		}
		else
        {
            CanCreateFloatingBlock = false;
        }
        // Falling Blocks Reset
        if (TimeBetweenFallingBlockCreations >= MIN_FALLING_BLOCK_TIME)
        {
            CanCreateFallingBlock = true;
        }
        else
        {
            CanCreateFallingBlock = false;
        }
        // Projectiles Reset
        if (TimeBetweenEarthProjectiles >= MIN_PROJECTILE)
        {
            CanShootProjectile = true;
        }
        else
        {
            CanShootProjectile = false;
        }

    }
}
