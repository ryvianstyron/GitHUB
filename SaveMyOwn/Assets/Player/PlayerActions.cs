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
    /*public void PickupPower(Power Power)
    {
		Debug.Log ("Power.GetType() ->" + Power.GetType ());
		if(Power.GetType().Equals(Earth))
		{
        	Earth.PickUp();
        	Player.AddToPowerList(Power);
			PlayerPowerActions.SetCurrentPower(Power);
		}
		else if(Power.GetType().Equals(Fire))
		{
			Earth.PickUp();
			Player.AddToPowerList(Power);
			PlayerPowerActions.SetCurrentPower(Power);
		}
    }*/
	public void PickUpEarth(Earth Earth)
	{
		//Debug.Log ("PlayerActions PickUpEarth Earth Has:" + Earth.ToString());
        Earth.ActivatePower();
        PlayerPowerActions.SetCurrentPower(Earth);
        Player.AddToPowerList(Earth);
        PlayerPowerActions.DeactivateOtherPowers();
		Earth.PickUp ();
	}
	public void PickUpFire(Fire Fire)
	{
		//Debug.Log ("PlayerActions PickUpFire Fire Has: " + Fire.ToString());

        Fire.ActivatePower();
        PlayerPowerActions.SetCurrentPower(Fire);
        Player.AddToPowerList(Fire);
        PlayerPowerActions.DeactivateOtherPowers();
		Fire.PickUp ();
	}
}
