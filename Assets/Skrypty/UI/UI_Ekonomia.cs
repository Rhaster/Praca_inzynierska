using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_Ekonomia : MonoBehaviour
{
    private Lista_Surowce_SO resourceTypeList;
    private Dictionary<Surowce_SO, Transform> resourceTypeTransformDictionary;

    private void Awake()
    {
        resourceTypeList = Resources.Load<Lista_Surowce_SO>("Surowce_Lista");
        resourceTypeTransformDictionary = new Dictionary<Surowce_SO, Transform>();
        Transform resourceTemplate = transform.Find("SurowceTemplate"); 
        resourceTemplate.gameObject.SetActive(false);
        int index = 0;
        foreach (Surowce_SO resourceType in resourceTypeList.surowce_lista)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);

            float offsetAmount = -105f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            resourceTransform.Find("icon").GetComponent<Image>().sprite = resourceType.surowiec_sprite;

            resourceTypeTransformDictionary[resourceType] = resourceTransform;

            index++;
        }
    }

    private void Start()
    {
        MechanikaEkonomi.Instance.ZmianaIlosciSurowcow += Instance_ZmianaIlosciSurowcow;

        UpdateResourceAmount();
    }

    private void Instance_ZmianaIlosciSurowcow(object sender, System.EventArgs e)
    {
        UpdateResourceAmount();
    }


    private void UpdateResourceAmount()
    {
        foreach (Surowce_SO resourceType in resourceTypeList.surowce_lista)
        {
            Transform resourceTransform = resourceTypeTransformDictionary[resourceType];

            int resourceAmount =MechanikaEkonomi.Instance.GetIloscSurowca(resourceType);
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }
}
