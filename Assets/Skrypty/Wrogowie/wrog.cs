using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class wrog : MonoBehaviour
{
    public Dictionary<Amunicja_SO,float> amunicja_so;
    private static int poziom_trudnosci_int;
    public static wrog Stworz(string nazwa, Transform[] way ,float speed,int numerfali_Int,int poziomtrudnsci_int)
        {
            Transform pf_Wrog_Transform = Resources.Load<Transform>(nazwa);
            pf_Wrog_Transform.GetComponent<Pathing>().punkty_Transform = way;  // ustalenie trasy dla danego przeciwnika 
            pf_Wrog_Transform.GetComponent<Pathing>().predkosc_Float= speed; // ustalenie predkosci danego przeciwnika 
        
            pf_Wrog_Transform.GetComponent<SystemHP>().Set_maxymalne_hp(pf_Wrog_Transform.GetComponent<Wrog_holder>().wrog_Wrogowie_SO.liczba_hp_Int * (int)(1 +(poziomtrudnsci_int * ((float)numerfali_Int/5))),true);
            Transform wrog_Transform = Instantiate(pf_Wrog_Transform, way[0].position, Quaternion.identity);

            wrog wrog_Wrog = wrog_Transform.GetComponent<wrog>();
            return wrog_Wrog;
        }
    private void Awake()
    {
        poziom_trudnosci_int = LadowaniePlayerPrefs.GetDifficulty();
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
