using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Power: MonoBehaviour 
{
    public bool IsPowerActivated;
    public int PowerType;
    public string PowerTag;
	void Start()
	{ 
	}
    public void ActivatePower()
    {
        IsPowerActivated = true;
    }
    public void DeactivatePower()
    {
        IsPowerActivated = false;
    }
    public void SetPowerType(int Type)
    {
        PowerType = Type;
    }
    public int GetPowerType()
    {
        return PowerType;
    }
    public void SetPowerTag(string PowerTag)
    {
        this.PowerTag = PowerTag;
    }
    public string GetPowerTag()
    {
        return PowerTag;
    }
	// Parents the power to the player, so it still remains active
    public virtual void PickUp()
    {
		//Debug.Log ("Power SuperClass Pickup");
		if(PowerTag.Equals("Fire"))
		{
			GameObject.Find("Power_Fire").GetComponent<MeshRenderer>().enabled = false;
			GameObject.Find ("Power_Fire").GetComponent<SphereCollider>().enabled = false;
		}
		else if(PowerTag.Equals("Earth"))
		{
			GameObject.Find("Power_Earth").GetComponent<MeshRenderer>().enabled = false;
			GameObject.Find ("Power_Earth").GetComponent<CapsuleCollider>().enabled = false;
		}

    }
    public override string ToString()
    {
        return 
            "IsPowerActivated:" + IsPowerActivated +
            "\nPowerType:" + PowerType + 
            "\nPowerTag:" + PowerTag;
    }
}

