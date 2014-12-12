using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    public string EnemyName;
    public int Type;
    public int Health;
	public int Damage;
    public void SetHealth(int Health)
    {
        this.Health = Health;
    }
    public int GetHealth()
    {
        return Health;
    }
    void Update()
    {
        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }
	public int GetDamageInflicted()
	{
		return Damage;
	}
}
