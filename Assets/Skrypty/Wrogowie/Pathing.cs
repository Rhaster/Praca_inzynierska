using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour
{
    public Transform wroga_Transform;
    public Transform[] punkty_Transform;
    private int obecny_punkt_Indeks = 0;
    public float predkosc_Float;
    public Rigidbody2D rigidbody2d;
    public Vector3 kierunek_ruchu_Vector3;
    public Transform obecnyWaypoint_transorm;
    private void Awake()
    {
        wroga_Transform = transform.Find("sprite");
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement(punkty_Transform);
    }

    private void HandleMovement(Transform[] target)
    {
        if (obecny_punkt_Indeks <= target.Length)
        {
            obecnyWaypoint_transorm = target[obecny_punkt_Indeks];

            float odleglosc_od_waypointu_Float = Vector3.Distance(transform.position, obecnyWaypoint_transorm.position);
            if (odleglosc_od_waypointu_Float > 0.5f)
            {
                kierunek_ruchu_Vector3 = (obecnyWaypoint_transorm.position - transform.position).normalized;
                
                rigidbody2d.velocity = kierunek_ruchu_Vector3 * predkosc_Float;
            }
            else
            {
                obecny_punkt_Indeks++;

                if (obecny_punkt_Indeks == target.Length)
                {
                    Destroy(gameObject);                }
            }
            if (kierunek_ruchu_Vector3.x > 0.5)
            {

                PoruszanieWPrawo();
            }
            else if (kierunek_ruchu_Vector3.y > 0.5)
            {
              
                PoruszanieWGore();
            }
            else if (kierunek_ruchu_Vector3.y < -0.5)
            {
                PoruszanieWDol();
            }
            else if (kierunek_ruchu_Vector3.x < -0.5)
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
