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
        transform.Find("Rodzaj_ataku_string").GetComponent<TextMeshProUGUI>().SetText(wieza_Wieza_SO.wieza_rodzajataku_Enum.ToString());
        transform.Find("Zasiêg ataku_liczba").GetComponent<TextMeshProUGUI>().SetText(wieza_Wieza_SO.wieza_zasiegataku_float.ToString());
        foreach (StartowaIloscSur sur in holderSur_Lista)
        {
            Transform btnTransform = Instantiate(surowce, transform);
            btnTransform.Find("Image").GetComponent<Image>().sprite = sur.surowiec.surowiec_sprite;
            btnTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(sur.ilosc.ToString());
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-27 + (i * offset), -62);
            btnTransform.gameObject.SetActive(true);
            i += 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
