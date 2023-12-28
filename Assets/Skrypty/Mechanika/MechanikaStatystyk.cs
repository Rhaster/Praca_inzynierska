using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanikaStatystyk : MonoBehaviour
{
    // Nazwa klucza dla PlayerPrefs
    public static MechanikaStatystyk instance;
    public int i = 1;
    private const string zabite_jedn_Key = "KilledUnits";
    private const string postawione_Key = "POSTAWIONEBUDYNKI";
    private const string wydobyte_Key = "Wydobyte";
    private void Awake()
    {
        instance = this;
    }
 
    void Start()
    {
        // Odczytaj ilo�� zabitych jednostek przy uruchamianiu gry i je wyzeruj
        int initialKilledUnits = PlayerPrefs.GetInt(zabite_jedn_Key, 0);
        PlayerPrefs.SetInt(zabite_jedn_Key, 0);
        int initialBuild = PlayerPrefs.GetInt(postawione_Key, 0);
        PlayerPrefs.SetInt(postawione_Key, 0);
        int initialWydobyte = PlayerPrefs.GetInt(wydobyte_Key, 0);
        PlayerPrefs.SetInt(wydobyte_Key, 0);

    }

    // Funkcja do zwi�kszania liczby zabitych jednostek
    public void IncreaseKilledUnits()
    {
        // Odczytaj aktualn� warto��
        int obecnie_zabite_jednostki_Int = PlayerPrefs.GetInt(zabite_jedn_Key, 0);

        // Zwi�ksz warto�� o 1
        obecnie_zabite_jednostki_Int++;

        // Zapisz now� warto��
        PlayerPrefs.SetInt(zabite_jedn_Key, obecnie_zabite_jednostki_Int);
        PlayerPrefs.Save(); // Wa�ne: zapisz zmiany

    }
    public void IncreaseBuilded()
    {
        // Odczytaj aktualn� warto��
        int obecnie_zab_jedn_Int = PlayerPrefs.GetInt(postawione_Key, 0);

        // Zwi�ksz warto�� o 1
        obecnie_zab_jedn_Int++;

        // Zapisz now� warto��
        PlayerPrefs.SetInt(postawione_Key, obecnie_zab_jedn_Int);
        PlayerPrefs.Save(); // Wa�ne: zapisz zmiany
 
    }
  
}
