using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_wrogowie_info : MonoBehaviour
{
    [SerializeField] private wrog lokalny_Wrog;
    [SerializeField] private Wrog_holder lokalny_Wrog_SO;
    [SerializeField] private SystemHP lokalny_SystemHP;
    // Start is called before the first frame update
    #region zmienne elementy ui
    private Image wskaznik_hp_Image;
    private TextMeshProUGUI ilosc_hp_Text;
    private TextMeshProUGUI nazwa_Text;
    [SerializeField]private Transform sekcja_informacnji_Transform;
    #endregion
    private void Awake()
    {
        sekcja_informacnji_Transform.gameObject.SetActive(false);
    }
    void Start()
    {
        lokalny_Wrog = GetComponent<wrog>();
        lokalny_SystemHP = GetComponent<SystemHP>();
        wskaznik_hp_Image = sekcja_informacnji_Transform.Find("bar_image").GetComponent<Image>();
        ilosc_hp_Text = sekcja_informacnji_Transform.Find("ilosc_hp_text").GetComponent<TextMeshProUGUI>();
        lokalny_SystemHP.Zadano_Obrazenia += Lokalny_SystemHP_OnDamaged;
        nazwa_Text = sekcja_informacnji_Transform.Find("nazwa").GetComponent<TextMeshProUGUI>();
        lokalny_Wrog_SO = GetComponent<Wrog_holder>();
        nazwa_Text.SetText(lokalny_Wrog_SO.wrog_Wrogowie_SO.wrog_Nazwa);
        wskaznik_hp_Image.fillAmount = lokalny_SystemHP.GetHealthAmountNormalized();
        ilosc_hp_Text.SetText(lokalny_SystemHP.GetHealthAmount().ToString());
    }

    private void Lokalny_SystemHP_OnDamaged(object sender, System.EventArgs e)
    {
        wskaznik_hp_Image.fillAmount = lokalny_SystemHP.GetHealthAmountNormalized();
        ilosc_hp_Text.SetText(lokalny_SystemHP.GetHealthAmount().ToString());

        }
    private void OnMouseDown()
    {
        if(!sekcja_informacnji_Transform.gameObject.activeSelf)
        {
            Dezaktywacja();
            sekcja_informacnji_Transform.gameObject.SetActive(true);
        }
        else
        {
            Dezaktywacja();
        }
    }
    public void Dezaktywacja()
    {
        GameObject[] obiektyZTagiem = GameObject.FindGameObjectsWithTag("UI_wroga");

        foreach (GameObject obiekt in obiektyZTagiem)
        {
            obiekt.SetActive(false);
        }
    }

}
