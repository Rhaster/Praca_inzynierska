using System;
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
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement(waypoints);
    }

    private void HandleMovement(Transform[] target)
    {
        if (currentWaypointIndex <= target.Length)
        {
            Transform currentWaypoint = target[currentWaypointIndex];

            float distanceToWaypoint = Vector3.Distance(transform.position, currentWaypoint.position);
            if (distanceToWaypoint > 0.1f)
            {
                Vector3 moveDir = (currentWaypoint.position - transform.position).normalized;
                rigidbody2d.velocity = moveDir * speed;
            }
            else
            {
                currentWaypointIndex++;

                if (currentWaypointIndex == target.Length)
                {
                    Destroy(gameObject); // dodac powiazanie z uszkodzeniem obiektu 
                }
            }
        }
        else
        {
            rigidbody2d.velocity = Vector2.zero;
        }
    }

}
