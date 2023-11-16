using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public float speed;
    public Rigidbody2D rigidbody2d;
    private void Awake()
    {
        rigidbody2d= GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement(waypoints);
    }

    private void HandleMovement(Transform[] target)
    {
        if (currentWaypointIndex < target.Length)
        {
            Transform currentWaypoint = target[currentWaypointIndex];

            float distanceToWaypoint = Vector3.Distance(transform.position, currentWaypoint.position);

            if (distanceToWaypoint > 0.1f)
            {
                Vector3 moveDir = (currentWaypoint.position - transform.position).normalized;
                float moveSpeed = 6f;
                rigidbody2d.velocity = moveDir * moveSpeed;
            }
            else
            {
                currentWaypointIndex++;

                if (currentWaypointIndex == target.Length)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            rigidbody2d.velocity = Vector2.zero;
        }
    }

void MoveToWaypoint()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            // Poruszanie siê w kierunku bie¿¹cego waypointu
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, Time.deltaTime * speed);
            Debug.Log("ide do " + waypoints[currentWaypointIndex].position);
            // SprawdŸ, czy jednostka dotar³a do waypointu
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                currentWaypointIndex++;

                // Jeœli to ostatni waypoint, zniszcz jednostkê
                if (currentWaypointIndex == waypoints.Length)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
