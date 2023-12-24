using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_tooltip_content : MonoBehaviour
{
    public Wieze_SO wieza_Wieza_SO;
    private List<StartowaIloscSur> holderSur_Lista;
    // Start is called before the first frame update
    void Awake()
    {

        
    }
    private void Start()
    {
        Transform surowce = transform.Find("template");
        surowce.gameObject.SetActive(false);
        holderSur_Lista = wieza_Wieza_SO.koszt_StartowaIloscSur_Lista;
        int i = 0;
        int offset = 30;
        transform.Find("nazwa_wiezy_text").GetComponent<TextMeshProUGUI>().SetText(wieza_Wieza_SO.wieza_Nazwa);
        string wynik;
        if (wieza_Wieza_SO.wieza_zasiegataku_float > 0) 
        { 
             wynik = "Obszarowy: " + wieza_Wieza_SO.wieza_zasieg_ataku_amunicji_Float.ToString(); 
        }
        else
        {
            wynik = "Pojedyñczy cel";
        }
        transform.Find("Rodzaj_ataku_string").GetComponent<TextMeshProUGUI>().SetText(wynik);
        transform.Find("Zasiêg ataku_liczba").GetComponent<TextMeshProUGUI>().SetText(wieza_Wieza_SO.wieza_zasiegataku_float.ToString());
        transform.Find("obrazenia_liczba").GetComponent<TextMeshProUGUI>().SetText(wieza_Wieza_SO.Obrazenia_wiezy_Float.ToString());
        transform.Find("przeladowanie_liczba").GetComponent<TextMeshProUGUI>().SetText(wieza_Wieza_SO.Czas_przeladowania_wiezy_Float.ToString()+"s");
        foreach (StartowaIloscSur sur in holderSur_Lista)
        {
            Transform btnTransform = Instantiate(surowce, transform);
            btnTransform.Find("Image").GetComponent<Image>().sprite = sur.surowiec.surowiec_sprite;
            btnTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(sur.ilosc.ToString());
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-27 + (i * offset), -120);
            btnTransform.gameObject.SetActive(true);
            i += 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
