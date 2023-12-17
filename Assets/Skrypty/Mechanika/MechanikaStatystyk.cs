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
        // Odczytaj ilo�� zabitych jednostek przy uruchamianiu gry
        int initialKilledUnits = PlayerPrefs.GetInt(KilledUnitsKey, 0);
        PlayerPrefs.SetInt(KilledUnitsKey, 0);
        int initialBuild = PlayerPrefs.GetInt(postawione_Key, 0);
        PlayerPrefs.SetInt(postawione_Key, 0);
        int initialWydobyte = PlayerPrefs.GetInt(wydobyte_Key, 0);
        PlayerPrefs.SetInt(postawione_Key, 0);
        //Debug.Log("Initial Killed Units: " + initialKilledUnits);
    }

    // Funkcja do zwi�kszania liczby zabitych jednostek
    public void IncreaseKilledUnits()
    {
        // Odczytaj aktualn� warto��
        int currentKilledUnits = PlayerPrefs.GetInt(KilledUnitsKey, 0);

        // Zwi�ksz warto�� o 1
        currentKilledUnits++;

        // Zapisz now� warto��
        PlayerPrefs.SetInt(KilledUnitsKey, currentKilledUnits);
        PlayerPrefs.Save(); // Wa�ne: zapisz zmiany
        //Debug.Log("Killed Units: " + currentKilledUnits);
    }
    public void IncreaseBuilded()
    {
        // Odczytaj aktualn� warto��
        int currentKilledUnits = PlayerPrefs.GetInt(postawione_Key, 0);

        // Zwi�ksz warto�� o 1
        currentKilledUnits++;

        // Zapisz now� warto��
        PlayerPrefs.SetInt(postawione_Key, currentKilledUnits);
        PlayerPrefs.Save(); // Wa�ne: zapisz zmiany
        //Debug.Log("Killed Units: " + currentKilledUnits);
    }
  
}
