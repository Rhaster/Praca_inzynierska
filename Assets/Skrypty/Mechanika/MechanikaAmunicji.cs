using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanikaAmunicji : MonoBehaviour
{
    public static MechanikaAmunicji Instance { get; private set; }
    public event EventHandler ZmianaIlosciAmunicji;
    public List<StartowaIloscSur> kosztStworzeniaAmunicji;
    private Lista_Amunicja_SO amunicja_List;
    private Dictionary<Amunicja_SO, int> IloscAmunicji_slownik;

    private void Awake()
    {

        Instance = this;

        IloscAmunicji_slownik = new Dictionary<Amunicja_SO, int>();

        amunicja_List = Resources.Load<Lista_Amunicja_SO>("Amunicja_Lista");
        List<Amunicja_SO> amunicja_ = amunicja_List.amunicja_Lista;
        foreach (Amunicja_SO resourceType in amunicja_)
        {
            IloscAmunicji_slownik[resourceType] = 0;
        }

    }
    public Lista_Amunicja_SO lista()
    {
        return amunicja_List;
    }

    public void DodajAmunicji(Amunicja_SO resourceType, int amount)
    {
        if (MechanikaEkonomi.Instance.CzyStac(kosztStworzeniaAmunicji)){
            IloscAmunicji_slownik[resourceType] += amount;
            MechanikaEkonomi.Instance.WydajSurowce(kosztStworzeniaAmunicji);
            ZmianaIlosciAmunicji?.Invoke(this, EventArgs.Empty);
        }
    }

    public int GetIloscAmunicji(Amunicja_SO resourceType)
    {
        return IloscAmunicji_slownik[resourceType];
    }



 
    public bool CzyStac_na_Strzal(Amunicja_SO resourceType)
    {
        if (IloscAmunicji_slownik[resourceType]>0)
        {
            return true;
        }
        return false;
    }
    public void strzel(Amunicja_SO resourceType,int ilosc)
    {
        if(ilosc>0)
        {
            IloscAmunicji_slownik[resourceType] -= ilosc;
            ZmianaIlosciAmunicji?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            IloscAmunicji_slownik[resourceType] += -ilosc;
            ZmianaIlosciAmunicji?.Invoke(this, EventArgs.Empty);
        }

       
    }
    private void UstawAmunicjeWszystkimWiezom(Amunicja_SO amunicja)
    {
        GameObject[] obiektyZTagiem = GameObject.FindGameObjectsWithTag("Wieza_Obszarowa");
        GameObject[] obiektyZTagiem1 = GameObject.FindGameObjectsWithTag("Wieza_Pojedyncza");
        foreach (GameObject obiekt in obiektyZTagiem)
        {
            obiekt.GetComponent<Wieza>().amunicja_Wybrana_Amunicja_SO = amunicja;
            obiekt.GetComponent<UI_Wieze_Zasieg_klikalnosc>().Ustaw_podswietlenie();
            obiekt.GetComponent<UI_Wieze_Zasieg_klikalnosc>().Wywolaj_event_zmiany_amunicji();
        }
        foreach (GameObject obiekt in obiektyZTagiem1)
        {
            obiekt.GetComponent<Wieza>().amunicja_Wybrana_Amunicja_SO = amunicja;
            obiekt.GetComponent<UI_Wieze_Zasieg_klikalnosc>().Ustaw_podswietlenie();
            obiekt.GetComponent<UI_Wieze_Zasieg_klikalnosc>().Wywolaj_event_zmiany_amunicji();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
          
            UstawAmunicjeWszystkimWiezom(amunicja_List.amunicja_Lista[0]);
        }

        // Sprawd�, czy klawisz F2 zosta� naci�ni�ty
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UstawAmunicjeWszystkimWiezom(amunicja_List.amunicja_Lista[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            UstawAmunicjeWszystkimWiezom(amunicja_List.amunicja_Lista[2]);
        }
    }
}
