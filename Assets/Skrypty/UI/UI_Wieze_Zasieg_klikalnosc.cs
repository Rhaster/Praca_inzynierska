using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_Wieze_Zasieg_klikalnosc : MonoBehaviour
{
    
    public Transform ui_wiezy_zasieg_transform;
    public Transform ui_wiezy_ustawienia_Transform;
    public Transform sekcja_ustawien_Transform;
    public Transform sekcja_statystyk_Transform;
    public Transform opcje_wiezy_Transform;
    private List<Transform> Lista_wiez;
    private Wieze_SO wieza_Holder;
    private Button przycisk_produkuj_amunicje1_button;
    private Button przycisk_produkuj_amunicje2_button;
    private Button przycisk_produkuj_amunicje3_button;
    private Wieza kontrola_Wieza;
    private float zasieg_wiezy;
    private TextMeshProUGUI wyswietlany_zasieg_TMPRO;
    private TextMeshProUGUI wyswietlany_status_TMPRO;
    private TextMeshProUGUI wyswietlany_obrazenia_TMPRO;
    private TextMeshProUGUI wyswietlany_czas_przeladowania_TMPRO;
    private Image wskaznik_czasu_przeladowania_Image;

    void Start()
    {
        sekcja_ustawien_Transform = ui_wiezy_ustawienia_Transform.Find("Sekcja_wiezy");
        wieza_Holder = GetComponent<HolderRodzajuWiezy>().holderWiezy;
       kontrola_Wieza = GetComponent<Wieza>();
       
        #region grabberi settery pod statystyki wiez
        zasieg_wiezy = kontrola_Wieza.zasieg_wiezy_Float;
        
        wyswietlany_zasieg_TMPRO = sekcja_ustawien_Transform.Find("zasieg_text").GetComponent<TextMeshProUGUI>();
        wyswietlany_zasieg_TMPRO.SetText(zasieg_wiezy.ToString());
        sekcja_statystyk_Transform = sekcja_ustawien_Transform.Find("Sekcja_generatora");
        sekcja_ustawien_Transform.Find("nazwa_wiezy_text").GetComponent<TextMeshProUGUI>().SetText(wieza_Holder.wieza_Nazwa);
        kontrola_Wieza.zmianaCzasuPrzeladowania += Kontrola_Wieza_zmianaCzasuPrzeladowania;
        wskaznik_czasu_przeladowania_Image = sekcja_statystyk_Transform.Find("Image").Find("pasek").GetComponent<Image>();
        wyswietlany_status_TMPRO = sekcja_statystyk_Transform.Find("Statystyka").GetComponent<TextMeshProUGUI>();
        wyswietlany_obrazenia_TMPRO = sekcja_ustawien_Transform.Find("Obrazenia_text").GetComponent<TextMeshProUGUI>();
        wyswietlany_obrazenia_TMPRO.SetText(wieza_Holder.Obrazenia_wiezy_Float.ToString());
        wyswietlany_czas_przeladowania_TMPRO = sekcja_ustawien_Transform.Find("Przeladowanie_text").GetComponent<TextMeshProUGUI>();
        wyswietlany_czas_przeladowania_TMPRO.SetText(wieza_Holder.Czas_przeladowania_wiezy_Float.ToString());

        #endregion
        #region Grabbery przycisków UI_ ustawien wiez
        Transform grabberButton1 = sekcja_statystyk_Transform.Find("Button_1");
        Transform grabberButton2 = sekcja_statystyk_Transform.Find("Button_2");
        Transform grabberButton3 = sekcja_statystyk_Transform.Find("Button_3");
        przycisk_produkuj_amunicje1_button = grabberButton1.GetComponent<Button>();
        przycisk_produkuj_amunicje2_button = grabberButton2.GetComponent<Button>();
        przycisk_produkuj_amunicje3_button = grabberButton3.GetComponent<Button>();
        #endregion
        #region Graber i setter obiektu reprezentujacego zasieg wiezy
        ui_wiezy_zasieg_transform = transform.Find("Zasieg");
        float zasieg = GetComponent<Wieza>().zasieg_wiezy_Float;
        ui_wiezy_zasieg_transform.localScale = new Vector3(zasieg,zasieg,0);
        ui_wiezy_zasieg_transform.gameObject.SetActive(false);
        #endregion
        #region sluchacze pod przyciski
        przycisk_produkuj_amunicje1_button.onClick.AddListener(() => {
            kontrola_Wieza.GetCzasPrzeladowania();
        });
        przycisk_produkuj_amunicje2_button.onClick.AddListener(() => {
            

        });
        przycisk_produkuj_amunicje3_button.onClick.AddListener(() => {
            
        });
        #endregion
    }
 

    private void Kontrola_Wieza_zmianaCzasuPrzeladowania(object sender, System.EventArgs e)
    {
        if(kontrola_Wieza.GetCzyPRzeladowano())
        {
            wyswietlany_status_TMPRO.SetText("Gotowa do strza³u");
        }
        else
        {
            wyswietlany_status_TMPRO.SetText("Prze³adowanie");
        }
      
        wskaznik_czasu_przeladowania_Image.fillAmount = kontrola_Wieza.GetCzasPrzeladowania();
    }

 

    private void Awake()
    {
        ui_wiezy_ustawienia_Transform= UIController.instance.transform.Find("UI_menu_wiezy");
        Lista_wiez = new List<Transform>();
    }

    void OnMouseDown()
    {

            if (!ui_wiezy_zasieg_transform.gameObject.activeSelf)
            {
                Budynki_klikanlosc.instance.DezaktywujDzieci();
                ui_wiezy_zasieg_transform.gameObject.SetActive(true);
                ui_wiezy_ustawienia_Transform.gameObject.SetActive(true);
            }
            else
            {
            Budynki_klikanlosc.instance.DezaktywujDzieci();
            ui_wiezy_zasieg_transform.gameObject.SetActive(false);
                ui_wiezy_ustawienia_Transform.gameObject.SetActive(false);
            }

    }
    private void OnMouseExit()
    {
        if (ui_wiezy_zasieg_transform.gameObject.activeSelf)
        {
            ui_wiezy_zasieg_transform.gameObject.SetActive(false);
            
        }
    }
}
