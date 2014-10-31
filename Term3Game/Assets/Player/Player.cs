using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class Player : MonoBehaviour 
{
	private int Health = 20;
    private int MaxHealth = 100;

    private int CurrentLevel = 1;

    private int Mana = 20;
    private int MaxMana = 100;

    GameObject GameManager;
    List<Power> PickedUpPowers = new List<Power>();

    void Start()
    {

    }
    public Player() 
    {
       
    }
	public void SetHealth(int Health)
	{
		this.Health = Health;
	}
	public int GetHealth()
	{
		return Health;
	}
	public void SetMana(int Mana)
	{
		this.Mana = Mana;
	}
	public int GetMana()
	{
		return Mana;
	}
	public void SetMaxHealth(int MaxHealth)
	{
		this.MaxHealth = MaxHealth;
	}
	public int GetMaxHealth()
	{
		return MaxHealth;
	}
	public void SetMaxMana(int MaxMana)
	{
		this.MaxMana = MaxMana;
	}
	public int GetMaxMana()
	{
		return MaxMana;
	}
	public int GetLevel()
	{
		return CurrentLevel;
	}
	public void SetLevel(int Level)
	{
		this.CurrentLevel = Level;
	}
    public void AddToPowerList(Power Power)
    {
        Debug.Log("Player AddToPowerList");

        PickedUpPowers.Add(Power);
    }
    public List<Power> GetPowersCollected()
    {
        return PickedUpPowers;
    }
    public int CheckIfPowerExists(string PowerTag)
    {
        Debug.Log("Power Tag" + PowerTag);
        int ReturnIndex = -1;
        if (PickedUpPowers != null)
        {
            Debug.Log("PickedUpPowers has Powers");
            for(int i = 0; i < PickedUpPowers.Count; i++)
            {
                Debug.Log("PickedUpPowers [" + i + "]= " + PickedUpPowers[i].GetPowerTag());
                if(PickedUpPowers[i].GetPowerTag().Equals(PowerTag))
                {
                    Debug.Log("MATCH FOUND");
                    ReturnIndex = i;
                }
            }
        }
        Debug.Log("Return Index: " + ReturnIndex);
        return ReturnIndex;
    }
	public override string ToString()
	{
        string Powers = "";
        if (PickedUpPowers != null)
        {
            foreach (Power PW in PickedUpPowers)
            {
                Powers += PW.GetPowerTag() + "\t";
            }
        }
        else Powers = "None";
        
        return "Health: " + this.GetHealth() +
            "\nMaxHealth: " + this.GetMaxHealth() +
            "\nMana: " + this.GetMana() +
            "\nMaxMana: " + this.GetMaxMana() +
            "\nLevel: " + this.GetLevel() +
            "\nPowers:" + Powers;
	}
}
