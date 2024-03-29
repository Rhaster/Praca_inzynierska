using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Budynkow : MonoBehaviour
{
    private Transform schemat_Transform;
    public Button przycisk_dodaj_energia_button;
    public Button przycisk_odejmij_energia_button;
    public Surowce_SO surowiec_Surowce_SO;
    public TextMeshProUGUI nazwa_kopalni_Text;
    public TextMeshProUGUI aktualnailoscenergi_kopalni_Text; 
    private Transform statystyka_Transform;
    public TextMeshProUGUI Statystyka_Text;
    public GeneratorSurowcow generator;
    public GeneratorAmunicji generatorAmunicji;
    public Image pasek_image;
    public bool czyFabryka_Bool; // do zmiany zachowania gdy bedzie to fabryka
                                 // Start is called before the first frame update
    #region przyciski dla fabryk
    public Button przycisk_produkuj_amunicje1_button;
    public Button przycisk_produkuj_amunicje2_button;
    public Button przycisk_produkuj_amunicje3_button;
    #endregion
    #region amunicja
    private Lista_Amunicja_SO surowce_Lista;
    #endregion
    #region transform selectu amunicji 
    private Transform wybrany_Transform;
    #endregion
    void Start()
    {

 
        if (!czyFabryka_Bool)
        {
            schemat_Transform = transform.Find("Sekcja_zasilania_" + generator.surowiecGenerowany.surowiec_nazwa_String);
        }
        else
        {
            schemat_Transform = transform.Find("Sekcja_Fabryki");
            wybrany_Transform = schemat_Transform.Find("Sekcja_generatora").Find("selected");
            wybrany_Transform.gameObject.SetActive(false);
        }
        aktualnailoscenergi_kopalni_Text = schemat_Transform.transform.Find("aktualna_ilosc_energi_wkopalni_text").GetComponent<TextMeshProUGUI>();
        przycisk_odejmij_energia_button = schemat_Transform.Find("odejmij_energi_button").GetComponent<Button>();
        przycisk_dodaj_energia_button = schemat_Transform.Find("dodaj_energi_button").GetComponent<Button>();
        przycisk_dodaj_energia_button.onClick.AddListener(() =>
        {
            if (!czyFabryka_Bool)
            {
                if (generator.getIloscEnergi() <8 && MechanikaEnergi.Instance.Get_Obecna_ilosc_energi() > 0)
                {

                    if(generator.OgraniczenieDlaGeneratorBool(1))
                    {
                        MechanikaEnergi.Instance.Odejmij_Energi(1);
                        generator.zmienIloscEnergi(1);
                        AktualizacjaIloscEnergiWObiekcie();
                    }

                }
                else
                {
                   // Debug.Log("brak energi w obiekcie");
                }
            }
            else
            {
                if (generatorAmunicji.getIloscEnergi() <8 && MechanikaEnergi.Instance.Get_Obecna_ilosc_energi() > 0)
                {
                    if (generatorAmunicji.OgraniczenieDlaGeneratorBool(1))
                    { 
                    generatorAmunicji.zmienIloscEnergi(1);
                    MechanikaEnergi.Instance.Odejmij_Energi(1);
                    AktualizacjaIloscEnergiWObiekcie();
                }

                }
                else
                {
                   // Debug.Log("brak energi w obiekcie");
                }
            }
        });
        przycisk_odejmij_energia_button.onClick.AddListener(() =>
         {
             if (!czyFabryka_Bool)
             {
                 if (generator.getIloscEnergi() >= 0 )
                 {
                     if (generator.OgraniczenieDlaGeneratorBool(-1))
                     {
                         generator.zmienIloscEnergi(-1);
                         MechanikaEnergi.Instance.Dodaj_Energi(1);
                         AktualizacjaIloscEnergiWObiekcie();
                     }

                 }
                 else
                 {
                     //Debug.Log("brak energi w obiekcie");
                 }
             }
             else
             {
                 if (generatorAmunicji.getIloscEnergi() >= 0)
                 {

                     if (generatorAmunicji.OgraniczenieDlaGeneratorBool(-1))
                     {
                         generatorAmunicji.zmienIloscEnergi(-1);
                         MechanikaEnergi.Instance.Dodaj_Energi(1);
                         AktualizacjaIloscEnergiWObiekcie();
                     }

                 }
                 else
                 {
                     //Debug.Log("brak energi w obiekcie");
                 }
             }
         });


        #region Statystyki generatora UI 
        statystyka_Transform = schemat_Transform.Find("Sekcja_generatora");
        Statystyka_Text = statystyka_Transform.Find("Statystyka").GetComponent<TextMeshProUGUI>();
        pasek_image = statystyka_Transform.Find("Image").Find("pasek").GetComponent<Image>();
        if(!czyFabryka_Bool)
        {
            generator.ZmianaTimeraEvent += Generator_ZmianaTimeraEvent;
        }
        else
        {
            generatorAmunicji.ZmianaTimeraEvent += Generator_ZmianaTimeraEvent;
        }
        #endregion
        #region Kontrola Fabryki
        if(czyFabryka_Bool)
        {
            Transform grabberButton1 = statystyka_Transform.Find("Button_1");
            Transform grabberButton2 = statystyka_Transform.Find("Button_2");
            Transform grabberButton3 = statystyka_Transform.Find("Button_3");
            przycisk_produkuj_amunicje1_button = grabberButton1.GetComponent<Button>();
            przycisk_produkuj_amunicje2_button = grabberButton2.GetComponent<Button>();
            przycisk_produkuj_amunicje3_button = grabberButton3.GetComponent<Button>();

            przycisk_produkuj_amunicje1_button.onClick.AddListener(() => {
                if (GeneratorAmunicji.Instance.amunicjaGenerowany != surowce_Lista.amunicja_Lista[0])
                {
                    GeneratorAmunicji.Instance.amunicjaGenerowany = surowce_Lista.amunicja_Lista[0];
                    AktywujSelekcje(grabberButton1.position);
                }
                else
                {
                    GeneratorAmunicji.Instance.amunicjaGenerowany = null;
                    DeaktywujSelekcje();
                }
            });
            przycisk_produkuj_amunicje2_button.onClick.AddListener(() => {
                if (GeneratorAmunicji.Instance.amunicjaGenerowany != surowce_Lista.amunicja_Lista[1])
                {
                    GeneratorAmunicji.Instance.amunicjaGenerowany = surowce_Lista.amunicja_Lista[1];
                    AktywujSelekcje(grabberButton2.position);
                }
                else
                {
                    GeneratorAmunicji.Instance.amunicjaGenerowany = null;
                    DeaktywujSelekcje();
                }
               
            
            
            });
            przycisk_produkuj_amunicje3_button.onClick.AddListener(() => {
                if (GeneratorAmunicji.Instance.amunicjaGenerowany != surowce_Lista.amunicja_Lista[2])
                {
                    GeneratorAmunicji.Instance.amunicjaGenerowany = surowce_Lista.amunicja_Lista[2];
                    AktywujSelekcje(grabberButton3.position);
                }
                else
                {
                    GeneratorAmunicji.Instance.amunicjaGenerowany = null;
                    DeaktywujSelekcje();
                }
            });
        }
        #endregion
    }
    private void AktywujSelekcje(Vector3 pos)
    {
        wybrany_Transform.gameObject.SetActive(true);
        wybrany_Transform.GetComponent<RectTransform>().position = pos;
    }
    private void DeaktywujSelekcje()
    {
        wybrany_Transform.gameObject.SetActive(false);
    }
    private void Awake()
    {
        surowce_Lista = Resources.Load<Lista_Amunicja_SO>("Amunicja_Lista");
    }
    private void Generator_ZmianaTimeraEvent(object sender, System.EventArgs e)
    {
        if (!czyFabryka_Bool)
        {

            if (generator.getTimerMax() != 0)
            {
                Statystyka_Text.SetText("1/" + generator.getTimerMax().ToString() + "s");
                pasek_image.fillAmount = 1 - (generator.GetTimer() / generator.getTimerMax());
            }
            else
            {
                Statystyka_Text.SetText("Wy��czony");
                pasek_image.fillAmount = 0;
            }
        }
        else
        {
            if (generatorAmunicji.getTimerMax() != 0)
            {
                Statystyka_Text.SetText("1/" + generatorAmunicji.getTimerMax().ToString() + "s");
                pasek_image.fillAmount = 1 - (generatorAmunicji.GetTimer() / generatorAmunicji.getTimerMax());
            }
            else
            {
                Statystyka_Text.SetText("Wy��czony");
                pasek_image.fillAmount = 0;
            }
        }
    }

    private void AktualizacjaIloscEnergiWObiekcie()
    {
        if (!czyFabryka_Bool)
        {
            aktualnailoscenergi_kopalni_Text.SetText(generator.getIloscEnergi().ToString());
        }
        else {
            aktualnailoscenergi_kopalni_Text.SetText(generatorAmunicji.getIloscEnergi().ToString());
                }
    }

    // Update is called once per frame
    #region kontrola wyboru amunicji w fabryce poprzez klawisze
    private void Update()
    {
        if (czyFabryka_Bool)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                przycisk_produkuj_amunicje1_button.onClick.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                przycisk_produkuj_amunicje2_button.onClick.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                przycisk_produkuj_amunicje3_button.onClick.Invoke();
            }
            
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            przycisk_dodaj_energia_button.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            przycisk_odejmij_energia_button.onClick.Invoke();
        }
    }
    #endregion
}
