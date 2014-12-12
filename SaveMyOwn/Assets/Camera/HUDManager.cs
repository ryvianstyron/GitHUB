using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class HUDManager : MonoBehaviour 
{
    public Image HealthImage;
    public Image ManaImage;

	public Image FireCollected;
	public Image EarthCollected;

    public GameObject PlayerGameObject;

	public Text Message;
    private Player Player;
    void Start()
    {
		Message.text = "";
		FireCollected.canvasRenderer.SetAlpha(0.0f);
		EarthCollected.canvasRenderer.SetAlpha (0.0f);
        
        Player = (Player)PlayerGameObject.GetComponent(typeof(Player));
        HealthImage.fillAmount = 1 * (Player.GetHealth() / Player.GetMaxHealth());
        ManaImage.fillAmount = 1 * (Player.GetMana() / Player.GetMaxMana());
    }
    void Update()
    {
        HealthImage.fillAmount = 1 * (Player.GetHealth() / Player.GetMaxHealth());
        ManaImage.fillAmount =  1 * (Player.GetMana() / Player.GetMaxMana());
    } 
	public void UpdatePowers()
	{
		List<Power> PlayerPowers = Player.GetPowersCollected();
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
    public void DrawDamageTakenEffect()
    {

    }
    public void DrawLifeReplinishedEffect()
    {

    }
    public void DrawManaReplinishedEffect()
    {

    }
}
