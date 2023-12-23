using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class UI_MenadzerEnergi : MonoBehaviour
{

    #region Deklaracje zmiennych Transform
    public  static UI_MenadzerEnergi Instance { get; private set; }
    public Transform UI_Menadzer_Energi_Object_transform;
    public Transform templatka;
    public Transform rodzictemplatka;
    #endregion
    #region Deklaracje zmiennych Button
    [SerializeField] private Button UI_Menadzer_Energi_ResetujPrad;
    [SerializeField] private Button UI_Menadzer_Budowania_Button;
    #endregion
    #region Deklaracja slownika do przechowywania odpowiednio obiektu i transformu jego
    private Dictionary<GeneratorSurowcow, Transform> Transform_Slownik;
    private Dictionary<GeneratorSurowcow, Image> Image_Slownik;
    #endregion
    #region Deklaracja listy do przechowywania generatorow
    [SerializeField]private List<GeneratorSurowcow> listaGeneratorow;
    #endregion
    #region zmienne do pozycjonowania
    [SerializeField]int index;
    [SerializeField] float odleglosc;
    #endregion
    Image holder;
    public GeneratorAmunicji holder_Generatora_Amunicji;
    #region Transformy wskaznikow energi
    public Transform zuzywanaEnergia_Transform;
    public Transform maksymalnaEnergia_Transform;
    private Image slup_zuzEnergia_Image;
    private Image slup_maxEnergia_Image;
    #endregion
    private void Awake()
    {
        Instance = this;
        index = 0;
        List<GeneratorSurowcow> listaGeneratorow = new List<GeneratorSurowcow>();
        Transform[] dzieci = UI_Menadzer_Energi_Object_transform.GetComponentsInChildren<Transform>();

        // Iteruj przez wszystkie dzieci i sprawdü, czy majπ komponent GeneratorSurowcow
        foreach (Transform dziecko in dzieci)
        {
            GeneratorSurowcow generator = dziecko.GetComponent<GeneratorSurowcow>();
            // Jeúli obiekt ma komponent GeneratorSurowcow, dodaj go do listy
            if (generator != null)
            {
                listaGeneratorow.Add(generator);
            }

        }
        Transform_Slownik = new Dictionary<GeneratorSurowcow, Transform>();
        Image_Slownik = new Dictionary<GeneratorSurowcow, Image>();
        templatka.gameObject.SetActive(false);
        
        //MouseEnterExit mouseEnterExitEvents = templatka.GetComponent<MouseEnterExit>();
        foreach (GeneratorSurowcow buildingType in listaGeneratorow)
        {
            Transform btnTransform = Instantiate(templatka, transform);
            btnTransform.SetParent(rodzictemplatka);
            
            btnTransform.gameObject.SetActive(true);
            odleglosc = -40f;
            holder = btnTransform.Find("Bar").Find("Image").GetComponent<Image>();
            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 129 +  odleglosc * index);
            holder.fillAmount = buildingType.getIloscEnergi()/8;
            btnTransform.Find("nazwa_obiektu").GetComponent<TextMeshProUGUI>().SetText(buildingType.nazwa_kopalni);
            Transform_Slownik[buildingType] = btnTransform;
            Image_Slownik[buildingType] = holder;
            index++;
             
        }

            Transform btnTransforms = Instantiate(templatka, transform);
            btnTransforms.SetParent(rodzictemplatka);

            btnTransforms.gameObject.SetActive(true);
            odleglosc = -40f;
            holder = btnTransforms.Find("Bar").Find("Image").GetComponent<Image>();
            btnTransforms.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 129 + odleglosc * index);
            holder.fillAmount = holder_Generatora_Amunicji.getIloscEnergi() / 8;
            btnTransforms.Find("nazwa_obiektu").GetComponent<TextMeshProUGUI>().SetText(holder_Generatora_Amunicji.nazwa_kopalni);
        zuzywanaEnergia_Transform = transform.Find("Template_zuzywana_energia");
        maksymalnaEnergia_Transform = transform.Find("Template_aktualna_energia");
        slup_zuzEnergia_Image = zuzywanaEnergia_Transform.Find("Bar").Find("slupek").GetComponent<Image>();
        slup_maxEnergia_Image = maksymalnaEnergia_Transform.Find("Bar").Find("slupek").GetComponent<Image>();
        index++;

       
    }

    public void Aktualizuj_bar_UI_Menadzera_energi(GeneratorSurowcow e,GeneratorAmunicji a)
    {
        float k = (float)((float)MechanikaEnergi.Instance.Get_Maxymalna_ilosc_energi() - (float)MechanikaEnergi.Instance.Get_Obecna_ilosc_energi())
     / (float)MechanikaEnergi.Instance.Get_Maxymalna_ilosc_energi();
        float t = (float)((float)MechanikaEnergi.Instance.Get_Obecna_ilosc_energi())
            / (float)MechanikaEnergi.Instance.Get_Maxymalna_ilosc_energi();
       // Debug.Log(k);
       // Debug.Log(t);
        slup_zuzEnergia_Image.fillAmount = k;
        slup_maxEnergia_Image.fillAmount = t;
        if (e != null)
        {
            Image_Slownik[e].fillAmount = e.getIloscEnergi() / 8;
        }
        if(a!=null)
        {
            holder.fillAmount = a.getIloscEnergi() / 8;
        }

 
    }
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void UsunZMenadzeraEnergi()
    {

    }
    public void DodajDoMenadzeraEnergi()
    {

    }
}

/*
 *  btnTransform.Find("Nazwa_wiezy").GetComponent<TextMeshProUGUI>().SetText(buildingType.wieza_Nazwa);
            btnTransform.GetComponent<Button>().onClick.AddListener(() => {
                MechanikaBudowania.Instance.SetActiveBuildingType(buildingType);
                UpdateActiveBuildingTypeButton();
            });
            mouseEnterExitEvents = btnTransform.GetComponent<MouseEnterExit>();
            mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) => {
                //TooltipUI.Instance.Show(buildingType.nameString + "\n" + buildingType.GetConstructionResourceCostString());
            };
            mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) => {
                //TooltipUI.Instance.Hide();
            };
*/