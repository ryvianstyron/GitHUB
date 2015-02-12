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

    public Image EarthFallingBlockIcon;
    public Image EarthFloatingBlockIcon;
    public Image EarthBulletIcon;

    public Image EarthFallingBlockIconBACK;
    public Image EarthFloatingBlockIconBACK;
    public Image EarthBulletIconBACK;

    public GameObject PlayerGameObject;

	public Text Message;
    private Player Player;
    void Start()
    {
		Message.text = "";

		FireCollected.canvasRenderer.SetAlpha(0.0f);
		EarthCollected.canvasRenderer.SetAlpha (0.0f);

        EarthFallingBlockIconBACK.canvasRenderer.SetAlpha(0.0f);
        EarthFloatingBlockIconBACK.canvasRenderer.SetAlpha(0.0f);
        EarthBulletIconBACK.canvasRenderer.SetAlpha(0.0f);

        EarthFallingBlockIcon.canvasRenderer.SetAlpha(0.0f);
        EarthFloatingBlockIcon.canvasRenderer.SetAlpha(0.0f);
        EarthBulletIcon.canvasRenderer.SetAlpha(0.0f);

        Player = (Player)PlayerGameObject.GetComponent(typeof(Player));
        HealthImage.fillAmount = 1 * (Player.GetHealth() / Player.GetMaxHealth());
        ManaImage.fillAmount = 1 * (Player.GetMana() / Player.GetMaxMana());
    }
    void Update()
    {
        HealthImage.fillAmount = 1 * (Player.GetHealth() / Player.GetMaxHealth());
        ManaImage.fillAmount =  1 * (Player.GetMana() / Player.GetMaxMana());
    } 
    public void StartPowerCooldown(CoolDownIcon.Type Icon, float IconTimer)
    {
        CoolDownIconBehavior IconBehavior = null;
        switch(Icon)
        {
            case CoolDownIcon.Type.EarthBulletIcon:
                IconBehavior = EarthBulletIcon.GetComponent<CoolDownIconBehavior>();
                break;
            case CoolDownIcon.Type.EarthFallingBIcon:
                IconBehavior = EarthFallingBlockIcon.GetComponent<CoolDownIconBehavior>();
                break;
            case CoolDownIcon.Type.EarthGFloatingIcon:
                IconBehavior = EarthFloatingBlockIcon.GetComponent<CoolDownIconBehavior>();
                break;
        }
        if (IconBehavior != null)
        {
            IconBehavior.ActivateIcon();
            IconBehavior.PerformCoolDown(IconTimer);
        }
    }
	public void UpdatePowers()
	{
		List<Power> PlayerPowers = Player.GetPowersCollected();
		foreach(Power PW in PlayerPowers)
		{
			// Set Alpha to hidden
			UISetAlphaUsingPowerTag(PW.PowerTag, 0.0f);
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

            if(Alpha == 1.0f)
            {
                EarthFallingBlockIcon.canvasRenderer.SetAlpha(0.0f);
                EarthFloatingBlockIcon.canvasRenderer.SetAlpha(0.0f);
                EarthBulletIcon.canvasRenderer.SetAlpha(0.0f);

                EarthFallingBlockIconBACK.canvasRenderer.SetAlpha(0.0f);
                EarthFloatingBlockIconBACK.canvasRenderer.SetAlpha(0.0f);
                EarthBulletIconBACK.canvasRenderer.SetAlpha(0.0f);
            }
		}
		else if(Tag.Contains ("Earth"))
		{
			EarthCollected.canvasRenderer.SetAlpha(Alpha);
            if(Alpha == 1.0f)
            {
                EarthFallingBlockIcon.canvasRenderer.SetAlpha(0.5f);
                EarthFloatingBlockIcon.canvasRenderer.SetAlpha(0.5f);
                EarthBulletIcon.canvasRenderer.SetAlpha(0.5f);

                EarthFallingBlockIconBACK.canvasRenderer.SetAlpha(0.5f);
                EarthFloatingBlockIconBACK.canvasRenderer.SetAlpha(0.5f);
                EarthBulletIconBACK.canvasRenderer.SetAlpha(0.5f);
            }
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
