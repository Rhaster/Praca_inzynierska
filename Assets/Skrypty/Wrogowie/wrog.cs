using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class wrog : MonoBehaviour
{
    public Dictionary<Amunicja_SO,float> amunicja_so;
    public static wrog Stworz(Vector3 position,string nazwa, Transform[] way ,float speed)
        {
            Transform pf_Wrog_Transform = Resources.Load<Transform>(nazwa);
            pf_Wrog_Transform.GetComponent<Pathing>().punkty_Transform = way;  // ustalenie trasy dla danego przeciwnika 
            pf_Wrog_Transform.GetComponent<Pathing>().predkosc_Float= speed; // ustalenie predkosci danego przeciwnika 
            Transform enemyTransform = Instantiate(pf_Wrog_Transform, way[0].position, Quaternion.identity);

            wrog wrog_Wrog = enemyTransform.GetComponent<wrog>();
            return wrog_Wrog;
        }
  
    private void Start()
    {
        amunicja_so = Wyznacznik_fali.instance.GetWartosciPancerza();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CEL"))
        {
            collision.GetComponent<SystemHP>().Obrazenia(1);
            if(GetComponent<MechanikaBossa>() != null)
            {
                collision.GetComponent<SystemHP>().Obrazenia(10000);
            }
        }
        
    }
    
    // Update is called once per frame
}
