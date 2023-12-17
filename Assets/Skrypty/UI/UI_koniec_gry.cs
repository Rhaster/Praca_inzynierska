using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_koniec_gry : MonoBehaviour
{
    private TextMeshProUGUI Wynik_tmpro;
    private TextMeshProUGUI ilosc_zabitych_tmpro;
    private TextMeshProUGUI ilosc_postawionych_wiez_tmpro;
    private TextMeshProUGUI Wynik_ekonomi_tmpro;
    private const string KilledUnitsKey = "KilledUnits";
    private const string postawione_Key = "POSTAWIONEBUDYNKI";
    private const string wydobyte_Key = "Wydobyte";
    int currentKilledUnits;
    int currentpostawioneUnits;
    int currentwydobyteUnits;
    public Button wyjscie;
    // Start is called before the first frame update
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    void Start()
    {
        wyjscie = transform.Find("menu").GetComponent<Button>();
        Wynik_tmpro=transform.Find("wynik").GetComponent<TextMeshProUGUI>();
        ilosc_zabitych_tmpro = transform.Find("il_zab").GetComponent<TextMeshProUGUI>();
        ilosc_postawionych_wiez_tmpro = transform.Find("Post_wieze").GetComponent<TextMeshProUGUI>();
        Wynik_ekonomi_tmpro = transform.Find("wyn_ekonomi").GetComponent<TextMeshProUGUI>();
        currentKilledUnits = PlayerPrefs.GetInt(KilledUnitsKey, 0);
        currentpostawioneUnits = PlayerPrefs.GetInt(postawione_Key, 0);
        currentwydobyteUnits =0;
        ilosc_postawionych_wiez_tmpro.SetText(currentpostawioneUnits.ToString());
        wyjscie.onClick.AddListener(() =>
        { SceneManager.LoadScene("MenuG³ówne"); }
        );
        inicjalizacja();
    }
    private void inicjalizacja()
    {
        Wynik_tmpro.SetText(  MechanikaGameplayu.instance.getWynik());
        ilosc_zabitych_tmpro.SetText(currentKilledUnits.ToString());
        ilosc_postawionych_wiez_tmpro.SetText(currentpostawioneUnits.ToString());
        Wynik_ekonomi_tmpro.SetText(MechanikaEkonomi.Instance.wydobyte_sur().ToString());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
