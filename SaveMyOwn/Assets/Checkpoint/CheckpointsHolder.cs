using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointsHolder : MonoBehaviour 
{
    public GameObject Checkpoint1;
    public GameObject Checkpoint2;
    public GameObject Checkpoint3;
	public GameObject Checkpoint4;
	public GameObject Checkpoint5;

    List<GameObject> Checkpoints = new List<GameObject>();
    Player Player;
	
    void Awake()
    {
        Checkpoints.Add(Checkpoint1);
        Checkpoints.Add(Checkpoint2);
        Checkpoints.Add(Checkpoint3);
		Checkpoints.Add(Checkpoint4);
		Checkpoints.Add(Checkpoint5);

        Player = GameObject.Find("Player").GetComponent<Player>();
	}
	void Update ()
    {
	
	}
    public Vector3 SpawnPlayerAtActiveCheckpoint()
    {
        Vector3 CheckpointToSpawnAt = Vector3.zero;
        foreach (GameObject CP in Checkpoints)
        {
            CheckpointBehavior CheckpointBehavior = (CheckpointBehavior)CP.GetComponent<CheckpointBehavior>();
            if (CheckpointBehavior.IsCheckpointActive())
            {
                CheckpointToSpawnAt = CP.transform.position;
                CheckpointToSpawnAt.x = CheckpointToSpawnAt.x + 2;
            }
        }
        // Make player go back to full life
        Player.SetHealth(50);
        return CheckpointToSpawnAt;
    }
    public void ActivateCheckPointWithNumber(int CheckpointNumber) // Deactivates the rest
    {
        foreach(GameObject CP in Checkpoints)
        {
            CheckpointBehavior CheckpointBehavior = (CheckpointBehavior)CP.GetComponent<CheckpointBehavior>();
            if(CheckpointBehavior.GetCheckpointNumber() == CheckpointNumber)
            {
                CheckpointBehavior.ActivateCheckpoint();
                CP.renderer.material.color = Color.red;
            }
            else
            {
                CheckpointBehavior.DeactivateCheckpoint();
                CP.renderer.material.color = Color.white;
            }
        }
    }
}
