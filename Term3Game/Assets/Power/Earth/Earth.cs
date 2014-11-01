using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Earth : Power 
{
    List<GameObject> EarthBlocksOnScreen;

    private bool CanCreateFloatingBlock;
    private bool CanCreateFallingBlock;
    private HUDManager HUD;

    //private int NumberOfBlocksOnScreen = 0;
    private int RangeInDistance;
    private int FacingDirection = -1;

    private const int LEFT = 0;
    private const int RIGHT = 1;

    private const float MIN_FLOATING_BLOCK_TIME = 10.0f;
    private const float MIN_FALLING_BLOCK_TIME = 10.0f;
    private const int MIN_PROJECTILE = 10;

    private float RangeInAngleDegrees;
    private float RangeinAngleRadians;

    private const float BLOCK_OFFSET_X = 2.0f;
    private const float BLOCK_OFFSET_Y = 0.1f;
    private const float BLOCK_OFFSET_Y_PLUS = 0.5f;

    private double TimeBetweenEarthProjectiles;
    private float TimeBetweenFloatingBlockCreations = MIN_FLOATING_BLOCK_TIME;
    private float TimeBetweenFallingBlockCreations = MIN_FALLING_BLOCK_TIME;
   
    private PlayerMovement Movement;

    private GameObject Player;
    private GameObject Earth_Block_Floating;
    private GameObject Earth_Block_Falling;
    
    private Vector3 PlayerCurrentPosition;
    private Vector3 BlockSpawnPoint;

    void Start()
    {
        HUD = GameObject.Find("Camera").GetComponent<HUDManager>();
        EarthBlocksOnScreen = new List<GameObject>();
        Earth_Block_Floating = GameObject.Find("Earth_Block_Floating");
        Earth_Block_Falling = GameObject.Find("Earth_Block_Falling");
        Player = GameObject.Find("Player");
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
                    Debug.Log("Player Facing Left");
                    BlockSpawnPoint = new Vector3(PlayerCurrentPosition.x - BLOCK_OFFSET_X, PlayerCurrentPosition.y + BLOCK_OFFSET_Y_PLUS, PlayerCurrentPosition.z);
                    GameObject FloatingLeftBlock = (GameObject)Instantiate(Earth_Block_Falling, BlockSpawnPoint, Quaternion.identity);
                    EarthBlocksOnScreen.Add(FloatingLeftBlock);
                    break;
                case RIGHT:
                    Debug.Log("Player facing Right");
                    BlockSpawnPoint = new Vector3(PlayerCurrentPosition.x + BLOCK_OFFSET_X, PlayerCurrentPosition.y + BLOCK_OFFSET_Y_PLUS, PlayerCurrentPosition.z);
                    GameObject FloatingRightBlock = (GameObject)Instantiate(Earth_Block_Falling, BlockSpawnPoint, Quaternion.identity);
                    EarthBlocksOnScreen.Add(FloatingRightBlock);
                    break;
            }
        }
    }
    public void CreateFloatingBlock()
    {
        PlayerCurrentPosition = Player.transform.position;
        FacingDirection = Movement.GetPlayerDirection();
        Vector3 BlockSpawnPoint;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        string output = " " + CanCreateFloatingBlock + " " + TimeBetweenFloatingBlockCreations;
        HUD.OnScreenDebugLine(output);
        Debug.Log(output);
        if (CanCreateFloatingBlock)
        {
            switch (FacingDirection)
            {
                case LEFT:
                    BlockSpawnPoint = new Vector3(PlayerCurrentPosition.x - BLOCK_OFFSET_X, PlayerCurrentPosition.y + BLOCK_OFFSET_Y, PlayerCurrentPosition.z);
                    GameObject FallingLeftBlock = (GameObject)Instantiate(Earth_Block_Floating, BlockSpawnPoint, Quaternion.identity);
                    EarthBlocksOnScreen.Add(FallingLeftBlock);
                    TimeBetweenFloatingBlockCreations = 0;
                    break;
                case RIGHT:
                    BlockSpawnPoint = new Vector3(PlayerCurrentPosition.x + BLOCK_OFFSET_X, PlayerCurrentPosition.y + BLOCK_OFFSET_Y, PlayerCurrentPosition.z);
                    GameObject FallingRightBlock = (GameObject)Instantiate(Earth_Block_Floating, BlockSpawnPoint, Quaternion.identity);
                    EarthBlocksOnScreen.Add(FallingRightBlock);
                    TimeBetweenFloatingBlockCreations = 0;
                    break;
            }
        }
    }
    public void ShootEarthProjecile()
    {
        Debug.Log("Shoot Earth Projectile");
        if(TimeBetweenEarthProjectiles >= MIN_PROJECTILE)
        {
            //Shoot 
            TimeBetweenEarthProjectiles = 0;
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
            Debug.Log("Destroyed all blocks in level");
        }
    }
    void FixedUpdate()
    {/////////////////////////////////////////////////////////////////////////////////////////////
        TimeBetweenEarthProjectiles += Time.deltaTime;
        TimeBetweenFloatingBlockCreations += Time.deltaTime;
        if (TimeBetweenFloatingBlockCreations >= MIN_FLOATING_BLOCK_TIME)
        {
            CanCreateFloatingBlock = true;
        }
        else CanCreateFloatingBlock = false;
        
        TimeBetweenFallingBlockCreations += Time.deltaTime;
        if (TimeBetweenFallingBlockCreations >= MIN_FALLING_BLOCK_TIME)
        {
            CanCreateFallingBlock = true;
        }
        else CanCreateFallingBlock = false;
    }
    void Update()
    {
       // not working somehow
    }
}
