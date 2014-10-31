using UnityEngine;
using System.Collections;

public class Power : MonoBehaviour
{
    public bool IsPowerActivated;
    public int PowerType;
    public string PowerTag;

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
    public void PickUp()
    {
        Destroy(gameObject);
    }
    public override string ToString()
    {
        return 
            "IsPowerActivated:" + IsPowerActivated +
            "\nPowerType:" + PowerType + 
            "\nPowerTag:" + PowerTag;
    }
}

