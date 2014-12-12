using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class Player : MonoBehaviour 
{
    public GameObject JumpPoint;

	private float Health = 80;
    private float MaxHealth = 100;

    private int CurrentLevel = 1;

    private float Mana = 65;
    private float MaxMana = 100;

    GameObject GameManager;
    List<Power> PickedUpPowers = new List<Power>();

    private CheckpointsHolder CheckpointsHolder;
    private CameraFollower CameraFollower;
    void Start()
    {
        CheckpointsHolder = (CheckpointsHolder)GameObject.Find("Camera").GetComponent<CheckpointsHolder>();
    }
    void Update()
    {
        if(Health == 0)
        {
            Vector3 SpawnAt = CheckpointsHolder.SpawnPlayerAtActiveCheckpoint();
            transform.position = SpawnAt;
            Health = 100;
            Mana = 100;
        }
    }
    public Player() 
    {
       
    }
    public void SetHealth(float Health)
	{
		this.Health = Health;
	}
    public float GetHealth()
	{
		return Health;
	}
    public void SetMana(float Mana)
	{
		this.Mana = Mana;
	}
    public float GetMana()
	{
		return Mana;
	}
    public void SetMaxHealth(float MaxHealth)
	{
		this.MaxHealth = MaxHealth;
	}
    public float GetMaxHealth()
	{
		return MaxHealth;
	}
    public void SetMaxMana(float MaxMana)
	{
		this.MaxMana = MaxMana;
	}
    public float GetMaxMana()
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
		//Debug.Log ("Player Class - Add To Power List");
        PickedUpPowers.Add(Power);
    }
    public List<Power> GetPowersCollected()
    {
        return PickedUpPowers;
    }
    public int CheckIfPowerExists(string PowerTag)
    {
        int ReturnIndex = -1;
        if (PickedUpPowers != null)
        {
            for(int i = 0; i < PickedUpPowers.Count; i++)
            {
                if(PickedUpPowers[i].GetPowerTag().Equals(PowerTag))
                {
                    ReturnIndex = i;
                }
            }
        }
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
