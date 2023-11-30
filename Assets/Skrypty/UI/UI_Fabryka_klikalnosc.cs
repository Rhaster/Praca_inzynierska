using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Fabryka_klikalnosc : MonoBehaviour
{
    public static UI_Fabryka_klikalnosc instance { get; private set; }
    public Transform ui_budowy_transform_parent;
    public Transform ui_budowy_transform;
    // Przypisz przycisk UI do tej zmiennej w inspektorze Unity
    private GeneratorAmunicji generator;
    private UI_Budynkow UI_budynkow_holder;
    public Button przyciskUI;
    bool isactive;
    private void Awake()
    {
        instance= this;
    }
    void Start()
    {
        isactive = false;
    }
    void InstancjonujObiekt()
    {
        // Instancjonuj obiekt przy u¿yciu pozycji i rotacji aktualnego Transforma
        generator = GetComponent<GeneratorAmunicji>();
        ui_budowy_transform_parent.gameObject.SetActive(true);
        DezaktywujDzieci();
        Budynki_klikanlosc.instance.DezaktywujDzieci();
        ui_budowy_transform.gameObject.SetActive(true);
        UI_budynkow_holder = ui_budowy_transform.GetComponent<UI_Budynkow>();
        //UI_budynkow_holder.surowiec = generator.amunicjaGenerowany;
        Debug.Log(generator.nazwa_kopalni);
        UI_budynkow_holder.nazwa_kopalni_Text.SetText(generator.nazwa_kopalni);
        //UI_budynkow_holder.generator = generator;
    }
    public void DezaktywujDzieci()
    {
        foreach (Transform dziecko in ui_budowy_transform_parent)
        {
            dziecko.gameObject.SetActive(false);
        }
    }
    void OnMouseDown()
    {
        if (!isactive)
        {
            InstancjonujObiekt();

        }
    }
    
}
