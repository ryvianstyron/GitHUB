using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour 
{
    int RangeInDistance;
    double TimeBetweenWaterProjectiles;
    const int MIN_TIME = 10;
    
    public void ShootWaterBeam()
    {
        if (TimeBetweenWaterProjectiles > MIN_TIME)
        {
            // shoot here
            TimeBetweenWaterProjectiles = 0;
        }
    }
    void FixedUpdate()
    {
        TimeBetweenWaterProjectiles += Time.deltaTime;
    }
}
