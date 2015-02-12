using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour 
{
    HUDManager HUD;
    Player Player;
	void Start () 
    {
        HUD = (HUDManager)GameObject.Find("Camera").GetComponent<HUDManager>();
        StartCoroutine("DestroyBullet");
	}
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
	void Update () 
    {
	    
	}
    void OnCollisionEnter(Collision Collision)
    {
        if (Collision.gameObject.name.Contains("Enemy"))
        {
            HUD.Message.text = "Shot Enemy!";
            Enemy EnemyScript = (Enemy)Collision.gameObject.GetComponent<Enemy>();
            EnemyScript.SetHealth(EnemyScript.GetHealth() - 10);
            Destroy(Collision.gameObject);
        }
        if(Collision.gameObject.name.Contains("Earth_Block"))
        {
            Destroy(Collision.gameObject);
        }
        if (!(Collision.gameObject.name.Contains("Player"))) 
        {
            Destroy(gameObject);
        }
       
    }
}
