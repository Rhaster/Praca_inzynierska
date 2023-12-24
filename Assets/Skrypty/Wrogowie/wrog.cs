using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wrog : MonoBehaviour
{
    public Dictionary<Amunicja_SO,float> amunicja_so;
    public static wrog Create(Vector3 position,string nazwa, Transform[] way ,float speed)
        {
            Transform pfEnemy = Resources.Load<Transform>(nazwa);
            pfEnemy.GetComponent<Pathing>().waypoints = way;  // ustalenie trasy dla danego przeciwnika 
            pfEnemy.GetComponent<Pathing>().speed= speed; // ustalenie predkosci danego przeciwnika 
            Transform enemyTransform = Instantiate(pfEnemy, way[0].position, Quaternion.identity);

            wrog enemy = enemyTransform.GetComponent<wrog>();
            return enemy;
        }
  
    private void Start()
    {
        amunicja_so = Wyznacznik_fali.instance.GetWartosciPancerza();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CEL"))
        {
            collision.GetComponent<SystemHP>().Damage(1);
        }
    }
    // Update is called once per frame
}
