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
    public static UIController instance;
    #region Kontrola tekstu w UI 
    [SerializeField] private TextMeshProUGUI czas_do_nast_fali_TMPRO;
    [SerializeField] private TextMeshProUGUI nr_fali_TMPRO;
    #endregion
    #region Kontrola widocznosci UI
    private Transform UI_wavemanager_transfrom;
    private Transform opcje_transform;
    #endregion
    private Transform UI_elektrka;
    private Transform UI_amunicja;
    #region UI budynków
    private Boolean czybylootwarte;
    private Transform UI_budynkow;
    #endregion
    private void Awake()
    {
        instance = this;
        #region Grabbery transformów 
        zasoby_Transform = transform.Find("UI_Ekonomia");
        czasUI = transform.Find("UI_czas");
        Licznikczasu_transform = czasUI.Find("czas_gry_mod").GetComponent<TextMeshProUGUI>();
        UI_wavemanager_transfrom = transform.Find("UI_Wavemanager");
        opcje_transform = transform.Find("OpcjeExpander");
        UI_elektrka = transform.Find("UI_Energia");
        UI_amunicja = transform.Find("UI_Amunicja");
        UI_budynkow = transform.Find("UI_budynkow");

        #endregion
        // wylaczenie ui budynkow na starcie
        czybylootwarte = false;
        UI_budynkow.gameObject.SetActive(false);

    }
    void Start()
    {
        MechanikaFal.Instance.zmianaFali_event += Instance_zmianaFali_event;
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UI_budynkow.gameObject.activeSelf)
            {
                czybylootwarte = true;
            }    
            UI_budynkow.gameObject.SetActive(false);
            UI_amunicja.gameObject.SetActive(false);
            UI_elektrka.gameObject.SetActive(false);
            zasoby_Transform.gameObject.SetActive(false);
            czasUI.gameObject.SetActive(false);
            UI_wavemanager_transfrom.gameObject.SetActive(false);
            opcje_transform.gameObject.SetActive(true);
   
        }
    }
    public void ReaktuywujUI()
    {
        if (czybylootwarte)
        {
            UI_budynkow.gameObject.SetActive(true);
        }
        UI_amunicja.gameObject.SetActive(true);
        UI_elektrka.gameObject.SetActive(true);
        zasoby_Transform.gameObject.SetActive(true) ;
        czasUI.gameObject.SetActive(true);
        UI_wavemanager_transfrom.gameObject.SetActive(true);
    }
}
