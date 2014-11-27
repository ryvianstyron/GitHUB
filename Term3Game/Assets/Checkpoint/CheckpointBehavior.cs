using UnityEngine;
using System.Collections;

public class CheckpointBehavior : MonoBehaviour 
{
    public int CheckpointNumber;
    private bool IsActive;

    public int GetCheckpointNumber()
    {
        return CheckpointNumber;
    }
    public void ActivateCheckpoint()
    {
        IsActive = true;
    }
    public void DeactivateCheckpoint()
    {
        IsActive = false;
    }
    public bool IsCheckpointActive()
    {
        return IsActive;
    }
    void Start()
    {
        IsActive = false;
    }
	void Update () 
    {
	
	}
}
