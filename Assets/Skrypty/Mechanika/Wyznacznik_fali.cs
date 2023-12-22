using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wyznacznik_fali : MonoBehaviour 
{


    [SerializeField]private List<string> Sk�adFali;
    [SerializeField] private Lista_Wrogowie_SO wrog_lista;
    [SerializeField]private int ZczytanyPoziomTrudnosci_Int;
    private void Awake()
    {
        // Wczytanie poziomu trudnosci\
        ZczytanyPoziomTrudnosci_Int = LadowaniePlayerPrefs.GetDifficulty();
        wrog_lista = Resources.Load<Lista_Wrogowie_SO>("Wrogowie_Lista");
    }
    public List<string> ustalfale(float rozmiar, float budynkigracza)
    {
        Sk�adFali = new List<string>();
        for (int i=0;i<rozmiar;i++)
        {
            // Narazie random bo nie ma budynk�w gracza
            if(Random.Range(0,2) ==1)
            {
                Sk�adFali.Add(wrog_lista.wrogowie_so_Lista[0].wrog_Nazwa);
            }
            else
            {
                Sk�adFali.Add(wrog_lista.wrogowie_so_Lista[1].wrog_Nazwa);
            }
        }
        return Sk�adFali;
    }
}
