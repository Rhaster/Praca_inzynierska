using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_Amunicja : MonoBehaviour
{
    private Lista_Amunicja_SO surowce_Lista;
    private Dictionary<Amunicja_SO, Transform> surowce_Slownik;
    
    private void Awake()
    {
        #region Logika pobierania i tworzenia obiektów w ui
        surowce_Lista = Resources.Load<Lista_Amunicja_SO>("Amunicja_Lista");
        surowce_Slownik = new Dictionary<Amunicja_SO, Transform>();
        Transform resourceTemplate = transform.Find("AmunicjaTemplate");
        resourceTemplate.gameObject.SetActive(false);
        int index = 0;
        foreach (Amunicja_SO surowiec in surowce_Lista.amunicja_Lista)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);

            float offsetAmount = 75f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            resourceTransform.Find("icon").GetComponent<Image>().sprite = surowiec.amunicja_Sprite;

            surowce_Slownik[surowiec] = resourceTransform;

            index++;
        }
        
        #endregion
    }

    private void Start()
    {
        MechanikaAmunicji.Instance.ZmianaIlosciAmunicji += Instance_ZmianaIlosciAmunicji;

    }

    private void Instance_ZmianaIlosciAmunicji(object sender, System.EventArgs e)
    {
        Aktualizuj_Ilosc_surowcow();
    }

    private void Aktualizuj_Ilosc_surowcow()
    {
        foreach (Amunicja_SO surowiec in surowce_Lista.amunicja_Lista)
        {
            Transform resourceTransform = surowce_Slownik[surowiec];

            int surowce_Ilosc_Int = MechanikaAmunicji.Instance.GetIloscAmunicji(surowiec);
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(surowce_Ilosc_Int.ToString());
        }
    }
}
