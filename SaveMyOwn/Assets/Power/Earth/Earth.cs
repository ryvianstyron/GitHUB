using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Earth : Power 
{
    public GameObject EarthBullet;
    List<GameObject> EarthBlocksOnScreen;

    private bool CanCreateFloatingBlock;
    private bool CanCreateFallingBlock;

    //private int NumberOfBlocksOnScreen = 0;
    private int RangeInDistance;
    private int FacingDirection = -1;

    private const int LEFT = 0;
    private const int RIGHT = 1;

    private const float MIN_FLOATING_BLOCK_TIME = 2.0f;
    private const float MIN_FALLING_BLOCK_TIME = 7.0f;
    private const int MIN_PROJECTILE = 10;

    private float RangeInAngleDegrees;
    private float RangeinAngleRadians;

    private const float BLOCK_OFFSET_X = 2.8f;
    private const float BLOCK_OFFSET_Y = 0.1f;
    private const float BLOCK_OFFSET_Y_PLUS = 3.0f;

    private double TimeBetweenEarthProjectiles;
    private float TimeBetweenFloatingBlockCreations = MIN_FLOATING_BLOCK_TIME;
    private float TimeBetweenFallingBlockCreations = MIN_FALLING_BLOCK_TIME;
   
    private PlayerMovement Movement;

    public GameObject Player;
    public GameObject Earth_Block_Floating;
    public GameObject Earth_Block_Falling;
    
    private Vector3 PlayerCurrentPosition;
    private Vector3 BlockSpawnPoint;

	public HUDManager HUD;

    void Start()
    {
		HUD = (HUDManager)GameObject.Find("Camera").GetComponent<HUDManager>();
        EarthBlocksOnScreen = new List<GameObject>();
		Movement = (PlayerMovement)Player.GetComponent(typeof(PlayerMovement));
    }
    // Probably can combine into one method
    public void CreateFallingBlock()
    {
        PlayerCurrentPosition = Player.transform.position;
        FacingDirection = Movement.GetPlayerDirection();
        if(CanCreateFallingBlock)
        {
            switch (FacingDirection)
            {
                case LEFT:
                    BlockSpawnPoint = new Vector3(PlayerCurrentPosition.x - BLOCK_OFFSET_X , PlayerCurrentPosition.y + BLOCK_OFFSET_Y_PLUS, PlayerCurrentPosition.z);
                    GameObject FloatingLeftBlock = (GameObject)Instantiate(Earth_Block_Falling, BlockSpawnPoint, Quaternion.identity);
                    EarthBlocksOnScreen.Add(FloatingLeftBlock);
                    TimeBetweenFallingBlockCreations = 0;
                    break;
                case RIGHT:
                    BlockSpawnPoint = new Vector3(PlayerCurrentPosition.x + BLOCK_OFFSET_X, PlayerCurrentPosition.y + BLOCK_OFFSET_Y_PLUS, PlayerCurrentPosition.z);
                    GameObject FloatingRightBlock = (GameObject)Instantiate(Earth_Block_Falling, BlockSpawnPoint, Quaternion.identity);
                    EarthBlocksOnScreen.Add(FloatingRightBlock);
                    TimeBetweenFallingBlockCreations = 0;
                    break;
            }
        }
    }
    public void CreateFloatingBlock()
    {
        PlayerCurrentPosition = Player.transform.position;
        FacingDirection = Movement.GetPlayerDirection();
        Vector3 BlockSpawnPoint;
        if (CanCreateFloatingBlock)
        {
            switch (FacingDirection)
            {
                case LEFT:
                    BlockSpawnPoint = new Vector3(PlayerCurrentPosition.x - (BLOCK_OFFSET_X + 3), PlayerCurrentPosition.y + BLOCK_OFFSET_Y, PlayerCurrentPosition.z);
                    GameObject FallingLeftBlock = (GameObject)Instantiate(Earth_Block_Floating, BlockSpawnPoint, Quaternion.identity);
                    EarthBlocksOnScreen.Add(FallingLeftBlock);
                    TimeBetweenFloatingBlockCreations = 0;
                    break;
                case RIGHT:
                    BlockSpawnPoint = new Vector3(PlayerCurrentPosition.x + (BLOCK_OFFSET_X + 3), PlayerCurrentPosition.y + BLOCK_OFFSET_Y, PlayerCurrentPosition.z);
                    GameObject FallingRightBlock = (GameObject)Instantiate(Earth_Block_Floating, BlockSpawnPoint, Quaternion.identity);
                    EarthBlocksOnScreen.Add(FallingRightBlock);
                    TimeBetweenFloatingBlockCreations = 0;
                    break;
            }
        }
    }
	public override void PickUp()
	{
		//Debug.Log ("Earth Base Class Pickup");
		base.PickUp();
		HUD.UpdatePowers();
	}
    public void ShootEarthProjecile()
    {
        //if(TimeBetweenEarthProjectiles >= MIN_PROJECTILE)
        //{
            GameObject InstantiateBullet;
            Vector3 BulletSpawn;
            if (Player.GetComponent<PlayerMovement>().GetPlayerDirection() == RIGHT)
            {
                //if (EnergyHealthMeter.GetEnergyHealth() > 0.0f)
                //{
                BulletSpawn = new Vector3(Player.transform.position.x + 1f, Player.transform.position.y + 1f, Player.transform.position.z);
                    InstantiateBullet = (GameObject)Instantiate(EarthBullet, BulletSpawn, Quaternion.identity);
                    InstantiateBullet.rigidbody.AddForce(EarthBullet.transform.up * 1500);
                    //EnergyHealthMeter.UseEnergy(10);
                //}

            }
            else if (Player.GetComponent<PlayerMovement>().GetPlayerDirection() == LEFT)
            {
                //if (EnergyHealthMeter.GetEnergyHealth() > 0.0f)
                //{
                    BulletSpawn = new Vector3(Player.transform.position.x - 1f, Player.transform.position.y + 1f, Player.transform.position.z);
                    InstantiateBullet = (GameObject)Instantiate(EarthBullet, BulletSpawn, Quaternion.identity);
                    InstantiateBullet.rigidbody.AddForce(-EarthBullet.transform.up * 1500);
                    //EnergyHealthMeter.UseEnergy(10);
                //}
            }
            TimeBetweenEarthProjectiles = 0;
        //}
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
		//Debug.Log ("TimeBetweenFloatingBlockCreations:" + TimeBetweenFloatingBlockCreations);
		//Debug.Log ("MIN_FLOATING_BLOCK_TIME:" + MIN_FLOATING_BLOCK_TIME);
		TimeBetweenFloatingBlockCreations += Time.deltaTime;
        TimeBetweenFallingBlockCreations += Time.deltaTime;
        // Floating Block Resets
		if (TimeBetweenFloatingBlockCreations >= MIN_FLOATING_BLOCK_TIME)
		{
			//Debug.Log ("Can Create Floating Block set in Update()");
			CanCreateFloatingBlock = true;
		}
		else
        {
            CanCreateFloatingBlock = false;
        }
        // Falling Blocks Reset
        if (TimeBetweenFallingBlockCreations >= MIN_FALLING_BLOCK_TIME)
        {
            //Debug.Log ("Can Create Falling Block set in Update()");
            CanCreateFallingBlock = true;
        }
        else
        {
            CanCreateFallingBlock = false;
        }
    }
}
