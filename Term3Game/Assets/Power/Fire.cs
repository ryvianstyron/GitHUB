﻿using UnityEngine;
using System.Collections;

public class Fire : Power 
{
    int RangeInDistance;
    float RangeInAngelRadians;
    float RangeInAngleDegrees;
    double TimeBetweenFireProjectiles;
    bool IsFireShieldActive;
    const int MIN_TIME = 10;

    public void ActivateFireShield()
    {
        IsFireShieldActive = true;
        Debug.Log("Activated Fire Shield");
    }
    public void DeactivateFireShield()
    {
        IsFireShieldActive = false;
        Debug.Log("Deactivated Fire Shield");
    }
    public void ShootFireProjectile()
    {
        Debug.Log("Shoot Fire Projectile");
        if(TimeBetweenFireProjectiles > MIN_TIME)
        {
            TimeBetweenFireProjectiles = 0;
        }
    }
    void FixedUpdate()
    {
        TimeBetweenFireProjectiles += Time.deltaTime;
    }
}
