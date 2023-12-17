using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour
{
    public Transform wroga_Transform;
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public float speed;
    public Rigidbody2D rigidbody2d;
    public Vector3 moveDir;

    private void Awake()
    {
        wroga_Transform = transform.Find("sprite");
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
            if (distanceToWaypoint > 0.4f)
            {
                moveDir = (currentWaypoint.position - transform.position).normalized;
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
            if (moveDir.x > 0.5)
            {

                PoruszanieWPrawo();
            }
            else if (moveDir.y > 0.5)
            {
              
                PoruszanieWGore();
            }
            else if (moveDir.y < -0.5)
            {
                PoruszanieWDol();
            }
            else if (moveDir.x < -0.5)
            {
                PoruszanieWLewo();
            }

        }
        else
        {
            rigidbody2d.velocity = Vector2.zero;
        }


        
    }

    public void PoruszanieWPrawo()
    {
        wroga_Transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void PoruszanieWGore()
    {
        wroga_Transform.rotation = Quaternion.Euler(0, 0, 90f);
    }
    public void PoruszanieWLewo()
    {
        wroga_Transform.rotation = Quaternion.Euler(0, 180f, 0f);
    }
    public void PoruszanieWDol()
    {
        wroga_Transform.rotation = Quaternion.Euler(0, 180f, -90f);
    }

}
