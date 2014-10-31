using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour 
{
    Player Player;
    PlayerPowerActions PlayerPowerActions;
	void Start()
    {
        Player = (Player)GameObject.Find("Player").GetComponent(typeof(Player));
        PlayerPowerActions = (PlayerPowerActions)Player.GetComponent(typeof(PlayerPowerActions));
    }
    public void PickUpMana(PickUp Mana)
    {
        Mana.ApplyPickupToPlayer();
    }
    public void PickUpHealth(PickUp Health)
    {
        Health.ApplyPickupToPlayer();
    }
    public void PickupPower(Power Power)
    {
        Debug.Log("PlayerActions Pickup Power With Tag : " + Power.GetPowerTag());
        Power.PickUp();
        Player.AddToPowerList(Power);
        PlayerPowerActions.SetCurrentPower(Power);
    }
}
