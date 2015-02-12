using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoolDownIconBehavior : MonoBehaviour 
{
    public Image CoolDownImage;
    private bool Activate = false;

    private float FullTimer = 0.0f;
    private float TimerTracker = 0.0f;

	// Use this for initialization
	void Start () 
    {
        CoolDownImage.canvasRenderer.SetAlpha(0.0f);
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Activate)
        {
            if(!(TimerTracker <= 0.0f))
            {
                TimerTracker -= Time.deltaTime;
                CoolDownImage.fillAmount = 1 * (TimerTracker / FullTimer);
            }
            else
            {
                CoolDownImage.canvasRenderer.SetAlpha(0.5f);
                CoolDownImage.fillAmount = 1;
                TimerTracker = 0.0f;
                FullTimer = 0.0f;
                Activate = false;
            }
        }
	}
    public void ActivateIcon()
    {
        CoolDownImage.canvasRenderer.SetAlpha(1.0f);
        Activate = true;
    }
    public void PerformCoolDown(float MinTimer)
    {
        TimerTracker = MinTimer;
        FullTimer = MinTimer;
        Activate = true;
    }
}
