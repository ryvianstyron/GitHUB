using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PickUp : MonoBehaviour 
{
    public int PickUpType;
    public int PickUpAmount;
    GameObject CameraGameObject;
    GameObject PlayerGameObject;
    Player Player;
    HUDManager HUD;

    void Start ()
    {
        CameraGameObject = GameObject.Find("Camera");
        PlayerGameObject = GameObject.Find("Player");

        Player = (Player)PlayerGameObject.GetComponent(typeof(Player));
        HUD = (HUDManager)CameraGameObject.GetComponent(typeof(HUDManager));
    }
    public void ApplyPickupToPlayer()
    {
        float PlayerHealth = Player.GetHealth();
        float MaxPlayerHealth = Player.GetMaxHealth();

        float PlayerMana = Player.GetMana();
        float MaxPlayerMana = Player.GetMaxMana();

        if(PickUpType == 0) // Health
        {
            if (PlayerHealth + PickUpAmount >= MaxPlayerHealth)
            {
                Player.SetHealth(MaxPlayerHealth);
            }
            else
            {
                Player.SetHealth(PlayerHealth + PickUpAmount);
            }
        }
        else if(PickUpType == 1) // Mana
        {
            if (PlayerMana + PickUpAmount >= MaxPlayerMana)
            {
                Player.SetMana(MaxPlayerMana);
            }
            else
            {
                Player.SetMana(PlayerMana + PickUpAmount);
            }
        }
        Destroy(gameObject);
    }
}
