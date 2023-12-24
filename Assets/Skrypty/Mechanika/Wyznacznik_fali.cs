using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wyznacznik_fali : MonoBehaviour 
{
    public static Wyznacznik_fali instance { get;private set; }
    float zmiennaFloat1;
    float zmiennaFloat2;
    float zmiennaFloat3;
    [SerializeField]private List<string> Sk³adFali;
    [SerializeField] private Lista_Wrogowie_SO wrog_lista;
    [SerializeField]private int ZczytanyPoziomTrudnosci_Int;
    private Dictionary<Amunicja_SO,float > amunicja_so_Dictionary;
    int poziom_trudnosci_int;
    public event EventHandler<Holder_fali> Wyznaczenie_fali_ui_event;
    public event EventHandler<Holder_pancerza> Wyznaczenie_pancerza_ui_event;
    private List<Amunicja_SO> amunicja_;
    public class Holder_fali
    {
        public List<string> wrogow_List;
    };
    public class Holder_pancerza
    {
        public Dictionary<Amunicja_SO, float> amunicja_Dictionary;
    };
    private void Awake()
    {
        
        instance= this;
        // Wczytanie poziomu trudnosci\
        ZczytanyPoziomTrudnosci_Int = LadowaniePlayerPrefs.GetDifficulty();
        wrog_lista = Resources.Load<Lista_Wrogowie_SO>("Wrogowie_Lista");
        poziom_trudnosci_int = LadowaniePlayerPrefs.GetDifficulty();
        amunicja_so_Dictionary = new Dictionary<Amunicja_SO, float>();
         amunicja_ = MechanikaAmunicji.Instance.lista().amunicja_Lista;
    }
    public List<string> ustalfale(float rozmiar, float budynkigracza)
    {
        Sk³adFali = new List<string>();
        for (int i=0;i<rozmiar;i++)
        {
            // Narazie random bo nie ma budynków gracza
            if(UnityEngine.Random.Range(0,2) ==1)
            {
                Sk³adFali.Add(wrog_lista.wrogowie_so_Lista[0].wrog_Nazwa);
            }
            else
            {
                Sk³adFali.Add(wrog_lista.wrogowie_so_Lista[1].wrog_Nazwa);
            }
        }
        Wyznaczenie_fali_ui_event?.Invoke(this, new Holder_fali { wrogow_List = Sk³adFali });
        ustalPancerz();
        return Sk³adFali;
    }
    public void ustalPancerz()
    {
        Debug.Log("poz trud:" + ZczytanyPoziomTrudnosci_Int.ToString());
        // SprawdŸ wartoœæ zmiennej
        if (ZczytanyPoziomTrudnosci_Int == 0)
        {
            zmiennaFloat1 = (float)System.Math.Round(Random.Range(0.7f, 1f),1);
            zmiennaFloat2 = (float)System.Math.Round(Random.Range(0.7f, 1f), 1);
            zmiennaFloat3 = (float)System.Math.Round(Random.Range(0.7f, 1f), 1);
        }
        else if (ZczytanyPoziomTrudnosci_Int == 1)
        {
            Debug.Log("Wartoœæ zmiennej jest równa 1.");
            // Losuj 3 zmienne typu float z zakresów od 0.5 do 1
            zmiennaFloat1 = (float)System.Math.Round(Random.Range(0.5f, 1f), 1);
            zmiennaFloat2 = (float)System.Math.Round(Random.Range(0.5f, 1f), 1);
            zmiennaFloat3 = (float)System.Math.Round(Random.Range(0.5f, 1f), 1);
        }
        else if (ZczytanyPoziomTrudnosci_Int == 2)
        {
            Debug.Log("Wartoœæ zmiennej jest równa 2.");
            // Losuj 3 zmienne typu float z zakresów od 0.3 do 8
            zmiennaFloat1 = (float)System.Math.Round(Random.Range(0.3f, 0.8f), 1);
            zmiennaFloat2 = (float)System.Math.Round(Random.Range(0.3f, 0.8f), 1);
            zmiennaFloat3 = (float)System.Math.Round(Random.Range(0.3f, 0.8f), 1);
        }
        // Wyœwietl wartoœci zmiennych
        Debug.Log("Zmienna float 1: " + zmiennaFloat1);
        Debug.Log("Zmienna float 2: " + zmiennaFloat2);
        Debug.Log("Zmienna float 3: " + zmiennaFloat3);
        amunicja_so_Dictionary[amunicja_[0]] = zmiennaFloat1;
        amunicja_so_Dictionary[amunicja_[1]] = zmiennaFloat2;
        amunicja_so_Dictionary[amunicja_[2]] = zmiennaFloat3;
        Wyznaczenie_pancerza_ui_event?.Invoke(this,new Holder_pancerza { amunicja_Dictionary= amunicja_so_Dictionary });
    }
    public Dictionary<Amunicja_SO,float> GetWartosciPancerza()
    {
        return amunicja_so_Dictionary;
    }
}


