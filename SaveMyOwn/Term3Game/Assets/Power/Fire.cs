using UnityEngine;
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
    }
    public void DeactivateFireShield()
    {
        IsFireShieldActive = false;
    }
    public void ShootFireProjectile()
    {
        if(TimeBetweenFireProjectiles > MIN_TIME)
        {
            // Shoot from here
            TimeBetweenFireProjectiles = 0;
        }
    }
    void FixedUpdate()
    {
        TimeBetweenFireProjectiles += Time.deltaTime;
    }
}
