using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{
    public string Name;
    public int Type;
    public int DamageAmount;

	public void ApplyDamageToEnemy(Enemy Enemy)
    {
        Enemy.SetHealth(Enemy.GetHealth() - DamageAmount);   
    }
    void OnCollisionEnter(Collision Collison)
    {
        if (Collison.gameObject.tag == "BasicEnemy")
        {
            Debug.Log("OnCollisionEnter : Weapon Hit Enemy");
            ApplyDamageToEnemy((Enemy)GameObject.Find("BasicEnemy").GetComponent(typeof(Enemy)));
        }
    }
    void OnTriggerEnter(Collider Collison)
    {
        if (Collison.gameObject.tag == "BasicEnemy")
        {
            Debug.Log("OnTriggerEnter :Weapon Hit Enemy");
            ApplyDamageToEnemy((Enemy)GameObject.Find("BasicEnemy").GetComponent(typeof(Enemy)));
        }
    }
}
