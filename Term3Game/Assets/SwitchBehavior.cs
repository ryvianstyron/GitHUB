using UnityEngine;
using System.Collections;

public class SwitchBehavior : MonoBehaviour 
{
    private bool IsActive;
    public float WeightNeededForActivation;
    public GameObject ObjectThatThisSwitchActivates;

	// Use this for initialization
	void Start () 
    {
        IsActive = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	}
    public void ApplyBlockOnSwitch(Transform Block)
    {
        if(transform.rigidbody.mass >= WeightNeededForActivation)
        {
            IsActive = true;
            ToggleSwitch(Block);
        }
    }
    public void ToggleSwitch(Transform Block)
    {
        Transform Button = transform.GetChild(0);
        Button.position = new Vector3(Button.position.x, 0, Button.position.z);
        Block.rigidbody.isKinematic = false;

    }
}
