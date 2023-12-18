using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_generatoraEnergi : MonoBehaviour
{
    private Transform UI_genEnergi_Transform;
    public TextMeshProUGUI wartosc_energi_Text;
    private Button DodanieMaxEnergi;
    public List<StartowaIloscSur> KoszZwiekszeniaEnergi;
    // Start is called before the first frame update
    private void Awake()
    {
        UI_genEnergi_Transform = transform.Find("Sekcja_Generowana");
        wartosc_energi_Text = UI_genEnergi_Transform.Find("Ilosc_energi_text").GetComponent<TextMeshProUGUI>();
        DodanieMaxEnergi = UI_genEnergi_Transform.Find("dodaj_energi_button").GetComponent<Button>();
    }
    void Start()
    {

        DodanieMaxEnergi.onClick.AddListener(() =>
        {
            if (MechanikaEkonomi.Instance.CzyStac(KoszZwiekszeniaEnergi))
            {
                MechanikaEkonomi.Instance.WydajSurowce(KoszZwiekszeniaEnergi);
                ZwiekszMaxEnergie();
            }
        });
    }
    private void AktualizacjaTextu()
    {
        wartosc_energi_Text.SetText(MechanikaEnergi.Instance.Get_Maxymalna_ilosc_energi().ToString());
    }
    private void ZwiekszMaxEnergie()
    {
        MechanikaEnergi.Instance.ZwiekszMaxIloscEnergi();
        AktualizacjaTextu();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
