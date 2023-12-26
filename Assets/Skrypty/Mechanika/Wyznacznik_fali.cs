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
    [SerializeField]private List<string> zawartosc_fali_List;
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
        // Wczytanie poziomu trudnosci
        ZczytanyPoziomTrudnosci_Int = LadowaniePlayerPrefs.GetDifficulty();
        wrog_lista = Resources.Load<Lista_Wrogowie_SO>("Wrogowie_Lista");
        poziom_trudnosci_int = LadowaniePlayerPrefs.GetDifficulty();
        amunicja_so_Dictionary = new Dictionary<Amunicja_SO, float>();
         amunicja_ = MechanikaAmunicji.Instance.lista().amunicja_Lista;
    }
    public List<string> ustalfale(float rozmiar,bool czyboss)
    {
        #region poziom trudnosci latwy
        if (ZczytanyPoziomTrudnosci_Int == 0)
        {
            int ograniczenia_Analizatorow_Int = 6;
            int ograniczenia_Niszczycieli_Int = 3;
            int ograniczenia_Mechanidow_Int = 2;
            int ograniczenia_Pancernikow_Int = 1;
            if (czyboss == false)
            {
                zawartosc_fali_List = new List<string>(); // 0  niszcz // 1 mechanid // 2 pancernik // 3 analizator
                for (int i = 0; i < rozmiar; i++)
                {

                    int holder = UnityEngine.Random.Range(0, 4);
                    List<int> wynik = ustal_budynki_gracza();
                    int totalna_liczba_wiez_Int = wynik[0] + wynik[1];
                    int wynik_odejmowania_wiez_Int = wynik[0] - wynik[1];
                    if (wynik_odejmowania_wiez_Int == 0) // jesli  nie ma wiez lub równa ich ilosc to randomowo 
                    {
                        if (holder == 2)
                        {
                            for (int a = 0; a < ograniczenia_Niszczycieli_Int; a++)
                            {

                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[0].wrog_Nazwa);

                            }
                        }
                        else if (holder == 1)
                        {   
                            for (int a = 0; a < ograniczenia_Mechanidow_Int; a++)
                            {

                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[1].wrog_Nazwa);

                            }

                        }
                        else if (holder == 0)
                        {
                            for (int a = 0; a < ograniczenia_Pancernikow_Int; a++)
                            {
                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[2].wrog_Nazwa);
                            }
                        }
                        else if (holder == 3)
                        {
                            for (int a = 0; a < ograniczenia_Analizatorow_Int; a++)
                            {
                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[3].wrog_Nazwa);
                            }
                        }
                    }
                    else if(  wynik_odejmowania_wiez_Int>0)// jesli  jest przewaga wiez obszarowych
                    {
                        
                        float waga = wynik_odejmowania_wiez_Int / totalna_liczba_wiez_Int;
                        int wynik_Kalkulacja_Int = (int)Mathf.Clamp((float)(holder - waga), 0, 3);
                        Debug.Log("waga " + waga);
                        Debug.Log("wynik odejmowania " + wynik_odejmowania_wiez_Int);
                        Debug.Log("wartosc po kalkulacjach" + wynik_Kalkulacja_Int);
                        if (wynik_Kalkulacja_Int == 2)
                        {
                            for (int a = 0; a < ograniczenia_Niszczycieli_Int; a++)
                            {

                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[0].wrog_Nazwa);

                            }
                        }
                        else if (wynik_Kalkulacja_Int == 1)
                        {
                            for (int a = 0; a < ograniczenia_Mechanidow_Int; a++)
                            {

                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[1].wrog_Nazwa);

                            }

                        }
                        else if (wynik_Kalkulacja_Int == 0)
                        {
                            for (int a = 0; a < ograniczenia_Pancernikow_Int; a++)
                            {
                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[2].wrog_Nazwa);
                            }
                        }
                        else if (wynik_Kalkulacja_Int == 3)
                        {
                            for (int a = 0; a < ograniczenia_Analizatorow_Int; a++)
                            {
                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[3].wrog_Nazwa);
                            }
                        }

                    }
                    else // jesli  jest przewaga wiez single target 
                    {
                        float waga = wynik_odejmowania_wiez_Int / totalna_liczba_wiez_Int;
                        int wynik_Kalkulacja_Int = (int)Mathf.Clamp((float)(holder - waga), 0, 3);
                        Debug.Log("waga " + waga);
                        Debug.Log("wynik odejmowania " + wynik_odejmowania_wiez_Int);
                        Debug.Log("wartosc po kalkulacjach" + wynik_Kalkulacja_Int);
                        if (wynik_Kalkulacja_Int == 2)
                        {
                            for (int a = 0; a < ograniczenia_Niszczycieli_Int; a++)
                            {

                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[0].wrog_Nazwa);

                            }
                        }
                        else if (wynik_Kalkulacja_Int == 1)
                        {
                            for (int a = 0; a < ograniczenia_Mechanidow_Int; a++)
                            {

                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[1].wrog_Nazwa);

                            }

                        }
                        else if (wynik_Kalkulacja_Int == 0)
                        {
                            for (int a = 0; a < ograniczenia_Pancernikow_Int; a++)
                            {
                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[2].wrog_Nazwa);
                            }
                        }
                        else if (wynik_Kalkulacja_Int == 3)
                        {
                            for (int a = 0; a < ograniczenia_Analizatorow_Int; a++)
                            {
                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[3].wrog_Nazwa);
                            }
                        }
                    }

                    
                }
            }
            else
            {
                zawartosc_fali_List = new List<string>();
                int holder = UnityEngine.Random.Range(0, 3);
                if (holder == 1)
                {
                    zawartosc_fali_List.Add("Mapa1");
                }
                else
                {
                    zawartosc_fali_List.Add("Mapa1");
                }
            }
        }
        #endregion
        #region poziom trudnosci sredni
        else if (ZczytanyPoziomTrudnosci_Int == 1)
        {
            int ograniczenia_Analizatorow_Int = 9;
            int ograniczenia_Niszczycieli_Int = 4;
            int ograniczenia_Mechanidow_Int = 3;
            int ograniczenia_Pancernikow_Int = 2;
            if (czyboss == false)
            {
                zawartosc_fali_List = new List<string>(); // 0  niszcz // 1 mechanid // 2 pancernik // 3 analizator
                for (int i = 0; i < rozmiar; i++)
                {

                    int holder = UnityEngine.Random.Range(0, 4);
                    List<int> wynik = ustal_budynki_gracza();
                    int totalna_liczba_wiez_Int = wynik[0] + wynik[1];
                    int wynik_odejmowania_wiez_Int = wynik[0] - wynik[1];
                    if (wynik_odejmowania_wiez_Int == 0) // jesli  nie ma wiez lub równa ich ilosc to randomowo 
                    {
                        if (holder == 2)
                        {
                            for (int a = 0; a < ograniczenia_Niszczycieli_Int; a++)
                            {

                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[0].wrog_Nazwa);

                            }
                        }
                        else if (holder == 1)
                        {
                            for (int a = 0; a < ograniczenia_Mechanidow_Int; a++)
                            {

                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[1].wrog_Nazwa);

                            }

                        }
                        else if (holder == 0)
                        {
                            for (int a = 0; a < ograniczenia_Pancernikow_Int; a++)
                            {
                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[2].wrog_Nazwa);
                            }
                        }
                        else if (holder == 3)
                        {
                            for (int a = 0; a < ograniczenia_Analizatorow_Int; a++)
                            {
                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[3].wrog_Nazwa);
                            }
                        }
                    }
                    else if (wynik_odejmowania_wiez_Int > 0)// jesli  jest przewaga wiez obszarowych
                    {

                        float waga = wynik_odejmowania_wiez_Int / totalna_liczba_wiez_Int;
                        int wynik_Kalkulacja_Int = (int)Mathf.Clamp((float)(holder - waga), 0, 3);
                        Debug.Log("waga " + waga);
                        Debug.Log("wynik odejmowania " + wynik_odejmowania_wiez_Int);
                        Debug.Log("wartosc po kalkulacjach" + wynik_Kalkulacja_Int);
                        if (wynik_Kalkulacja_Int == 2)
                        {
                            for (int a = 0; a < ograniczenia_Niszczycieli_Int; a++)
                            {

                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[0].wrog_Nazwa);

                            }
                        }
                        else if (wynik_Kalkulacja_Int == 1)
                        {
                            for (int a = 0; a < ograniczenia_Mechanidow_Int; a++)
                            {

                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[1].wrog_Nazwa);

                            }

                        }
                        else if (wynik_Kalkulacja_Int == 0)
                        {
                            for (int a = 0; a < ograniczenia_Pancernikow_Int; a++)
                            {
                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[2].wrog_Nazwa);
                            }
                        }
                        else if (wynik_Kalkulacja_Int == 3)
                        {
                            for (int a = 0; a < ograniczenia_Analizatorow_Int; a++)
                            {
                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[3].wrog_Nazwa);
                            }
                        }

                    }
                    else // jesli  jest przewaga wiez single target 
                    {
                        float waga = wynik_odejmowania_wiez_Int / totalna_liczba_wiez_Int;
                        int wynik_Kalkulacja_Int = (int)Mathf.Clamp((float)(holder - waga), 0, 3);
                        Debug.Log("waga " + waga);
                        Debug.Log("wynik odejmowania " + wynik_odejmowania_wiez_Int);
                        Debug.Log("wartosc po kalkulacjach" + wynik_Kalkulacja_Int);
                        if (wynik_Kalkulacja_Int == 2)
                        {
                            for (int a = 0; a < ograniczenia_Niszczycieli_Int; a++)
                            {

                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[0].wrog_Nazwa);

                            }
                        }
                        else if (wynik_Kalkulacja_Int == 1)
                        {
                            for (int a = 0; a < ograniczenia_Mechanidow_Int; a++)
                            {

                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[1].wrog_Nazwa);

                            }

                        }
                        else if (wynik_Kalkulacja_Int == 0)
                        {
                            for (int a = 0; a < ograniczenia_Pancernikow_Int; a++)
                            {
                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[2].wrog_Nazwa);
                            }
                        }
                        else if (wynik_Kalkulacja_Int == 3)
                        {
                            for (int a = 0; a < ograniczenia_Analizatorow_Int; a++)
                            {
                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[3].wrog_Nazwa);
                            }
                        }
                    }


                }
            }
            else
            {
                zawartosc_fali_List = new List<string>();
                int holder = UnityEngine.Random.Range(0, 3);
                if (holder == 1)
                {
                    zawartosc_fali_List.Add("Mapa1");
                }
                else
                {
                    zawartosc_fali_List.Add("Mapa1");
                }
            }
        }
        #endregion
        #region poziom trudnosci trudny
        else
        {
            int ograniczenia_Analizatorow_Int = 12;
            int ograniczenia_Niszczycieli_Int = 9;
            int ograniczenia_Mechanidow_Int = 6;
            int ograniczenia_Pancernikow_Int = 3;
            if (czyboss == false)
            {
                zawartosc_fali_List = new List<string>(); // 0  niszcz // 1 mechanid // 2 pancernik // 3 analizator
                for (int i = 0; i < rozmiar; i++)
                {

                    int holder = UnityEngine.Random.Range(0,4);
                    List<int> wynik = ustal_budynki_gracza();
                    int totalna_liczba_wiez_Int = wynik[0] + wynik[1];
                    int wynik_odejmowania_wiez_Int = wynik[0] - wynik[1];
                    if (wynik_odejmowania_wiez_Int == 0) // jesli  nie ma wiez lub równa ich ilosc to randomowo 
                    {
                        if (holder == 2)
                        {
                            for (int a = 0; a < ograniczenia_Niszczycieli_Int; a++)
                            {

                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[0].wrog_Nazwa);

                            }
                        }
                        else if (holder == 1)
                        {
                            for (int a = 0; a < ograniczenia_Mechanidow_Int; a++)
                            {

                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[1].wrog_Nazwa);

                            }

                        }
                        else if (holder == 0)
                        {
                            for (int a = 0; a < ograniczenia_Pancernikow_Int; a++)
                            {
                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[2].wrog_Nazwa);
                            }
                        }
                        else if (holder == 3)
                        {
                            for (int a = 0; a < ograniczenia_Analizatorow_Int; a++)
                            {
                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[3].wrog_Nazwa);
                            }
                        }
                    }
                    else if (wynik_odejmowania_wiez_Int > 0)// jesli  jest przewaga wiez obszarowych
                    {

                        float waga = wynik_odejmowania_wiez_Int / totalna_liczba_wiez_Int;
                        int wynik_Kalkulacja_Int = (int)Mathf.Clamp((float)(holder - waga), 0, 3);
                        Debug.Log("waga " + waga);
                        Debug.Log("wynik odejmowania " + wynik_odejmowania_wiez_Int);
                        Debug.Log("wartosc po kalkulacjach" + wynik_Kalkulacja_Int);
                        if (wynik_Kalkulacja_Int == 2)
                        {
                            for (int a = 0; a < ograniczenia_Niszczycieli_Int; a++)
                            {

                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[0].wrog_Nazwa);

                            }
                        }
                        else if (wynik_Kalkulacja_Int == 1)
                        {
                            for (int a = 0; a < ograniczenia_Mechanidow_Int; a++)
                            {

                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[1].wrog_Nazwa);

                            }

                        }
                        else if (wynik_Kalkulacja_Int == 0)
                        {
                            for (int a = 0; a < ograniczenia_Pancernikow_Int; a++)
                            {
                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[2].wrog_Nazwa);
                            }
                        }
                        else if (wynik_Kalkulacja_Int == 3)
                        {
                            for (int a = 0; a < ograniczenia_Analizatorow_Int; a++)
                            {
                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[3].wrog_Nazwa);
                            }
                        }

                    }
                    else // jesli  jest przewaga wiez single target 
                    {
                        float waga = wynik_odejmowania_wiez_Int / totalna_liczba_wiez_Int;
                        int wynik_Kalkulacja_Int = (int)Mathf.Clamp((float)(holder - waga), 0, 3);
                        Debug.Log("waga " + waga);
                        Debug.Log("wynik odejmowania " + wynik_odejmowania_wiez_Int);
                        Debug.Log("wartosc po kalkulacjach" + wynik_Kalkulacja_Int);
                        if (wynik_Kalkulacja_Int == 2)
                        {
                            for (int a = 0; a < ograniczenia_Niszczycieli_Int; a++)
                            {

                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[0].wrog_Nazwa);

                            }
                        }
                        else if (wynik_Kalkulacja_Int == 1)
                        {
                            for (int a = 0; a < ograniczenia_Mechanidow_Int; a++)
                            {

                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[1].wrog_Nazwa);

                            }

                        }
                        else if (wynik_Kalkulacja_Int == 0)
                        {
                            for (int a = 0; a < ograniczenia_Pancernikow_Int; a++)
                            {
                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[2].wrog_Nazwa);
                            }
                        }
                        else if (wynik_Kalkulacja_Int == 3)
                        {
                            for (int a = 0; a < ograniczenia_Analizatorow_Int; a++)
                            {
                                zawartosc_fali_List.Add(wrog_lista.wrogowie_so_Lista[3].wrog_Nazwa);
                            }
                        }
                    }


                }
            }
            else
            {
                zawartosc_fali_List = new List<string>();
                int holder = UnityEngine.Random.Range(0, 3);
                if (holder == 1)
                {
                    zawartosc_fali_List.Add("Mapa1");
                }
                else
                {
                    zawartosc_fali_List.Add("Mapa1");
                }
            }
        }
        #endregion
        Wyznaczenie_fali_ui_event?.Invoke(this, new Holder_fali { wrogow_List = zawartosc_fali_List });
        ustalPancerz();
        return zawartosc_fali_List;
    }
    public List<int> ustal_budynki_gracza()
    {
        GameObject[] obiektyZTagiem = GameObject.FindGameObjectsWithTag("Wieza_Obszarowa");
        GameObject[] obiektyZTagiem1 = GameObject.FindGameObjectsWithTag("Wieza_Pojedyncza");
        
        List<int> zawartosc_List = new List<int>();
        zawartosc_List.Add(obiektyZTagiem.Length);
        zawartosc_List.Add(obiektyZTagiem1.Length);
        return zawartosc_List;
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

            // Losuj 3 zmienne typu float z zakresów od 0.5 do 1
            zmiennaFloat1 = (float)System.Math.Round(Random.Range(0.5f, 1f), 1);
            zmiennaFloat2 = (float)System.Math.Round(Random.Range(0.5f, 1f), 1);
            zmiennaFloat3 = (float)System.Math.Round(Random.Range(0.5f, 1f), 1);
        }
        else if (ZczytanyPoziomTrudnosci_Int == 2)
        {
            zmiennaFloat1 = (float)System.Math.Round(Random.Range(0.3f, 0.8f), 1);
            zmiennaFloat2 = (float)System.Math.Round(Random.Range(0.3f, 0.8f), 1);
            zmiennaFloat3 = (float)System.Math.Round(Random.Range(0.3f, 0.8f), 1);
        }
        // Wyœwietl wartoœci zmiennych
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


