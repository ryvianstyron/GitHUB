using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour
{
    public Transform Target;

    public float Distance = 3.0f;
    public float Height = 3.0f;
    public float Damping = 5.0f;
    public float RotationDamping = 10.0f;
    
    public bool SmoothRotation = true;
    public bool LockRotation;

    void Update()
    {
        if(Target)
        {
            Vector3 Test = new Vector3(Target.position.x,Target.position.y,-Distance);
            //Vector3 WantedPosition = Target.TransformPoint(0, Height, -Distance);
            //transform.position = Vector3.Lerp(transform.position, WantedPosition, Time.deltaTime * Damping);
            transform.position = Test;
            /*if (SmoothRotation)
            {
                Quaternion WantedRotation = Quaternion.LookRotation(Target.position - transform.position, Target.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, WantedRotation, Time.deltaTime * RotationDamping);
            }
            else transform.LookAt(Target, Target.up);*/

            if (LockRotation)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
