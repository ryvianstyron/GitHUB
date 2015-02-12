using UnityEngine;
using System.Collections;

public class EnemyActions : MonoBehaviour
{
    Player PlayerScript;
    EnemyClass EnemyScript;
    // Use this for initialization
    void Start()
    {
        // m_someOtherScriptOnAnotherGameObject = GameObject.FindObjectOfType(typeof(Player)) as Player;
        PlayerScript = GameObject.Find("Player").GetComponent<Player>();
        EnemyClass Enemy = GetComponent<EnemyClass>();
        PlayerScript.GetHealth();
        Enemy.GetDamage();

    }
    void Update()
    {

    }
    // Update is called once per frame

    void OnCollisionEnter(Collision Collision)
    {
        if (Collision.gameObject.name.Contains("Player"))
        {
            EnemyClass Enemy = (EnemyClass) GetComponent<EnemyClass>();
            if(PlayerScript)
            {
                if (Enemy)
                {
                    PlayerScript.ApplyDamage(Enemy.GetDamage());
                }
                else Debug.Log("Enemy is null");
            }
            else Debug.Log("Player is null");
        }
    }
}
