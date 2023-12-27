using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.EventSystems;

public class MechanikaBudowania : MonoBehaviour
{
    public static MechanikaBudowania Instance { get; private set; }
    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public Wieze_SO aktywna_wieza_so;
    }

    private Wieze_SO aktywna_Wieza_so;

    private void Awake()
    {
        Instance = this;


    }

    public void SetActiveBuildingType(Wieze_SO buildingType)
    {
        aktywna_Wieza_so = buildingType;

        OnActiveBuildingTypeChanged?.Invoke(this,
            new OnActiveBuildingTypeChangedEventArgs { aktywna_wieza_so = aktywna_Wieza_so }
        );
    }

    public Wieze_SO GetActiveBuildingType()
    {
        return aktywna_Wieza_so;
    }

    private bool CanSpawnBuilding(Wieze_SO buildingType, Vector3 position, out string errorMessage)
    {
        errorMessage = "Nie stac";
        return true;
    }

 
}
