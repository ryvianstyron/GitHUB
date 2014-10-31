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
        Debug.Log("Create a falling block");
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
