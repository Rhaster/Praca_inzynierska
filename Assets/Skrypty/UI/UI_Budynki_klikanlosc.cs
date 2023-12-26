using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Budynki_klikanlosc : MonoBehaviour
{
    public static Budynki_klikanlosc instance { get; private set; } 
    public Transform ui_budowy_transform_parent;
    public Transform ui_budowy_transform;
    public Transform ui_generatora_transform;
    public Transform ui_wiezy_ustawienia_Transform;
    public Transform ui_fabryki_Transform;
    // Przypisz przycisk UI do tej zmiennej w inspektorze Unity
    private GeneratorSurowcow generator;
    private UI_Budynkow UI_budynkow_holder;
    public Button przyciskUI;
    bool isactive;
    void Start()
    {
        isactive= false;
    }
    void InstancjonujObiekt()
    {
        // Instancjonuj obiekt przy u¿yciu pozycji i rotacji aktualnego Transforma
        generator = GetComponent<GeneratorSurowcow>();
        ui_budowy_transform_parent.gameObject.SetActive(true);
        DezaktywujDzieci();
        UI_Fabryka_klikalnosc.instance.DezaktywujDzieci();
        ui_budowy_transform.gameObject.SetActive(true);
        UI_budynkow_holder = ui_budowy_transform.GetComponent<UI_Budynkow>();
        UI_budynkow_holder.surowiec = generator.surowiecGenerowany;
        //Debug.Log(generator.nazwa_kopalni);
        UI_budynkow_holder.nazwa_kopalni_Text.SetText(generator.nazwa_kopalni);
        UI_budynkow_holder.generator= generator;
        isactive= true;
    }
    private void Awake()
    {
        instance = this;
        //ui_wiezy_ustawienia_Transform = UIController.instance.transform.Find("UI_menu_wiezy");
    }
    public void DezaktywujDzieci()
    {
        foreach (Transform dziecko in ui_budowy_transform_parent)
        {
            dziecko.gameObject.SetActive(false);
        }
        ui_fabryki_Transform.gameObject.SetActive(false);
        ui_generatora_transform.gameObject.SetActive(false);
        //ui_wiezy_ustawienia_Transform.gameObject.SetActive(false) ;
        Dezaktywacja();
    }
    private void Dezaktywacja()
    {
        GameObject[] obiektyZTagiem = GameObject.FindGameObjectsWithTag("UstawienieWiezy");

        foreach (GameObject obiekt in obiektyZTagiem)
        {
            obiekt.SetActive(false);
        }
    }
    void OnMouseDown()
    {
        if (!isactive)
        {
            InstancjonujObiekt();
           
        }
        else
        {
            ui_budowy_transform.gameObject.SetActive(false);
            isactive= false;
            DezaktywujDzieci();
        }
    }
}
