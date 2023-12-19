using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MechanikaEnergi : MonoBehaviour
{
    [SerializeField] private int ilosc_energi_Int;
    [SerializeField] private int Limit;
    [SerializeField] private int ilosc_energi_Startowa_Int;
    public  event EventHandler zmiana_ilosci_energi_event;
    public static MechanikaEnergi Instance { get;private set; }
    private void Awake()
    {
        Limit = 8;
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

    public void Dodaj_Energi(int ilosc)
    {
        if (!Czy_Jest_osi¹gnieto_limit())
        {
            ilosc_energi_Int += ilosc;
            SoundManager_game.Instance.PlaySound(SoundManager_game.Sound.DodanieEnergi);
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
            SoundManager_game.Instance.PlaySound(SoundManager_game.Sound.ZmianaElektryki);
            zmiana_ilosci_energi_event?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("osiagnieto limit mocy");
        }
    }
    public void ZwiekszMaxIloscEnergi()
    {
        ilosc_energi_Startowa_Int += 1;
        Dodaj_Energi(1);
    }
    public void ZmniejszMaxIloscEnergi()
    {
        ilosc_energi_Startowa_Int -= 1;
        Odejmij_Energi(1);
    }


}

