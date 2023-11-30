using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Budynki_klikanlosc : MonoBehaviour
{
    public static Budynki_klikanlosc instance { get; private set; } 
    public Transform ui_budowy_transform_parent;
    public Transform ui_budowy_transform;
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
        Debug.Log(generator.nazwa_kopalni);
        UI_budynkow_holder.nazwa_kopalni_Text.SetText(generator.nazwa_kopalni);
        UI_budynkow_holder.generator= generator;
    }
    private void Awake()
    {
        instance = this;
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
            Debug.Log("kli");
            InstancjonujObiekt();
           
        }
    }
}
