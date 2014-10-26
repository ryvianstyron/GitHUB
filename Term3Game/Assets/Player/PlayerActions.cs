using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour 
{
	public void PickUpMana(PickupItem Mana)
    {
        Mana.ApplyPickupToPlayer();
    }
    public void PickUpHealth(PickupItem Health)
    {
        Health.ApplyPickupToPlayer();
    }
}
