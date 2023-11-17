using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wyznacznik_fali : MonoBehaviour 
{

    private enum Wróg
    {
        Oczekiwanie,
        TworzenieFali,
    }
    [SerializeField]private List<string> Sk³adFali;
    [SerializeField] private Lista_Wrogowie_SO wrog_lista;
    private void Awake()
    {
        wrog_lista = Resources.Load<Lista_Wrogowie_SO>("Wrogowie_Lista");
    }
    public List<string> ustalfale(float rozmiar, float budynkigracza)
    {
        Sk³adFali = new List<string>();
        for (int i=0;i<rozmiar;i++)
        {
            // Narazie random bo nie ma budynków gracza
            if(Random.Range(0,2) ==1)
            {
                Sk³adFali.Add(wrog_lista.wrogowie_so_Lista[0].wrog_Nazwa);
            }
            else
            {
                Sk³adFali.Add(wrog_lista.wrogowie_so_Lista[1].wrog_Nazwa);
            }
        }
        return Sk³adFali;
    }
}
