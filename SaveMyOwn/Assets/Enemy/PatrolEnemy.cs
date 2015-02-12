using UnityEngine;
using System.Collections;

public class PatrolEnemy : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    private int currentPoint;
    public Transform target;
    public int rotationSpeed;
    public float maxDistance;
    private Transform myTransform;
    //public Transform RunHere;
    //public Transform NavPosition;
    //NavMeshAgent Agent;
    void Awake()
    {
        myTransform = transform;
    }
    void Start()
    {
        transform.position = patrolPoints[0].position;
        currentPoint = 0;
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;
        maxDistance = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, myTransform.position) < maxDistance)
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        }

        if (transform.position == patrolPoints[currentPoint].position)
        {

            currentPoint++;

        }
        if (currentPoint >= patrolPoints.Length)
        {
            currentPoint = 0;
        }

        //if (Vector3.Distance(target.position, NavPosition.position) < 1)
        //{
        //    Agent.SetDestination(RunHere.position);
        //}

        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);
    }
}
