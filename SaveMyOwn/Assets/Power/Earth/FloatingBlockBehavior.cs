using UnityEngine;
using System.Collections;

public class FloatingBlockBehavior : MonoBehaviour 
{
    public float SizeToShrinkBy = 1.0f;
	public float StartWidth;
	void Start()
    {
		StartWidth = transform.localScale.x;
		//StartCoroutine("ShrinkBlock");
    }
    IEnumerator ShrinkBlock()
    {
		Debug.Log ("About to wait for 3 seconds");
        
		yield return new WaitForSeconds(3);
        float CurrentScale = gameObject.transform.localScale.x;

		Debug.Log ("SizeToShrinkBy: " + SizeToShrinkBy);
		Debug.Log("Current Scale:" + CurrentScale);
        
		if(!(CurrentScale <= SizeToShrinkBy))
        {
            Debug.Log("3 Seconds up, shrinking block");
			transform.localScale += new Vector3(-1.0f, 0, 0);
			//gameObject.transform.localScale.Set(CurrentScale -SizeToShrinkBy, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
			Debug.Log("About to wait for 2 seconds");
			yield return new WaitForSeconds(2);
            if(!(CurrentScale <= SizeToShrinkBy))
            {
                Debug.Log("5 Seconds up, shrinking block");
				transform.localScale += new Vector3(-1.0f, 0, 0);
				//gameObject.transform.localScale.Set(CurrentScale - SizeToShrinkBy, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
                yield return new WaitForSeconds(1);
                Debug.Log("6 Seconds up, destroying block");
                DestroyBlock();
            }
        }
    }
    void DestroyBlock()
    {
		DestroyObject(gameObject);
    }
	void Update()
	{
		if(transform.localScale.x > 0)
		{
			transform.localScale += new Vector3((0.2f) * -Time.deltaTime , 0, 0);
		}
		else
		{
			DestroyBlock();
		}
	}
}
