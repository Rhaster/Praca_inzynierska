using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanikaAmunicji : MonoBehaviour
{
    public static MechanikaAmunicji Instance { get; private set; }
    public event EventHandler ZmianaIlosciAmunicji;
    public List<StartowaIloscSur> kosztStworzeniaAmunicji;

    private Dictionary<Amunicja_SO, int> IloscAmunicji_slownik;

    private void Awake()
    {

        Instance = this;

        IloscAmunicji_slownik = new Dictionary<Amunicja_SO, int>();

        Lista_Amunicja_SO resourceTypeList = Resources.Load<Lista_Amunicja_SO>("Amunicja_Lista");

        foreach (Amunicja_SO resourceType in resourceTypeList.amunicja_Lista)
        {
            IloscAmunicji_slownik[resourceType] = 0;
        }

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

    public bool CzyStac(StartowaIloscSur[] resourceAmountArray)
    {
        foreach (StartowaIloscSur resourceAmount in resourceAmountArray)
        {
            if (true)
            {
                // Can afford
            }

        }

        // Can afford all
        return true;
    }

    public void WydajSurowce(StartowaIloscSur[] resourceAmountArray)
    {
        foreach (StartowaIloscSur resourceAmount in resourceAmountArray)
        {
            //IloscAmunicji_slownik[resourceAmount.surowiec] -= resourceAmount.ilosc;
        }
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

        foreach(Amunicja_SO x in IloscAmunicji_slownik.Keys)
        {
            Debug.Log(IloscAmunicji_slownik[x]);
        }
    }

}
