using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    public string EnemyName;
    public int Type;
    public int Health;
	
    public void SetHealth(int Health)
    {
        this.Health = Health;
        Debug.Log("Enemy Health: " + this.Health);
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
}
