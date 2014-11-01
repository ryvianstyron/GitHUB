using UnityEngine;
using System.Collections;

public class EarthBlockBehavior : MonoBehaviour 
{
    public float SizeToShrinkBy = 1.0f;
    void Start()
    {
      StartCoroutine("ShrinkBlock");
    }
    IEnumerator ShrinkBlock()
    {
        yield return new WaitForSeconds(3);
        Vector3 CurrentScale = gameObject.transform.localScale;
        if(!(CurrentScale.x <= SizeToShrinkBy))
        {
            //Debug.Log("3 Seconds up, shrinking block");
            CurrentScale = new Vector3(CurrentScale.x - SizeToShrinkBy, CurrentScale.y, CurrentScale.z);
            yield return new WaitForSeconds(2);
            if(!(CurrentScale.x <= SizeToShrinkBy))
            {
                //Debug.Log("5 Seconds up, shrinking block");
                CurrentScale = new Vector3(CurrentScale.x - SizeToShrinkBy, CurrentScale.y, CurrentScale.z);
                yield return new WaitForSeconds(1);
                //Debug.Log("6 Seconds up, destroying block");
                DestroyBlock();
            }
        }
    }
    void DestroyBlock()
    {
        //Debug.Log("Block should be destroyed");
        //DestroyObject(gameObject);
    }
}
