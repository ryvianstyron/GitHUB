using UnityEngine;
using System.Collections;

public class FallingBlockBehavior : MonoBehaviour 
{
    private const float SHRINK_BY = 0.5f;
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
                SHRINK_BY * -Time.deltaTime,
                SHRINK_BY * -Time.deltaTime,
                SHRINK_BY * -Time.deltaTime);
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
