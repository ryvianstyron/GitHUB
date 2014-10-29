using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour 
{
    Power CurrentPower;
	public void PickUpMana(PickupItem Mana)
    {
        Mana.ApplyPickupToPlayer();
    }
    public void PickUpHealth(PickupItem Health)
    {
        Health.ApplyPickupToPlayer();
    }
    public void SwitchPower(Power Power)
    {
        CurrentPower = Power;
    }
    public Power GetCurrentPower()
    {
        return CurrentPower;
    }
    public void PickUpPower(Power Power)
    {
        CurrentPower = Power;
    }
    public void TakeDamage(int DamageAmount)
    {

    }
    public void ActivatePower(Power PowerToActivate)
    {
        PowerToActivate.ActivatePower();
    }
}
