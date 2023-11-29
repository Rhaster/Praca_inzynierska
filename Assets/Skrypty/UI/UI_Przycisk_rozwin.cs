using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Przycisk_rozwin : MonoBehaviour
{
    // Skrypt odpowiada za mechanike aktywowania UI wyboru wie¿ do zbudowania 


    #region Deklaracje zmiennych Transform
    [SerializeField]private Transform UI_Menadzer_Energi_Transform;
    [SerializeField] private Transform UI_Menadzer_Budowania_Transform;
    public Transform UI_Menadzer_Budowanie_Object_transform;
    public Transform UI_Menadzer_Energia_Object_transform;
    #endregion
    #region Deklaracje zmiennych Button
    [SerializeField] private Button UI_Menadzer_Energi_Button;
    [SerializeField] private Button UI_Menadzer_Budowania_Button;
    #endregion
    private void Awake()
    {
        #region Pobranie transformów dzieci 
        UI_Menadzer_Energi_Transform = transform.Find("MenadzerEnergiOpener");
        UI_Menadzer_Budowania_Transform = transform.Find("BudowanieOpener");
        #endregion
        #region Wy³¹czenie domyslne tych transformów 
        UI_Menadzer_Energi_Transform.gameObject.SetActive(true);
        UI_Menadzer_Budowania_Transform.gameObject.SetActive(true);
        #endregion
        #region Pobranie przycisków dzieci 
        UI_Menadzer_Budowania_Button = UI_Menadzer_Budowania_Transform.Find("Button").GetComponent<Button>();
        UI_Menadzer_Energi_Button = UI_Menadzer_Energi_Transform.Find("Button").GetComponent<Button>();
        
        #endregion 


    }
    private void Start()
    {
        #region Dodanie s³uchaczy na przyciski 
        UI_Menadzer_Budowania_Button.onClick.AddListener(() => { if (StatusMenuBudowy()) { DeaktywujMenuBudowy(); } else { AktywujMenuBudowy(); } });
        UI_Menadzer_Energi_Button.onClick.AddListener(() => { if (StatusMenuEnergi()) { DeaktywujMenuEnergi(); } else { AktywujMenuEnergi(); } });
        #endregion
    }
    private void AktywujMenuEnergi()
    {
        UI_Menadzer_Energia_Object_transform.gameObject.SetActive(true);
    }
    private void DeaktywujMenuEnergi()
    {
        UI_Menadzer_Energia_Object_transform.gameObject.SetActive(false);
    }
    private bool StatusMenuEnergi()
    {
        return UI_Menadzer_Energia_Object_transform.gameObject.activeSelf;
    }
    private void AktywujMenuBudowy()
    {
        UI_Menadzer_Budowanie_Object_transform.gameObject.SetActive(true);
    }
    private void DeaktywujMenuBudowy()
    {
        UI_Menadzer_Budowanie_Object_transform.gameObject.SetActive(false);
        Budowanie_Grid.Instance.DeaktywujBudowanie();
    }
    private bool StatusMenuBudowy()
    {
        return UI_Menadzer_Budowanie_Object_transform.gameObject.activeSelf;
    }
}
