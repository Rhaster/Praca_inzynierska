using System.Collections;

using UnityEngine;
using TMPro;
using System;


public class UI_kontrola : MonoBehaviour
{

    #region Kontrola czasu
    private float czas_gry_float = 0f;
    [SerializeField]private float timer = 0f;
    [SerializeField] private float timer_czasSpawnuFali_float;
    private float logInterval = 1f;
    private TextMeshProUGUI Licznikczasu_transform;
    private Transform czasUI;
    #endregion
    #region ZasobyUI
    private Transform zasoby_Transform;
    #endregion
    #region Deklaracja instancji skryptu
    public static UI_kontrola instance;
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
    #region UI budynk�w
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
    #region UI indykatora fali

    private Transform indykator_fali_Transform;

    #endregion
    #region Event aktywacji bossa
    public event EventHandler aktywacja_bossa_Event;
    #endregion
    #region corutyna  oraz kontrola flow gry
    public float timer_spawnu_nast_fali_Float;
    // Debug.Log("Czas spawnu fali " + nextWaveSpawnTimer);
    public float timemax_Float;
    public int i;
    public int falabossa_znacznik_Int;
    #endregion
    #region UI generatoraEnergi
    private Transform UI_GeneratoraEnergi_Transform;
    private Boolean UI_generatoraEnergi_bool;
    #endregion
    
