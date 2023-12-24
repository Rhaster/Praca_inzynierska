using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_wieze : MonoBehaviour
{
    private Image wybrana_wieza_Transform;
    private Dictionary<Wieze_SO, Transform> btnTransformDictionary;
    private Transform arrowBtn;

    private void Awake()
    {

        Transform btnTemplate = transform.Find("Template");
        btnTemplate.gameObject.SetActive(false);

        Lista_Wieze_SO wieze_Lista = Resources.Load<Lista_Wieze_SO>("Wieze_Lista");

        btnTransformDictionary = new Dictionary<Wieze_SO, Transform>();

        int index = 0;

        arrowBtn = Instantiate(btnTemplate, transform);
        arrowBtn.gameObject.SetActive(false);

        float offsetAmount = +130f;


        foreach (Wieze_SO buildingType in wieze_Lista.Wieze_Lista)
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
                MechanikaBudowania.Instance.SetActiveBuildingType(buildingType);
                UpdateActiveBuildingTypeButton();
            });
            

            btnTransformDictionary[buildingType] = btnTransform;

            index++;
        }
    }

    private void Start()
    {
        MechanikaBudowania.Instance.SetActiveBuildingType(null);
        UpdateActiveBuildingTypeButton();

    }

    

    private void UpdateActiveBuildingTypeButton()
    {
        arrowBtn.Find("wybrany").gameObject.SetActive(false);
        foreach (Wieze_SO buildingType in btnTransformDictionary.Keys)
        {
            Transform btnTransform = btnTransformDictionary[buildingType];
            btnTransform.Find("wybrany").gameObject.SetActive(false);
        }

        Wieze_SO activeBuildingType = MechanikaBudowania.Instance.GetActiveBuildingType();
        if (activeBuildingType == null)
        {
            //Debug.Log("aktywna wieza to null");
        }
        else
        {
            btnTransformDictionary[activeBuildingType].Find("wybrany").gameObject.SetActive(true);
            //Debug.Log("aktywna wieza to"+activeBuildingType.wieza_Nazwa);
        }
    }
    private void OnEnable()
    {
        
       
    }
    private void OnDisable()
    {
        MechanikaBudowania.Instance.SetActiveBuildingType(null);
    }
}
