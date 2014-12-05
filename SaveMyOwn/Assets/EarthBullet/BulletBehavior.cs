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
        // Destroy bullet if it hits anything other than another player
        if (!(Collision.gameObject.name.Contains("Player"))) 
        {
            Destroy(gameObject);
        }
        else
        {
            if (Collision.gameObject.name.Contains("Player"))
            {
                Player = (Player)Collision.gameObject.GetComponent<Player>();
                if (Player.GetHealth() > 0)
                {
                    Player.SetHealth(Player.GetHealth() - 10);
                }
            }
        }
    }
}