    private void Awake()
    {
        #region flaga dla eventu konca gry
        falabossa_znacznik_Int = LadowaniePlayerPrefs.GetLiczbaFal() + 1;
        #endregion
        #region Przypisanie instancji
        instance = this;
        #endregion
        #region Grabbery transform�w 
        zasoby_Transform = transform.Find("UI_Ekonomia");
        czasUI = transform.Find("UI_czas");
        Licznikczasu_transform = czasUI.Find("czas_gry_mod").GetComponent<TextMeshProUGUI>();
        UI_wavemanager_transfrom = transform.Find("UI_MenadzerFali");
        opcje_transform = transform.Find("OpcjeExpander");
        UI_elektrka = transform.Find("UI_Energia");
        UI_amunicja = transform.Find("UI_Amunicja");
        UI_budynkow_transform = transform.Find("UI_budynkow");
        UI_wiez = transform.Find("UI_wieze");
        UI_Menu_Przycisk_rozwin = transform.Find("UI_Przycisk_rozwin");
        UI_Menadzera_energi = transform.Find("UI_MenadzerEnergi");
        UI_Fabryki_Transform = transform.Find("UI_Fabryki");
        UI_GeneratoraEnergi_Transform = transform.Find("UI_GeneratorEnergi");
        indykator_fali_Transform = transform.Find("UI_Znacznik_Fali");
        #endregion

    }
    void Start()
    {
        MechanikaFal.Instance.zmianaFali_event += Instance_zmianaFali_event;
        #region Przypisanie sluchacza do eventu zmiany fali
        
        //MechanikaFal.Instance.zmianaFali_event += Instance_zmianaFali_event;
        #endregion
        OnDeaktywacja();
    }
    private void OnDeaktywacja()
    {
        UI_budynkow_czybylootwarte_bool = false;
        UI_budynkow_transform.gameObject.SetActive(false);
        UI_Menadzera_energi.gameObject.SetActive(false);
        UI_Fabryki_Transform.gameObject.SetActive(false);
        UI_wiez.gameObject.SetActive(false);
        UI_GeneratoraEnergi_Transform.gameObject.SetActive(false);
        //indykator_fali_Transform.gameObject.SetActive(false);
       // UI_wiezy_ustawienia_Transform.gameObject.SetActive(false);
    }
    #region Zmiana fali update tekstu 
    private void Instance_zmianaFali_event(object sender, EventArgs e)
    {
        //Debug.Log(falabossa_znacznik_Int + " " + MechanikaFal.Instance.GetNumerFali());
        if(falabossa_znacznik_Int == MechanikaFal.Instance.GetNumerFali())
        {
            StartCoroutine(Odliczanie_Corutine());
            nr_fali_TMPRO.SetText("BOSS");
        }
        else
        {
            StartCoroutine(Odliczanie_Corutine());
        }
    }
    IEnumerator Odliczanie_Corutine()
    {
        if (falabossa_znacznik_Int != MechanikaFal.Instance.GetNumerFali())
        {
            nr_fali_TMPRO.SetText(MechanikaFal.Instance.GetNumerFali().ToString());
        }
        timer_spawnu_nast_fali_Float = MechanikaFal.Instance.GetCzasSpawnuFali();
       // Debug.Log("Czas spawnu fali " + nextWaveSpawnTimer);
        timemax_Float = timer_spawnu_nast_fali_Float;
        i = 1;
        timer_czasSpawnuFali_float = 1;
        // Dla oszcz�dno�ci zasob�w odwo�uje si� do mechaniki fal tylko przy pierwszym wywo�aniu
        // celem uzyskania czasu max spawnu
        while (timemax_Float >= 0)
        {
            timer_czasSpawnuFali_float -= Time.deltaTime;
            if (timer_czasSpawnuFali_float <= 0f)
            {
                timer_czasSpawnuFali_float = 1;
                
                timemax_Float -= i;
                if (timemax_Float >= 0)
                {
                    UstawTimerCzasuFali(((timemax_Float).ToString("F0") + " sekund"));
                }
            }
            yield return null; // Oczekaj na nast�pn� klatk�
        }

        if (timemax_Float <= 0)
        {
            UstawTimerCzasuFali("");
            if (falabossa_znacznik_Int == MechanikaFal.Instance.GetNumerFali())
            {
                aktywacja_bossa_Event?.Invoke(this, EventArgs.Empty);
            }
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
        czas_gry_float += Time.deltaTime;
        timer += Time.deltaTime;

        // Loguj co sekund�
        if (timer >= logInterval)
        {
            
            int minuty = Mathf.FloorToInt(czas_gry_float / 60);
            int sekundy = Mathf.FloorToInt(czas_gry_float % 60);
           
            Licznikczasu_transform.SetText(string.Format("{0:00}:{1:00}", minuty, sekundy));
            timer = 0f; // Zresetuj licznik czasu
        }
        #endregion
        #region obsluga przycisku escape 
        if (Input.GetKeyDown(KeyCode.Escape) && (opcje_transform.gameObject.activeSelf == false))
        {
            Dezaktywacja();
            Kontroler_kamery.Instance.Wylacz_kamere();
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
            if(UI_GeneratoraEnergi_Transform.gameObject.activeSelf)
            {
                UI_generatoraEnergi_bool = true;
            }
            else
            {
                UI_generatoraEnergi_bool =false;
            }
            if(UI_Fabryki_Transform.gameObject.activeSelf)
            {
                UI_Fabryki_Transform_CzyOtwarte = true;
            }
            else
            {
                UI_Fabryki_Transform_CzyOtwarte = false;
            }
            indykator_fali_Transform.gameObject.SetActive(false);
            UI_GeneratoraEnergi_Transform.gameObject.SetActive(false);
            UI_Fabryki_Transform.gameObject.SetActive(false);
            UI_wiez.gameObject.SetActive(false);
            UI_amunicja.gameObject.SetActive(false);
            UI_elektrka.gameObject.SetActive(false);
            zasoby_Transform.gameObject.SetActive(false);
            //czasUI.gameObject.SetActive(false); 
            czasUI.gameObject.SetActive(true);
            transform.Find("UI_Ustawienie_Czasu").gameObject.SetActive(true);
            UI_wavemanager_transfrom.gameObject.SetActive(false);
            opcje_transform.gameObject.SetActive(true);
            UI_Menu_Przycisk_rozwin.gameObject.SetActive(false);
            UI_budynkow_transform.gameObject.SetActive(false);
            UI_Menadzera_energi.gameObject.SetActive(false);
            
        }
       if(Input.GetKeyDown(KeyCode.V)) {

            Dezaktywacja();
            indykator_fali_Transform.gameObject.SetActive(false);
            UI_GeneratoraEnergi_Transform.gameObject.SetActive(false);
            UI_Fabryki_Transform.gameObject.SetActive(false);
            UI_wiez.gameObject.SetActive(false);
            UI_amunicja.gameObject.SetActive(false);
            UI_elektrka.gameObject.SetActive(false);
            zasoby_Transform.gameObject.SetActive(false);
            czasUI.gameObject.SetActive(false); 
            UI_wavemanager_transfrom.gameObject.SetActive(false);
            UI_Menu_Przycisk_rozwin.gameObject.SetActive(false);
            UI_budynkow_transform.gameObject.SetActive(false);
            UI_Menadzera_energi.gameObject.SetActive(false);
            transform.Find("UI_Ustawienie_Czasu").gameObject.SetActive(false);

        }
        #endregion
    }
    public void Dezaktywacja()
    {
        GameObject[] obiektyZTagiem = GameObject.FindGameObjectsWithTag("UstawienieWiezy");
        GameObject[] obiektyZTagiem1 = GameObject.FindGameObjectsWithTag("UI_wroga");
        foreach (GameObject obiekt in obiektyZTagiem)
        {
            obiekt.SetActive(false);
        }
        foreach (GameObject obiekt in obiektyZTagiem1)
        {
            obiekt.SetActive(false);
        }
    }
    #region Funkcja reaktywujaca interfejs uzytkownika
    public void ReaktuywujUI()
    {
        Kontroler_kamery.Instance.Wlacz_kamere();
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
        if(UI_generatoraEnergi_bool == true)
        {
            UI_GeneratoraEnergi_Transform.gameObject.SetActive(true);
        }
       if(UI_Fabryki_Transform_CzyOtwarte == true)
        {
            UI_Fabryki_Transform.gameObject.SetActive(true);
        }
        indykator_fali_Transform.gameObject.SetActive(true);
        UI_Menu_Przycisk_rozwin.gameObject.SetActive(true);
        UI_amunicja.gameObject.SetActive(true);
        UI_elektrka.gameObject.SetActive(true);
        zasoby_Transform.gameObject.SetActive(true) ;
        //czasUI.gameObject.SetActive(true);
        UI_wavemanager_transfrom.gameObject.SetActive(true);

    }
    #endregion

    

}
