using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	private int Health = 20;
    private int MaxHealth = 100;

    private int CurrentLevel = 1;

    private int Mana = 20;
    private int MaxMana = 100;

    public Player() {}
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

	public override string ToString()
	{
		return 	"Health: " + this.GetHealth () +
			"\nMaxHealth: " + this.GetMaxHealth () + 
			"\nMana: " + this.GetMana () +
			"\nMaxMana: " + this.GetMaxMana () +
			"\nLevel: " + this.GetLevel ();
	}
}
