using UnityEngine;
using System.Collections;

public class Earth : Power 
{
    private const int LEFT = 0;
    private const int RIGHT = 1;
    double TimeBetweenEarthProjectiles;
    double TimeBetweenBlockCreations;
    int RangeInDistance;
    float RangeInAngleDegrees;
    float RangeinAngleRadians;
    const int MIN_BLOCKS = 10;
    const int MIN_PROJECTILE = 10;
    PlayerMovement Movement;
    GameObject Player;
    private int FacingDirection = -1;

    const float BLOCK_OFFSET_X = 2.0f;
    const float BLOCK_OFFSET_Y = 0.1f;

    GameObject Earth_Block;
    

    void Start()
    {
        Earth_Block = GameObject.Find("Earth_Block");
        Player = GameObject.Find("Player");
        Movement = (PlayerMovement)Player.GetComponent(typeof(PlayerMovement));
        
    }
    // Probably can combine into one method
    public void CreateFallingBlock()
    {
        Vector3 PlayerCurrentPosition = Player.transform.position;
        // 3. TimeBetween Blocks

        FacingDirection = Movement.GetPlayerDirection();
        Vector3 BlockSpawnPoint;
        switch(FacingDirection)
        {
            case LEFT: 
                    Debug.Log("Player Facing Left");
                    BlockSpawnPoint = new Vector3(PlayerCurrentPosition.x - BLOCK_OFFSET_X, PlayerCurrentPosition.y + BLOCK_OFFSET_Y, PlayerCurrentPosition.z);
                    Instantiate(Earth_Block, BlockSpawnPoint, Quaternion.identity);
                break;
            case RIGHT: 
                    Debug.Log("Player facing Right");
                    BlockSpawnPoint = new Vector3(PlayerCurrentPosition.x + BLOCK_OFFSET_X, PlayerCurrentPosition.y + BLOCK_OFFSET_Y, PlayerCurrentPosition.z);
                    Instantiate(Earth_Block, BlockSpawnPoint, Quaternion.identity);
                break;
            default:
                break;
        }

        if (TimeBetweenBlockCreations >= MIN_BLOCKS)
        {
            // Create
            TimeBetweenBlockCreations = 0;
        }
    }
    public void CreateFloatingBlock()
    {
        Debug.Log("Create a floating block");
        if (TimeBetweenBlockCreations >= MIN_BLOCKS)
        {
            // Create
            TimeBetweenBlockCreations = 0;
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
    void FixedUpdate()
    {
        TimeBetweenEarthProjectiles += Time.deltaTime;
        TimeBetweenBlockCreations += Time.deltaTime;
    }
}
