using UnityEngine;
using System.Collections;

public class FallingBlockBehavior : MonoBehaviour 
{
    public float SizeToShrinkBy;
    private float StartWidth;
    private float StartHeight;
    private float StartLength;
	void Start () 
    {
        StartWidth = transform.localScale.x;
        StartHeight = transform.localScale.y;
        StartLength = transform.localScale.z;
	}
	// Update is called once per frame
	void Update () 
    {
	    if(transform.localScale.x > 0)
        {
            transform.localScale += new Vector3(
                SizeToShrinkBy * -Time.deltaTime,
                SizeToShrinkBy * -Time.deltaTime,
                SizeToShrinkBy * -Time.deltaTime);
        }
        else
        {
            DestroyBlock();
        }
	}
    public void OnCollisionEnter(Collision Collision)
    {
        if(Collision.gameObject.name.Contains("Switch"))
        {
            Debug.Log("Block Fell on Switch");
            GameObject Switch = Collision.gameObject;
            SwitchBehavior SwitchBehavior = Switch.GetComponent<SwitchBehavior>();
            SwitchBehavior.ApplyBlockOnSwitch(transform);
            transform.rigidbody.isKinematic = true;
        }
    }

    void DestroyBlock()
    {
        DestroyObject(gameObject);
    }
}
