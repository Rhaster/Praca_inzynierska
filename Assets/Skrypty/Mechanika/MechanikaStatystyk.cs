using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanikaStatystyk : MonoBehaviour
{
    // Nazwa klucza dla PlayerPrefs
    public static MechanikaStatystyk instance;
    public int i = 1;
    private const string KilledUnitsKey = "KilledUnits";
    private const string postawione_Key = "POSTAWIONEBUDYNKI";
    private const string wydobyte_Key = "Wydobyte";
    private void Awake()
    {
        instance = this;
    }
 
    void Start()
    {
        // Odczytaj iloœæ zabitych jednostek przy uruchamianiu gry
        int initialKilledUnits = PlayerPrefs.GetInt(KilledUnitsKey, 0);
        PlayerPrefs.SetInt(KilledUnitsKey, 0);
        int initialBuild = PlayerPrefs.GetInt(postawione_Key, 0);
        PlayerPrefs.SetInt(postawione_Key, 0);
        int initialWydobyte = PlayerPrefs.GetInt(wydobyte_Key, 0);
        PlayerPrefs.SetInt(postawione_Key, 0);
        //Debug.Log("Initial Killed Units: " + initialKilledUnits);
    }

    // Funkcja do zwiêkszania liczby zabitych jednostek
    public void IncreaseKilledUnits()
    {
        // Odczytaj aktualn¹ wartoœæ
        int currentKilledUnits = PlayerPrefs.GetInt(KilledUnitsKey, 0);

        // Zwiêksz wartoœæ o 1
        currentKilledUnits++;

        // Zapisz now¹ wartoœæ
        PlayerPrefs.SetInt(KilledUnitsKey, currentKilledUnits);
        PlayerPrefs.Save(); // Wa¿ne: zapisz zmiany
        //Debug.Log("Killed Units: " + currentKilledUnits);
    }
    public void IncreaseBuilded()
    {
        // Odczytaj aktualn¹ wartoœæ
        int currentKilledUnits = PlayerPrefs.GetInt(postawione_Key, 0);

        // Zwiêksz wartoœæ o 1
        currentKilledUnits++;

        // Zapisz now¹ wartoœæ
        PlayerPrefs.SetInt(postawione_Key, currentKilledUnits);
        PlayerPrefs.Save(); // Wa¿ne: zapisz zmiany
        //Debug.Log("Killed Units: " + currentKilledUnits);
    }
  
}
