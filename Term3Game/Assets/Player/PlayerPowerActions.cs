using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPowerActions : MonoBehaviour 
{
    const int PRIMARY_ACTION = 1;
    const int SECONDRY_ACTION = 2;
    const int TERTIARY_ACTION = 3;
    const int FOURTH_ACTION = 4;

    const int EARTH = 1;
    const int FIRE = 2;

	Power CurrentPower;
	HUDManager HUD;

    Player Player;
	void Start () 
    {
		HUD = (HUDManager)GameObject.Find ("Camera").GetComponent(typeof(HUDManager));
        Player = (Player)GameObject.Find("Player").GetComponent(typeof(Player));
	}
    public void SetCurrentPower(Power Power)
    {
        CurrentPower = Power;
    }
    public Power GetCurrentPower()
    {
        return CurrentPower;
    }
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            //Debug.Log("Check Earth");
            CheckIfPowerIsCollected("Earth");
        }
        else if(Input.GetKeyDown("2"))
        {
            //Debug.Log("Check Fire");
            CheckIfPowerIsCollected("Fire");
        }
        else if (Input.GetKeyDown("a"))
        {
            //Debug.Log("Perform Primary");
            PerformPowerAction(PRIMARY_ACTION);
        }
        else if (Input.GetKeyDown("w"))
        {
            //Debug.Log("Perform Tertiary");
            PerformPowerAction(TERTIARY_ACTION);
        }
        else if (Input.GetKeyDown("d"))
        {
            //Debug.Log("Perform Secondry");
            PerformPowerAction(SECONDRY_ACTION);
        }
        else if(Input.GetKeyDown("s"))
        {
            //Debug.Log("Perform Fourth");
            PerformPowerAction(FOURTH_ACTION);
        }
    }
	public void DeactivateOtherPowers() // Uses Current Power
	{
        //Debug.Log("DeactivateOtherPowers: PowersCollectedCount:" + Player.GetPowersCollected().Count);
		foreach(Power PW in Player.GetPowersCollected ())
		{
          
			if(!(PW.PowerTag.Equals(CurrentPower.PowerTag)))
			{
				PW.IsPowerActivated = false;
			}
		}
		HUD.UpdatePowers();
	}
    public void CheckIfPowerIsCollected(string PowerTag)
    {
        List<Power> PowersCollected = Player.GetPowersCollected();
        int PowerExistsAt = Player.CheckIfPowerExists(PowerTag);
        if (PowerExistsAt != -1)
        {
            PowersCollected[PowerExistsAt].ActivatePower();
			CurrentPower = PowersCollected[PowerExistsAt];
		    DeactivateOtherPowers();
        }
        HUD.UpdatePowers();
    }
    public void PerformPowerAction(int Action)
    {
        switch (CurrentPower.GetPowerType())
        {
            case EARTH:
            Earth Earth = CurrentPower as Earth;
            switch (Action)
            {
                case PRIMARY_ACTION:
                    Earth.CreateFloatingBlock();
                    break;
                case SECONDRY_ACTION:
                    Earth.CreateFallingBlock();
                    break;
                case TERTIARY_ACTION:
                    Earth.ShootEarthProjecile();
                    break;
                case FOURTH_ACTION:
                    Earth.DestroyAllBlocks();
                    break;
            }
            break;
            case FIRE:
            Fire Fire = CurrentPower as Fire;
            switch (Action)
            {
                case PRIMARY_ACTION:
                    Fire.ActivateFireShield();
                    break;
                case SECONDRY_ACTION:
                    Fire.DeactivateFireShield();
                    break;
                case TERTIARY_ACTION:
                    Fire.ShootFireProjectile();
                    break;
                default:
                    break;
            }
            break;
        }
    }    
}
