using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_generator_energi_klikalnosc : MonoBehaviour
{
    public static UI_generator_energi_klikalnosc instance { get; private set; }
    public Transform ui_budowy_transform_parent;
    public Transform ui_budowy_transform;
    // Przypisz przycisk UI do tej zmiennej w inspektorze Unity
    private UI_Budynkow UI_budynkow_holder;
    public Button przyciskUI;
    bool isactive;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        isactive = false;
    }
    void InstancjonujObiekt()
    {

        ui_budowy_transform_parent.gameObject.SetActive(true);
        ui_budowy_transform.gameObject.SetActive(true);
    }
    void DeinstacjounujObiekt()
    {

        ui_budowy_transform_parent.gameObject.SetActive(false);
        ui_budowy_transform.gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        if (!isactive)
        {
            Budynki_klikanlosc.instance.DezaktywujDzieci();
            InstancjonujObiekt();
            isactive = true;
        }
        else
        {
            
            DeinstacjounujObiekt();
            isactive = false;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            OnMouseDown();

        }
    }
}
