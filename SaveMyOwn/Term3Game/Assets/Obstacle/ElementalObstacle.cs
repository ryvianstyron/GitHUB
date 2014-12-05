using UnityEngine;
using System.Collections;

public class ElementalObstacle : MonoBehaviour 
{
    Power ElementalObstacleType;
    int ElementalObstacleHealth;
    string ElementalObstacleTag;
    bool ElementalObstacleIsActive;

    public int GetHealth()
    {
        return ElementalObstacleHealth;
    }
    public void TakeDamage(int DamageBy)
    {
        if (ElementalObstacleHealth <= DamageBy)
        {
            ElementalObstacleHealth = 0;
        }
        else ElementalObstacleHealth -= DamageBy;
    }
    void Update()
    {
        if(ElementalObstacleHealth == 0)
        {
            RemoveElementalObstacleFromGameView();
        }
    }
    public void RemoveElementalObstacleFromGameView()
    {
        // Destroy the elemental obtacle
    }
}
