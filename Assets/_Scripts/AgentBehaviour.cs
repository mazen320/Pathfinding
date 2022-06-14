using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehaviour : MonoBehaviour
{

    [SerializeField] float speed = 10f;
    public float AvoidSpeed = 200;
    const float distancetoChangeWaypoint = 5f;
    [SerializeField] Transform target;
    public PathMaker PathMaker;
    List<Transform> path { get { return PathMaker.Waypoints; } }

    int currentWaypoint = 0;
    Rigidbody rb;
    Sensor sensor;

    public float batteryLife = 100;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sensor = GetComponent<Sensor>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float avoid = sensor.Check();

        if (avoid == 0)
        {
            StandardSteer();
        }
        else
        {
            AvoidSteer(avoid);
        }
        Move();
        CheckWaypoint();
        sensor.Check();
    }

    void goToRecharge()
    {
        if (batteryLife < 80)
        {
        }
    }

    void Move()
    {
        rb.MovePosition(rb.position + (transform.forward * speed * Time.deltaTime));
        batteryLife -= Time.fixedDeltaTime;
    }
    void StandardSteer()
    {
        Vector3 targetDirection = path[currentWaypoint].position - rb.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, Time.fixedDeltaTime, 0);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
    void AvoidSteer(float avoid)
    {
        transform.RotateAround(transform.position, transform.up, AvoidSpeed * Time.fixedDeltaTime * avoid);
    }
    void CheckWaypoint()
    {

        if (Vector3.Distance(rb.position, path[currentWaypoint].position) < distancetoChangeWaypoint)
        {
            if (batteryLife < 95)
            {
                currentWaypoint = 10;
            }
            else
            {
            currentWaypoint++;
            if (currentWaypoint == path.Count)
                currentWaypoint = 0;
            }
        }


        Debug.Log(currentWaypoint);
        Debug.Log(batteryLife);

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward * 5));
    }
}
