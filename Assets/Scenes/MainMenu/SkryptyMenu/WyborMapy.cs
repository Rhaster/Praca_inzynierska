using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WyborMapy : MonoBehaviour
{
    public TMP_Dropdown mapa_Dropdown;
    public TMP_Dropdown poziom_trudnosci_Dropdown;
    public TMP_Dropdown fale_Dropdown;
    public Image wyswietlenie_Wybranej_Mapy1;
    public List<Sprite> Sprite_wyswietlenie_Mapy;
    public Transform flowmanager_Transform;
    #region Klucze player prefs 
    private string wybrana_mapa_String = "MAPA";
    private string poziom_trudnosci_klucz_String = "Difficulty";
    private string fale_klucz_String = "FALE";
    #endregion
    private Button powrot_button;
    private Button start_button;
    public Sprite domyslny_mapy_Sprite;
    public Sprite defaultDifficultySprite;
    public List<string> mapy;
    public List<int> iloscFal_Lista;
    private void Awake()
    {
        Zaktualizuj_opcje_dropdown();
        powrot_button = transform.Find("wyjscie").GetComponent<Button>();
        start_button = transform.Find("Start").GetComponent<Button>();
        gameObject.SetActive(false);
    }
    private void Start()
    {
        

        // Ustaw domyœlne wartoœci, jeœli jeszcze nie zapisano wyboru mapy i poziomu trudnoœci.
        if (!PlayerPrefs.HasKey(wybrana_mapa_String))
        {
            mapa_Dropdown.value = 0;
            Ustaw_wybrana_mape();
        }
        else
        {
            // Wczytaj zapisan¹ mapê.
            int zapisany_index_mapy_Int = PlayerPrefs.GetInt(wybrana_mapa_String);
            mapa_Dropdown.value = zapisany_index_mapy_Int;
            Debug.Log(mapa_Dropdown.value);
            Ustaw_wybrana_mape();
        }

        if (!PlayerPrefs.HasKey(poziom_trudnosci_klucz_String))
        {
            poziom_trudnosci_Dropdown.value = 0;
            Ustaw_poziom_trudnosci();
        }
        else
        {

            int savedDifficulty = PlayerPrefs.GetInt(poziom_trudnosci_klucz_String);
            poziom_trudnosci_Dropdown.value = savedDifficulty;
            Ustaw_poziom_trudnosci();
        }
        if (!PlayerPrefs.HasKey(fale_klucz_String))
        {
            fale_Dropdown.value = iloscFal_Lista[0];
            Ustaw_fale();
        }
        else
        {

            int savedFale = PlayerPrefs.GetInt(poziom_trudnosci_klucz_String);
            fale_Dropdown.value = savedFale;
            Ustaw_fale();
        }
        fale_Dropdown.onValueChanged.AddListener(delegate { Ustaw_fale(); });
        mapa_Dropdown.onValueChanged.AddListener(delegate { Ustaw_wybrana_mape(); });
        poziom_trudnosci_Dropdown.onValueChanged.AddListener(delegate { Ustaw_poziom_trudnosci(); });
        powrot_button.onClick.AddListener(() =>
        {
            flowmanager_Transform.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });
        start_button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(mapy[mapa_Dropdown.value]);
        });
    }
    private void Zaktualizuj_opcje_dropdown()
    {
        // Opcje dla map
        mapa_Dropdown.ClearOptions();
        mapa_Dropdown.AddOptions(new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("Ch³odna polana", domyslny_mapy_Sprite),
            new TMP_Dropdown.OptionData("S³oneczne lasy", domyslny_mapy_Sprite),
            new TMP_Dropdown.OptionData("Lodowate pola", domyslny_mapy_Sprite)
        });

        // Opcje dla poziomu trudnoœci.
        poziom_trudnosci_Dropdown.ClearOptions();
        poziom_trudnosci_Dropdown.AddOptions(new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("£atwy", defaultDifficultySprite),
            new TMP_Dropdown.OptionData("Œredni", defaultDifficultySprite),
            new TMP_Dropdown.OptionData("Trudny", defaultDifficultySprite)
        });
       fale_Dropdown.ClearOptions();
        fale_Dropdown.AddOptions(new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData(iloscFal_Lista[0].ToString(), domyslny_mapy_Sprite),
            new TMP_Dropdown.OptionData(iloscFal_Lista[1].ToString(), domyslny_mapy_Sprite),
            new TMP_Dropdown.OptionData(iloscFal_Lista[2].ToString(), domyslny_mapy_Sprite),
            new TMP_Dropdown.OptionData(iloscFal_Lista[3].ToString(), domyslny_mapy_Sprite)
        });
        fale_Dropdown.RefreshShownValue();
        mapa_Dropdown.RefreshShownValue();
        poziom_trudnosci_Dropdown.RefreshShownValue();
    }
    public void Ustaw_wybrana_mape()
    {
        int index_wybranej_mapy_int = mapa_Dropdown.value;
        PlayerPrefs.SetInt(wybrana_mapa_String, index_wybranej_mapy_int);
        PlayerPrefs.Save();
        string wybrana_mapa_string = mapa_Dropdown.options[index_wybranej_mapy_int].text;
        Debug.Log("Wybrano mapê: " + wybrana_mapa_string);
        wyswietlenie_Wybranej_Mapy1.sprite = Sprite_wyswietlenie_Mapy[index_wybranej_mapy_int];
    }

    public void Ustaw_poziom_trudnosci()
    {
        int index_poziomu_trudnosci_Int = poziom_trudnosci_Dropdown.value;
        PlayerPrefs.SetInt(poziom_trudnosci_klucz_String, index_poziomu_trudnosci_Int);
        PlayerPrefs.Save();
        string wybrany_poziom_trudnosci_String = poziom_trudnosci_Dropdown.options[index_poziomu_trudnosci_Int].text;
        Debug.Log("Wybrano poziom trudnoœci: " + wybrany_poziom_trudnosci_String);
    }
    public void Ustaw_fale()
    {
        int ddd = fale_Dropdown.value;
        Debug.Log("wybrane ==================================" + ddd);
        PlayerPrefs.SetInt(fale_klucz_String, iloscFal_Lista[ddd]);
        PlayerPrefs.Save();
        string wybrany_fale_String = fale_Dropdown.options[ddd].text;
        Debug.Log("Wybrano fale : " + wybrany_fale_String);
    }
}
