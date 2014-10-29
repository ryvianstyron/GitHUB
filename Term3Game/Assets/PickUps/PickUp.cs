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
        int PlayerHealth = Player.GetHealth();
        int MaxPlayerHealth = Player.GetMaxHealth();

        int PlayerMana = Player.GetMana();
        int MaxPlayerMana = Player.GetMaxMana();

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
            HUD.UpdateHealthBarOnScreen();
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
            HUD.UpdateManaBarOnScreen();
        }
        Destroy(gameObject);
    }
}
