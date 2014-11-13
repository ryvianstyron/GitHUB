using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class HUDManager : MonoBehaviour 
{
    public Text ManaText;
    public Text HealthText;
    public Text Debugger;

	public Image FireCollected;
	public Image EarthCollected;

    public GameObject PlayerGameObject;
    private Player Player;
    void Start()
    {
		FireCollected.canvasRenderer.SetAlpha(0.0f);
		EarthCollected.canvasRenderer.SetAlpha (0.0f);
	
        Player = (Player)PlayerGameObject.GetComponent(typeof(Player));

        HealthText.text = "Health: " + Player.GetHealth();
        ManaText.text = "Mana: " + Player.GetMana();
    }
    void Update()
    {
       
    }
	public void UpdatePowers()
	{
		List<Power> PlayerPowers = Player.GetPowersCollected();
		//Debug.Log("Update Powers in HUD: PlayerPowersCount: " + PlayerPowers.Count);
		foreach(Power PW in PlayerPowers)
		{
			// Set Alpha to collected
			UISetAlphaUsingPowerTag(PW.PowerTag, 0.5f);
			// If it's currently activated, set it to the foreground
			if(PW.IsPowerActivated)
			{
				UISetAlphaUsingPowerTag(PW.PowerTag, 1.0f);
			}
		}
	}
	public void UISetAlphaUsingPowerTag(string Tag, float Alpha)
	{
		//Debug.Log ("In UISetAlphaUsingPowerTag Tag: " + Tag + ", Alpha: " + Alpha);  
		if(Tag.Contains("Fire"))
		{
			FireCollected.canvasRenderer.SetAlpha(Alpha);
		}
		else if(Tag.Contains ("Earth"))
		{
			EarthCollected.canvasRenderer.SetAlpha(Alpha);
		}
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
