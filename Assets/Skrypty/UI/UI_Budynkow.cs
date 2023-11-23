using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Budynkow : MonoBehaviour
{
    private Transform templateTransform;
    public Button przycisk_dodaj_energia_button;
    public Button przycisk_odejmij_energia_button;
    public Surowce_SO surowiec;
    public TextMeshProUGUI nazwa_kopalni_Text;
    public TextMeshProUGUI aktualnailoscenergi_kopalni_Text; 
    private Transform statystykaTransform;
    public TextMeshProUGUI Statystyka_Text;
    public GeneratorSurowcow generator;
    public Image pasek_image;
    // Start is called before the first frame update
    void Start()
    {
        
        templateTransform = transform.Find("Sekcja_zasilania_"+generator.surowiecGenerowany.surowiec_nazwa_String);
        Debug.Log("Sekcja_zasilania_" + generator.surowiecGenerowany.surowiec_nazwa_String);
        aktualnailoscenergi_kopalni_Text = templateTransform.transform.Find("aktualna_ilosc_energi_wkopalni_text").GetComponent<TextMeshProUGUI>();
        przycisk_odejmij_energia_button = templateTransform.Find("odejmij_energi_button").GetComponent<Button>();
        przycisk_dodaj_energia_button = templateTransform.Find("dodaj_energi_button").GetComponent<Button>();
        przycisk_dodaj_energia_button.onClick.AddListener(() =>
        {
            if (generator.getIloscEnergi() >= 0 && MechanikaEnergi.Instance.Get_Obecna_ilosc_energi() >0)
            {
                generator.zmienIloscEnergi(1);
                MechanikaEnergi.Instance.Odejmij_Energi(1);
                AktualizacjaIloscEnergiWObiekcie();
            }
            else
            {
                Debug.Log("brak energi w obiekcie");
            }
        });
        przycisk_odejmij_energia_button.onClick.AddListener(() =>
         {
             if (generator.getIloscEnergi() >= 1)
             {
                 generator.zmienIloscEnergi(-1);
                 MechanikaEnergi.Instance.Dodaj_Energi(1);
                 AktualizacjaIloscEnergiWObiekcie();
             }
             else
             {
                 Debug.Log("brak energi w obiekcie");
             }

         });


        #region Statystyki generatora UI 
        statystykaTransform = templateTransform.Find("Sekcja_generatora");
        Statystyka_Text = statystykaTransform.Find("Statystyka").GetComponent<TextMeshProUGUI>();
        pasek_image = statystykaTransform.Find("Image").Find("pasek").GetComponent<Image>();
        generator.ZmianaTimeraEvent += Generator_ZmianaTimeraEvent;
        #endregion
    }

    private void Generator_ZmianaTimeraEvent(object sender, System.EventArgs e)
    {
        if (generator.getTimerMax() != 0)
        {
            Statystyka_Text.SetText("1 /" + generator.getTimerMax().ToString() + "s");
            pasek_image.fillAmount = 1 - (generator.GetTimer() / generator.getTimerMax());
        }
        else
        {
            Statystyka_Text.SetText("Wy³¹czony");
            pasek_image.fillAmount = 0;
        }
    }

    private void AktualizacjaIloscEnergiWObiekcie()
    {
        aktualnailoscenergi_kopalni_Text.SetText(generator.getIloscEnergi().ToString());
    }
 
    // Update is called once per frame

}
