using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanikaStatystyk : MonoBehaviour
{
    // Nazwa klucza dla PlayerPrefs
    private const string KilledUnitsKey = "KilledUnits";
    /// <summary> DO PRZYPISANIA NA ZGON WROGA 
    ///    private MechanikaStatystyk statManager;

    //void Start()
    //{
    // Pobierz referencj� do StatManagera
    // statManager = GameObject.FindObjectOfType<MechanikaStatystyk>();
    //}statManager.IncreaseKilledUnits();
    /// </summary>
    // Inicjalizacja
    void Start()
    {
        // Odczytaj ilo�� zabitych jednostek przy uruchamianiu gry
        int initialKilledUnits = PlayerPrefs.GetInt(KilledUnitsKey, 0);
        PlayerPrefs.SetInt(KilledUnitsKey, 0);
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
}
