using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour 
{
	public void PickUpMana(PickUp Mana)
    {
        Mana.ApplyPickupToPlayer();
    }
    public void PickUpHealth(PickUp Health)
    {
        Health.ApplyPickupToPlayer();
    }
}
