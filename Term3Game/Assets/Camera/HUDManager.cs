using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour 
{
    public Text ManaText;
    public Text HealthText;
    public Text Debugger;

    private GameObject PlayerGameObject;
    private Player Player;
    void Start()
    {
        PlayerGameObject = GameObject.Find("Player");
        Player = (Player)PlayerGameObject.GetComponent(typeof(Player));

        HealthText.text = "Health: " + Player.GetHealth();
        ManaText.text = "Mana: " + Player.GetMana();
    }
    void Update()
    {
       
    }

    public void UpdateHealthBarOnScreen()
    {
        HealthText.text = "Health: " + Player.GetHealth();
    }
    public void UpdateManaBarOnScreen()
    {
        ManaText.text = "Mana: " + Player.GetMana();
    }
    public void DrawDamageTakenEffect()
    {

    }
    public void DrawLifeReplinishedEffect()
    {

    }
    public void DrawManaReplinishedEffect()
    {

    }
    public void OnScreenDebugLine(string DebugLine)
    {
        Debugger.text ="DEBUG:" + DebugLine;
    }
}
