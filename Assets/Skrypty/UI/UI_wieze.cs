using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_wieze : MonoBehaviour
{
    private Image wybrana_wieza_Transform;
    private Dictionary<Wieze_SO, Transform> btn_Transform_Dictionary;
    private Transform arrowBtn;

    private void Awake()
    {

        Transform btnTemplate = transform.Find("Template");
        btnTemplate.gameObject.SetActive(false);


        List<Wieze_SO> wieze_Lista = LadowaniePlayerPrefs.OdczytajListeWiez();
        btn_Transform_Dictionary = new Dictionary<Wieze_SO, Transform>();

        int index = 0;

        arrowBtn = Instantiate(btnTemplate, transform);
        arrowBtn.gameObject.SetActive(false);

        float offsetAmount = +130f;


        foreach (Wieze_SO buildingType in wieze_Lista)
        {
            Transform btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);
            offsetAmount = +110f;
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            btnTransform.Find("image_wieza").GetComponent<Image>().sprite = buildingType.wieza_Sprite;
            wybrana_wieza_Transform = btnTransform.Find("wybrany").GetComponent<Image>();
            wybrana_wieza_Transform.gameObject.SetActive(false) ;
            btnTransform.Find("Nazwa_wiezy").GetComponent<TextMeshProUGUI>().SetText(buildingType.wieza_Nazwa);
            btnTransform.GetComponent<Button>().onClick.AddListener(() => {
                MechanikaBudowania.Instance.Ustaw_aktywny_typ_budowli(buildingType);
                Zaktualiuj_aktywny_rodzaj_budynku();
            });
            

            btn_Transform_Dictionary[buildingType] = btnTransform;

            index++;
        }
    }

    private void Start()
    {
        MechanikaBudowania.Instance.Ustaw_aktywny_typ_budowli(null);
        Zaktualiuj_aktywny_rodzaj_budynku();

    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
            {


            MechanikaBudowania.Instance.Ustaw_aktywny_typ_budowli(null);
            Zaktualiuj_aktywny_rodzaj_budynku();
        }
    }


    private void Zaktualiuj_aktywny_rodzaj_budynku()
    {
        arrowBtn.Find("wybrany").gameObject.SetActive(false);
        foreach (Wieze_SO buildingType in btn_Transform_Dictionary.Keys)
        {
            Transform btnTransform = btn_Transform_Dictionary[buildingType];
            btnTransform.Find("wybrany").gameObject.SetActive(false);
        }

        Wieze_SO activeBuildingType = MechanikaBudowania.Instance.Get_aktywny_budynek();
        if (activeBuildingType == null)
        {
            //Debug.Log("aktywna wieza to null");
        }
        else
        {
            btn_Transform_Dictionary[activeBuildingType].Find("wybrany").gameObject.SetActive(true);
            //Debug.Log("aktywna wieza to"+activeBuildingType.wieza_Nazwa);
        }
    }
    private void OnEnable()
    {
        
       
    }
    private void OnDisable()
    {
        arrowBtn.Find("wybrany").gameObject.SetActive(false);
        foreach (Wieze_SO rodzaj_budynku_Wieze_SO in btn_Transform_Dictionary.Keys)
        {
            Transform btn_Transform = btn_Transform_Dictionary[rodzaj_budynku_Wieze_SO];
            btn_Transform.Find("wybrany").gameObject.SetActive(false);
        }
        MechanikaBudowania.Instance.Ustaw_aktywny_typ_budowli(null);
    }
}
