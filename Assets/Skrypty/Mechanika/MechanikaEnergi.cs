using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MechanikaEnergi : MonoBehaviour
{
    [SerializeField] private int ilosc_energi_Int;
    public static event EventHandler zmiana_ilosci_energi_event;
    public static MechanikaEnergi Instance { get;private set; }
    private void Awake()
    {
        Instance= this;
    }
    public int Get_Obecna_ilosc_energi()
    {
        return ilosc_energi_Int;
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
        ilosc_energi_Int +=ilosc;
        zmiana_ilosci_energi_event?.Invoke(this, EventArgs.Empty);
    }
    public void Odejmij_Energi(int ilosc)
    {
        ilosc_energi_Int -= ilosc;
        zmiana_ilosci_energi_event?.Invoke(this, EventArgs.Empty);
    }
}
