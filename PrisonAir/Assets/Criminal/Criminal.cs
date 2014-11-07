using UnityEngine;
using System.Collections;

public class Criminal : MonoBehaviour 
{
    public int CriminalType;
    public int Weight;

    public void SetCriminalType(int CriminalType)
    {
        this.CriminalType = CriminalType;
    }
    public int GetCriminalType()
    {
        return CriminalType;
    }
    public int GetWeight()
    {
        return Weight;
    }
    public void SetWeight(int Weight)
    {
        this.Weight = Weight;
    }
}
