using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.EventSystems;

public class MechanikaBudowania : MonoBehaviour
{
    public static MechanikaBudowania Instance { get; private set; }
    public event EventHandler<Holder_Typu_Budowli> Zmiana_aktywnego_typu_wiezy;

    public class Holder_Typu_Budowli : EventArgs
    {
        public Wieze_SO aktywna_wieza_so;
    }

    private Wieze_SO aktywna_Wieza_so;

    private void Awake()
    {
        Instance = this;


    }

    public void Ustaw_aktywny_typ_budowli(Wieze_SO buildingType)
    {
        aktywna_Wieza_so = buildingType;

        Zmiana_aktywnego_typu_wiezy?.Invoke(this,
            new Holder_Typu_Budowli { aktywna_wieza_so = aktywna_Wieza_so }
        );
    }

    public Wieze_SO Get_aktywny_budynek()
    {
        return aktywna_Wieza_so;
    }

 

 
}
