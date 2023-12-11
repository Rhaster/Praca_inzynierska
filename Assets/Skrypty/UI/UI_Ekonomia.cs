using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_Ekonomia : MonoBehaviour
{
    private Lista_Surowce_SO surowce_Lista;
    private Dictionary<Surowce_SO, Transform> surowce_Slownik;

    private void Awake()
    {
        #region Logika pobierania i tworzenia obiektów w ui
        surowce_Lista = Resources.Load<Lista_Surowce_SO>("Surowce_Lista");
        surowce_Slownik = new Dictionary<Surowce_SO, Transform>();
        Transform resourceTemplate = transform.Find("SurowceTemplate"); 
        resourceTemplate.gameObject.SetActive(false);
        int index = 0;
        foreach (Surowce_SO surowiec in surowce_Lista.surowce_lista)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);
            
            float offsetAmount = 75f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            resourceTransform.Find("icon").GetComponent<Image>().sprite = surowiec.surowiec_sprite;

            surowce_Slownik[surowiec] = resourceTransform;

            index++;
        }
        #endregion
    }

    private void Start()
    {
        MechanikaEkonomi.Instance.ZmianaIlosciSurowcow += Instance_ZmianaIlosciSurowcow;

        Aktualizuj_Ilosc_surowcow();
    }

    private void Instance_ZmianaIlosciSurowcow(object sender, System.EventArgs e)
    {
        Aktualizuj_Ilosc_surowcow();
    }

    public void Aktualizuj_Ilosc_surowcow()
    {
        foreach (Surowce_SO surowiec in surowce_Lista.surowce_lista)
        {
            Transform resourceTransform = surowce_Slownik[surowiec];

            int surowce_Ilosc_Int =MechanikaEkonomi.Instance.GetIloscSurowca(surowiec);
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(surowce_Ilosc_Int.ToString());
        }
        
    }
    
}
