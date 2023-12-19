using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class MechanikaEkonomi : MonoBehaviour
{
    public static MechanikaEkonomi Instance { get; private set; }
    public event EventHandler ZmianaIlosciSurowcow;
    [SerializeField] private List<StartowaIloscSur> Lista_startowych_Surowcow;
    private int wydobyte;

    private Dictionary<Surowce_SO, int> IloscSurowcow_slownik;

    private void Awake()
    { 
      wydobyte= 0;
        Instance = this;

        IloscSurowcow_slownik = new Dictionary<Surowce_SO, int>();

        Lista_Surowce_SO resourceTypeList = Resources.Load<Lista_Surowce_SO>("Surowce_Lista");

        foreach (Surowce_SO resourceType in resourceTypeList.surowce_lista)
        {
            IloscSurowcow_slownik[resourceType] = 0;
        }

        foreach (StartowaIloscSur resourceAmount in Lista_startowych_Surowcow)
        {
            DodajSurowiec(resourceAmount.surowiec, resourceAmount.ilosc);
        }
    }


    private void Update()
    {
        
    }
    public void DodajSurowiec(Surowce_SO resourceType, int amount)
    {
        IloscSurowcow_slownik[resourceType] += amount;
        wydobyte += 1;
        ZmianaIlosciSurowcow?.Invoke(this, EventArgs.Empty);
    }

    public int GetIloscSurowca(Surowce_SO resourceType)
    {
        return IloscSurowcow_slownik[resourceType];
    }

    public bool CzyStac(List<StartowaIloscSur> resourceAmountArray)
    {
        foreach (StartowaIloscSur resourceAmount in resourceAmountArray)
        {
            if (GetIloscSurowca(resourceAmount.surowiec) >= resourceAmount.ilosc)
            {
                // Stac wiec nic nie robie
            }
            else
            {

                return false; // nie Stac wiec return false
            }
        }

        return true;
    }

    public void WydajSurowce(List<StartowaIloscSur> resourceAmountArray)
    {
        foreach (StartowaIloscSur resourceAmount in resourceAmountArray)
        {
            IloscSurowcow_slownik[resourceAmount.surowiec] -= resourceAmount.ilosc;
        }
        ZmianaIlosciSurowcow?.Invoke(this, EventArgs.Empty);
    }
    public void DodajSurowce(List<StartowaIloscSur> resourceAmountArray)
    {
        foreach (StartowaIloscSur resourceAmount in resourceAmountArray)
        {
            IloscSurowcow_slownik[resourceAmount.surowiec] += resourceAmount.ilosc;
        }
        ZmianaIlosciSurowcow?.Invoke(this, EventArgs.Empty);
    }
    public int wydobyte_sur()
    {
        return wydobyte;
    }
}
