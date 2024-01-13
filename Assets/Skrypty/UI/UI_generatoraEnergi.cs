using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_generatoraEnergi : MonoBehaviour
{
    private Transform UI_genEnergi_Transform;
    public TextMeshProUGUI wartosc_energi_Text;
    public TextMeshProUGUI wartosc_poziomu_Text;
    private Button DodanieMaxEnergi;
    private Button OdjecieMaxEnergi;
    public List<StartowaIloscSur> KoszZwiekszeniaEnergi;
    public List<StartowaIloscSur> KoszZmniejszeniaEnergi;
    private Transform template_kosztu_zwiek_energi_Transform;
    private Transform template_kosztu_zmniej_energi_Transform;
    private int poziom;
    // Start is called before the first frame update
    private void Awake()
    {
        poziom = 0;
        UI_genEnergi_Transform = transform.Find("Sekcja_Generowana");
        wartosc_energi_Text = UI_genEnergi_Transform.Find("ilosc_max_energi").GetComponent<TextMeshProUGUI>();
        wartosc_poziomu_Text = UI_genEnergi_Transform.Find("PoziomGeneratora_text").GetComponent<TextMeshProUGUI>(); 
        DodanieMaxEnergi = UI_genEnergi_Transform.Find("dodaj_energi_button").GetComponent<Button>();
        OdjecieMaxEnergi = UI_genEnergi_Transform.Find("odejmij_energi_button").GetComponent<Button>();
        template_kosztu_zwiek_energi_Transform = UI_genEnergi_Transform.Find("template");
        template_kosztu_zwiek_energi_Transform.gameObject.SetActive(false);
        template_kosztu_zmniej_energi_Transform = UI_genEnergi_Transform.Find("template_sell");
        template_kosztu_zmniej_energi_Transform.gameObject.SetActive(false);
    }
    void Start()
    {
        AktualizacjaTextu();
        DodanieMaxEnergi.onClick.AddListener(() =>
        {
            if (MechanikaEkonomi.Instance.CzyStac(KoszZwiekszeniaEnergi))
            {
                poziom += 1;
                MechanikaEkonomi.Instance.WydajSurowce(KoszZwiekszeniaEnergi);
                ZwiekszMaxEnergie();
            }
        });
        OdjecieMaxEnergi.onClick.AddListener(() =>
        {
            if (poziom >0&& MechanikaEnergi.Instance.Get_Obecna_ilosc_energi() >0)
            {
                poziom -= 1;
                MechanikaEkonomi.Instance.DodajSurowce(KoszZmniejszeniaEnergi);
                ZmniejszMaxIloscEnergi();
            }
        });
        int i = 0;
        int offset = 30;
        foreach (StartowaIloscSur sur in KoszZwiekszeniaEnergi)
        {
            Transform btnTransform = Instantiate(template_kosztu_zwiek_energi_Transform, transform);
            btnTransform.Find("Image").GetComponent<Image>().sprite = sur.surowiec.surowiec_sprite;
            btnTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(sur.ilosc.ToString());
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-30+(i * offset), -47);
            btnTransform.gameObject.SetActive(true);
            i += 1;
        }
        i += 1;
        foreach (StartowaIloscSur sur in KoszZmniejszeniaEnergi)
        {
            Transform btnTransform = Instantiate(template_kosztu_zmniej_energi_Transform, transform);
            btnTransform.Find("Image").GetComponent<Image>().sprite = sur.surowiec.surowiec_sprite;
            btnTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(sur.ilosc.ToString());
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-50+( i * offset), -47);
            btnTransform.gameObject.SetActive(true);
            i += 1;
        }

    }
    private void AktualizacjaTextu()
    {
        wartosc_energi_Text.SetText(MechanikaEnergi.Instance.Get_Maxymalna_ilosc_energi().ToString());
        wartosc_poziomu_Text.SetText(poziom.ToString());
    }
    private void ZwiekszMaxEnergie()
    {
        MechanikaEnergi.Instance.ZwiekszMaxIloscEnergi();
        AktualizacjaTextu();
    }
    private void ZmniejszMaxIloscEnergi()
    {
        MechanikaEnergi.Instance.ZmniejszMaxIloscEnergi();
        AktualizacjaTextu();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DodanieMaxEnergi.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            OdjecieMaxEnergi.onClick.Invoke();
        }
    }
}
