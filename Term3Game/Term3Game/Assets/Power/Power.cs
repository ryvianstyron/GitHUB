using UnityEngine;
using System.Collections;

public class Power : MonoBehaviour
{
    bool IsPowerActivated;
    int PowerType;
    string PowerTag;

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
}

