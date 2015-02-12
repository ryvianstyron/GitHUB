using UnityEngine;
using System.Collections;

public class EnemyClass : MonoBehaviour
{
    public GameObject Enemy;

    public string EnemyName = "BasicEarth";
    public int Type = 1;
    public int Health = 50;
    public int Damage = 10;
    
    public float speed = 1;



    private int CurrentLevel = 1;

    public void SetHealth(int Health)
    {
        this.Health = Health;
        Debug.Log("Enemy Health: " + this.Health);
    }

    public int GetHealth()
    {
        return Health;
    }
    public void SetDamage(int Damage)
    {
        this.Damage = Damage;
    }
    public int GetDamage()
    {
        return Damage;
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public float GetSpeed()
    {
        return speed;
    }
    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
