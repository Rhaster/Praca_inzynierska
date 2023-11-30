using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class UIController : MonoBehaviour
{

    #region Kontrola czasu
    private float gameTime = 0f;
    private float timer = 0f;
    private float logInterval = 1f;
    private TextMeshProUGUI Licznikczasu_transform;
    private Transform czasUI;
    #endregion
    #region ZasobyUI
    private Transform zasoby_Transform;
    #endregion
    #region Deklaracja instancji skryptu
    public static UIController instance;
    #endregion
    #region Kontrola tekstu w UI 
    [SerializeField] private TextMeshProUGUI czas_do_nast_fali_TMPRO;
    [SerializeField] private TextMeshProUGUI nr_fali_TMPRO;
    #endregion
    #region Kontrola widocznosci UI opcji i fal
    private Transform UI_wavemanager_transfrom;
    private Transform opcje_transform;
    #endregion
    #region kontrola ui pokazujacego wskaznik energi
    private Transform UI_elektrka;
    private Transform UI_amunicja;
    #endregion
    #region UI budynków
    [SerializeField] private Boolean UI_budynkow_czybylootwarte_bool;
    private Transform UI_budynkow_transform;
    #endregion
    #region UI wiez
    [SerializeField] private Boolean UI_wieze_czybylootwarte_bool;
    private Transform UI_wiez;
    #endregion
    #region UI przyciski rozwijane
    private Transform UI_Menu_Przycisk_rozwin;
    #endregion
    #region UI Menadzera energi
    private Transform UI_Menadzera_energi;
    [SerializeField]private Boolean UI_menadzera_energi_czybylootwarte_bool;
    #endregion
    #region UI Fabryki
    private Transform UI_Fabryki_Transform;
    private Boolean UI_Fabryki_Transform_CzyOtwarte;
    #endregion
    private void Awake()
    {
        #region Przypisanie instancji
        instance = this;
        #endregion
        #region Grabbery transformów 
        zasoby_Transform = transform.Find("UI_Ekonomia");
        czasUI = transform.Find("UI_czas");
        Licznikczasu_transform = czasUI.Find("czas_gry_mod").GetComponent<TextMeshProUGUI>();
        UI_wavemanager_transfrom = transform.Find("UI_Wavemanager");
        opcje_transform = transform.Find("OpcjeExpander");
        UI_elektrka = transform.Find("UI_Energia");
        UI_amunicja = transform.Find("UI_Amunicja");
        UI_budynkow_transform = transform.Find("UI_budynkow");
        UI_wiez = transform.Find("UI_wieze");
        UI_Menu_Przycisk_rozwin = transform.Find("UI_Przycisk_rozwin");
        UI_Menadzera_energi = transform.Find("UI_MenadzerEnergi");
        UI_Fabryki_Transform = transform.Find("UI_Fabryki");
        #endregion
        #region wylaczenie ui budynkow na starcie
        UI_budynkow_czybylootwarte_bool = false;
        UI_budynkow_transform.gameObject.SetActive(false);
        UI_Menadzera_energi.gameObject.SetActive(false) ;
        UI_Fabryki_Transform.gameObject.SetActive(false) ;
        #endregion
    }
    void Start()
    {
        #region Przypisanie sluchacza do eventu zmiany fali
        MechanikaFal.Instance.zmianaFali_event += Instance_zmianaFali_event;
        #endregion
    }

    #region Zmiana fali update tekstu 
    private void Instance_zmianaFali_event(object sender, EventArgs e)
    {
        StartCoroutine(CountdownCoroutine());
    }
    IEnumerator CountdownCoroutine()
    {
        nr_fali_TMPRO.SetText(MechanikaFal.Instance.GetNumerFali().ToString());
        float nextWaveSpawnTimer = MechanikaFal.Instance.GetCzasSpawnuFali();
       // Debug.Log("Czas spawnu fali " + nextWaveSpawnTimer);
        float timemax = nextWaveSpawnTimer;
        int i = 1;
        float timer = 1;
        // Dla oszczêdnoœci zasobów odwo³uje siê do mechaniki fal tylko przy pierwszym wywo³aniu
        // celem uzyskania czasu max spawnu
        while (timemax >= 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = 1;
                
                timemax -= i;
                UstawTimerCzasuFali(((timemax).ToString("F0") + " sekund"));
            }
            yield return null; // Oczekaj na nastêpn¹ klatkê
        }

        if (timemax < 0)
        {
            UstawTimerCzasuFali("");
        }
    }

    void UstawTimerCzasuFali(string text)
    {
        // Tutaj dodaj kod do ustawienia tekstu w UI
        czas_do_nast_fali_TMPRO.SetText(text);
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        #region Kontrola czasu w UI 
        gameTime += Time.deltaTime;
        timer += Time.deltaTime;

        // Loguj co sekundê
        if (timer >= logInterval)
        {
            int minutes = Mathf.FloorToInt(gameTime / 60);
            int seconds = Mathf.FloorToInt(gameTime % 60);
           
            Licznikczasu_transform.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
            timer = 0f; // Zresetuj licznik czasu
        }
        #endregion
        #region obsluga przycisku escape 
        if (Input.GetKeyDown(KeyCode.Escape) && (opcje_transform.gameObject.activeSelf == false))
        {
            if (UI_budynkow_transform.gameObject.activeSelf)
            {
                UI_budynkow_czybylootwarte_bool = true;
            }
            else
            {
                UI_budynkow_czybylootwarte_bool = false;
            }
            if(UI_Menadzera_energi.gameObject.activeSelf)
            {
                UI_menadzera_energi_czybylootwarte_bool =true;
            }
            else
            {
                UI_menadzera_energi_czybylootwarte_bool =false;
            }
            if(UI_wiez.gameObject.activeSelf)
            {
                UI_wieze_czybylootwarte_bool = true;
            }
            else
            {
                UI_wieze_czybylootwarte_bool = false;
            }
            UI_Fabryki_Transform.gameObject.SetActive(false);
            UI_wiez.gameObject.SetActive(false);
            UI_amunicja.gameObject.SetActive(false);
            UI_elektrka.gameObject.SetActive(false);
            zasoby_Transform.gameObject.SetActive(false);
            //czasUI.gameObject.SetActive(false); 
            UI_wavemanager_transfrom.gameObject.SetActive(false);
            opcje_transform.gameObject.SetActive(true);
            UI_Menu_Przycisk_rozwin.gameObject.SetActive(false);
            UI_budynkow_transform.gameObject.SetActive(false);
            UI_Menadzera_energi.gameObject.SetActive(false);

        }
        #endregion
    }
    #region Funkcja reaktywujaca interfejs uzytkownika
    public void ReaktuywujUI()
    {
        if (UI_budynkow_czybylootwarte_bool == true)
        {
            UI_budynkow_transform.gameObject.SetActive(true);
        }
        if (UI_menadzera_energi_czybylootwarte_bool == true)
        {
            UI_Menadzera_energi.gameObject.SetActive(true);
        }
        if (UI_wieze_czybylootwarte_bool == true)
        {
  
            UI_wiez.gameObject.SetActive(true);
        }
        UI_Menu_Przycisk_rozwin.gameObject.SetActive(true);
        UI_amunicja.gameObject.SetActive(true);
        UI_elektrka.gameObject.SetActive(true);
        zasoby_Transform.gameObject.SetActive(true) ;
        //czasUI.gameObject.SetActive(true);
        UI_wavemanager_transfrom.gameObject.SetActive(true);

    }
    #endregion
}
