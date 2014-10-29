using UnityEngine;
using System.Collections;

public class Earth : Power 
{
    double TimeBetweenEarthProjectiles;
    double TimeBetweenBlockCreations;
    int RangeInDistance;
    float RangeInAngleDegrees;
    float RangeinAngleRadians;
    const int MIN_BLOCKS = 10;
    const int MIN_PROJECTILE = 10;
    
    // Probably can combine into one method
    public void CreateFallingBlock()
    {
        if (TimeBetweenBlockCreations >= MIN_BLOCKS)
        {
            // Create
            TimeBetweenBlockCreations = 0;
        }
    }
    public void CreateFloatingBlock()
    {
        if (TimeBetweenBlockCreations >= MIN_BLOCKS)
        {
            // Create
            TimeBetweenBlockCreations = 0;
        }
    }
    public void ShootEarthProjecile()
    {
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
