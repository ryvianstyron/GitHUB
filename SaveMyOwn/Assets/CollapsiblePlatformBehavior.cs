using UnityEngine;
using System.Collections;

/* Simulates collapsing platform movement*/
public class CollapsiblePlatformBehavior : MonoBehaviour 
{
    enum Mode 
    { 
        POS = 1, 
        NEG =-1
    };

    private bool Activated = false;
    private bool Shrinking = false;
    private bool Falling = false;

    private Mode ActiveMode = Mode.POS;
    private float Timer = 0f;
    public float HowLongBeforeFalls = 100.0f;

    private float Distance = 1.0f;
    private float SizeX;

    private int UpDownCounter = 0;
    
    void Start()
    {
        SizeX = transform.localScale.x;
    }
    void OnCollisionEnter(Collision Collision)
    {
        if(Collision.gameObject.name.Contains("Player") && !Activated)
        {
            Activated = true;
        }
    }
    void Update()
    {
        if(Activated)
        {
            if(Timer > 0.6f)
            {
                if (ActiveMode == Mode.POS)
                {
                    ActiveMode = Mode.NEG;
                }
                else
                {
                    ActiveMode = Mode.POS;
                }
                Timer = 0f;
            }
            Timer += Time.deltaTime;
            gameObject.transform.Translate(Vector3.down * (int)ActiveMode * Time.smoothDeltaTime * Distance); // Do we want it to move up and down or just blink?
            Blink();
            UpDownCounter++;
            if (UpDownCounter > HowLongBeforeFalls) 
            {
                Activated = false;
                Shrinking = true;
                Timer = 0.0f;
            }
        }
        // Do we want it to shrink?
        if(Shrinking)
        {
            if (gameObject.transform.localScale.x > 0)
            {
                Timer += Time.deltaTime;
                gameObject.transform.localScale += new Vector3((2.0f) * -Time.deltaTime, 0, 0);
                Blink();
                if(Timer > 0.2f)
                {
                    if (ActiveMode == Mode.POS)
                    {
                        ActiveMode = Mode.NEG;
                    }
                    else
                    {
                        ActiveMode = Mode.POS;
                    }
                    Timer = 0.0f;
                }
                if(gameObject.transform.localScale.x < (SizeX * 0.5f) && Shrinking)
                {
                    // Make it fall
                    Falling = true;
                    Shrinking = false;
                    if(gameObject.rigidbody == null)
                    {
                        gameObject.AddComponent<Rigidbody>();
                    }
                }
            }
        }
        if(Falling)
        {
            Timer += Time.deltaTime;
            //Debug.Log("Falling:" + Timer);
            if(Timer > 3.0f) 
            {
                Destroy(gameObject);
            }
        }
    }
    public void Blink()
    {
        if (ActiveMode == Mode.NEG)
        {
            gameObject.renderer.material.color = Color.white;
        }
        else
        {
            gameObject.renderer.material.color = Color.red;
        }
    }
}
