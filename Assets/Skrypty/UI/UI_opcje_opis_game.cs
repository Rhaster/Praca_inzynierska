using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class opcje_opis_game : MonoBehaviour
{
# region Deklaracje zmiennych 
    public static opcje_opis_game instance;
    private Transform t�o_transform;
    private Transform wyjscie_transform;
    private Transform menu_transform;
    private TextMeshProUGUI text_Poziom_Trudnosci_TMPRO;
    private TextMeshProUGUI text_ilosc_Fal_TMPRO;
    private float holder_czasu_Float;
    #endregion
    private void Awake()
    {
        instance = this; // przypisanie instancji 
        #region pobranie obiekt�w przez find
        t�o_transform = transform.Find("T�o");
        wyjscie_transform = transform.Find("wyjscie");
        menu_transform = transform.Find("menu");
        #endregion
        #region sekcja poziomu trudnosci
        text_Poziom_Trudnosci_TMPRO = transform.Find("Poziom_trudnosci").Find("text1").GetComponent<TextMeshProUGUI>();
        WyswietlaniePozTrudnosci();
        text_ilosc_Fal_TMPRO = transform.Find("Ilosc_Fal").Find("text1").GetComponent<TextMeshProUGUI>();
        text_ilosc_Fal_TMPRO.SetText(LadowaniePlayerPrefs.GetLiczbaFal().ToString());
        #endregion
        gameObject.SetActive(false); // domyslnie wy��cz opcje na start 
    }
    private void WyswietlaniePozTrudnosci()
    {
        int a = LadowaniePlayerPrefs.GetDifficulty();
        if(a ==0)
        {
            text_Poziom_Trudnosci_TMPRO.SetText("�atwy");
        }
        else if(a==1)
        {
            text_Poziom_Trudnosci_TMPRO.SetText("Sredni");
        }
        else
        {
            text_Poziom_Trudnosci_TMPRO.SetText("Trudny");
        }
    }
    private void Start()
    {
#region   Logika menu opcji 
        WstrzymajGr�(); // domyslnie wstrzymaj gr� przy odpaleniu menu opcji 
        
        wyjscie_transform.GetComponent<Button>().onClick.AddListener(() => // s�uchacz gdy wcisnie si� przycisk wyjscie 
        {
            Wznow_gre();   // wzn�w gre 
            ButtonHandler_game xd = wyjscie_transform.GetComponent<ButtonHandler_game>(); // znajdz przycisk 
            UIController.instance.ReaktuywujUI(); // reaktywuj UI gracza 
            gameObject.SetActive(false); // wy��cz obiekt 
            
        });
        menu_transform.GetComponent<Button>().onClick.AddListener(() => // s�uchacz gdy wcisnie si� przycisk wyjscie  menu
        {
            Wznow_gre(); // wzn�w przep�yw czasu 
            SceneManager.LoadScene("MenuG��wne"); // za�aduj menu g��wne 
        });
        #endregion
    }
    private void Update()
    {
        #region Logika przy aktywnym obiekcie menu
        if (Time.timeScale > 0) // Wstrzymaj gre jesli obiekt jest aktywny
        {
            WstrzymajGr�();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) // s�uchacz na przycisk Escape (zamknie obiekt)
        {
            Debug.Log("wcisnieto escape");
            if (Budowanie_Grid.Instance.czyMenuBudowaniaOtwarte() == false)
            {
                Wznow_gre(); // wznowienie gry
                UIController.instance.ReaktuywujUI(); // reaktywacja ui 
                gameObject.SetActive(false); // wy��czenie opcji 
            }
            else
            {
                Budowanie_Grid.Instance.DeaktywujBudowanie();
            }

        }
        #endregion
    }
    #region Wstrzymanie Wznowienie gry
    private void WstrzymajGr�()
    {
        holder_czasu_Float = Time.timeScale;
        UI_Czasmanager.instance.StopCzas_Przycisk();
    }

    private void Wznow_gre()
    {
        UI_Czasmanager.instance.StartCzas_Przycisk(holder_czasu_Float);
    }
    #endregion
}
