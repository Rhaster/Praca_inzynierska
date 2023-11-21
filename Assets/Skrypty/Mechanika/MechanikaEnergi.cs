using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MechanikaEnergi : MonoBehaviour
{
    [SerializeField] private int ilosc_energi_Int;
    [SerializeField] private int ilosc_energi_Startowa_Int;
    public  event EventHandler zmiana_ilosci_energi_event;
    public static MechanikaEnergi Instance { get;private set; }
    private void Awake()
    {
        Instance = this;
        ilosc_energi_Int = ilosc_energi_Startowa_Int;
        zmiana_ilosci_energi_event?.Invoke(this, EventArgs.Empty);
    }
    public int Get_Obecna_ilosc_energi()
    {
        return ilosc_energi_Int;
    }
    public int Get_Maxymalna_ilosc_energi()
    {
        return ilosc_energi_Startowa_Int;
    }
    public float Get_Znormalizowana_ilosc_energi()
    {
        return (float)ilosc_energi_Int/ (float)ilosc_energi_Startowa_Int;
    }
    public Boolean Czy_Jest_energia()
    {
        return ilosc_energi_Int > 0;
    }
    public Boolean Czy_Jest_osi¹gnieto_limit()
    {
        return ilosc_energi_Int > ilosc_energi_Startowa_Int-1;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)) {
            Odejmij_Energi(1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Dodaj_Energi(1);
        }
    }
    public void Dodaj_Energi(int ilosc)
    {
        if (!Czy_Jest_osi¹gnieto_limit())
        {
            ilosc_energi_Int += ilosc;
            zmiana_ilosci_energi_event?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("osiagnieto limit mocy");
        }
    }
    public void Odejmij_Energi(int ilosc)
    {
        if (Czy_Jest_energia())
        {

            ilosc_energi_Int -= ilosc;
            zmiana_ilosci_energi_event?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("brak energi");
        }
    }

}

