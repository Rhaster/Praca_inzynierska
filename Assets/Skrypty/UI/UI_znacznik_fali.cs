using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class UI_znacznik_fali : MonoBehaviour
{
    private Transform sklad_fali_Transform;
    private Transform sklad_fali_zawartosc_Transform;
    private Transform pancerz_fali_Transform;
    private Transform pancerz_fali_zawartosc_Transform;
    private Image wrog_Image;
    // Start is called before the first frame update
    private void Awake()
    {
        sklad_fali_Transform = transform.Find("sklad_fali");
        sklad_fali_zawartosc_Transform = sklad_fali_Transform.Find("template");
        pancerz_fali_Transform = transform.Find("pancerz_fali");
        pancerz_fali_zawartosc_Transform = pancerz_fali_Transform.Find("template");
    }
    void Start()
    {
        Wyznacznik_fali.instance.Wyznaczenie_fali_ui_event += Instance_Wyznaczenie_fali_ui_event;
        Wyznacznik_fali.instance.Wyznaczenie_pancerza_ui_event += Instance_Wyznaczenie_pancerza_ui_event;
    }

    private void Instance_Wyznaczenie_pancerza_ui_event(object sender, Wyznacznik_fali.Holder_pancerza e)
    {
        Wy³¹czDzieci(pancerz_fali_Transform, "template(Clone)");
        int i = 0;
        int offset = 60;
        foreach(Amunicja_SO s in e.amunicja_Dictionary.Keys)
        {
            Transform x = Instantiate(pancerz_fali_zawartosc_Transform, pancerz_fali_zawartosc_Transform.position, Quaternion.identity, pancerz_fali_Transform.transform);

            x.Find("Image").GetComponent<Image>().sprite = s.amunicja_Sprite;
            x.Find("ilosc_text").GetComponent<TextMeshProUGUI>().SetText(e.amunicja_Dictionary[s].ToString());
            x.GetComponent<RectTransform>().anchoredPosition = new Vector2(-4 + (i * offset), -10);
            x.gameObject.SetActive(true);
            i += 1;
        }
        pancerz_fali_zawartosc_Transform.gameObject.SetActive(false);
    }

    private void Instance_Wyznaczenie_fali_ui_event(object sender, Wyznacznik_fali.Holder_fali e)
    {
        Wy³¹czDzieci(sklad_fali_Transform, "template(Clone)");
        Dictionary<string, int> slownik = StworzSlownik(e.wrogow_List);
        int i = 0;
        int offset = 70;
        int h = 0;
        foreach (string a in slownik.Keys)
        {
            Transform x = Instantiate(sklad_fali_zawartosc_Transform, sklad_fali_zawartosc_Transform.position, Quaternion.identity, sklad_fali_Transform.transform);

            x.Find("Image").GetComponent<Image>().sprite = Resources.Load<Transform>("pf_wrog_" + a).Find("sprite").GetComponent<SpriteRenderer>().sprite;
            x.Find("ilosc_text").GetComponent<TextMeshProUGUI>().SetText(slownik[a].ToString());
            if (h==2) {
                i += 1;
                h = 0;
            }
            x.GetComponent<RectTransform>().anchoredPosition = new Vector2(-50 + (90*(h)), -20 - (i * offset));
            h += 1;
            x.gameObject.SetActive(true);

        }
        sklad_fali_zawartosc_Transform.gameObject.SetActive(false);
    }

    private void Ustaw_UI_Pancerza_fali()
    {

    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
    Dictionary<string, int> StworzSlownik(List<string> lista)
    {
        Dictionary<string, int> slownik = new Dictionary<string, int>();

        foreach (string element in lista)
        {
            if (slownik.ContainsKey(element))
            {
                slownik[element]++;
            }
            else
            {
                slownik[element] = 1;
            }
        }

        return slownik;
    }
    void Wy³¹czDzieci(Transform parent, string nazwaObiektu)
    {
        // Iteruj przez wszystkie dzieci danego transformu
        foreach (Transform child in parent)
        {
            // SprawdŸ, czy nazwa dziecka jest równa oczekiwanej nazwie
            if (child.name == nazwaObiektu)
            {
                // Wy³¹cz dziecko
                child.gameObject.SetActive(false);
            }

            // Rekurencyjnie wywo³aj tê sam¹ funkcjê dla potomków tego dziecka
            Wy³¹czDzieci(child, nazwaObiektu);
        }
    }
}
