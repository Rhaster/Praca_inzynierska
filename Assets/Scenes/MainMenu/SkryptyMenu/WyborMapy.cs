using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WyborMapy : MonoBehaviour
{
    public TMP_Dropdown mapDropdown;
    public TMP_Dropdown difficultyDropdown;
    public TMP_Dropdown faleDropdown;
    public Image wyswietlenieWybranejMapy1;
    public List<Sprite> SpritewyswietlenieMapy;
    public Transform flowmanager; 
    /// Klucze player prefs 
    private string wybrana_mapa_String = "WybranaMapa";
    private string difficultyKey = "Difficulty";
    private string faleKey = "FALE";

    private Button powrot_button;
    private Button start_button;
    public Sprite defaultMapSprite;
    public Sprite defaultDifficultySprite;
    public List<string> mapy;
    public List<int> iloscFal_Lista;
    private void Awake()
    {
        powrot_button = transform.Find("wyjscie").GetComponent<Button>();
        start_button = transform.Find("Start").GetComponent<Button>();
        gameObject.SetActive(false);
    }
    private void Start()
    {
        // Ustaw domy�lne warto�ci, je�li jeszcze nie zapisano wyboru mapy i poziomu trudno�ci.
        if (!PlayerPrefs.HasKey(wybrana_mapa_String))
        {
            mapDropdown.value = 0;
            SetSelectedMap();
        }
        else
        {
            // Wczytaj zapisan� map�.
            int savedMapIndex = PlayerPrefs.GetInt(wybrana_mapa_String);
            mapDropdown.value = savedMapIndex;
            SetSelectedMap();
        }

        if (!PlayerPrefs.HasKey(difficultyKey))
        {
            difficultyDropdown.value = 0;
            SetDifficulty();
        }
        else
        {

            int savedDifficulty = PlayerPrefs.GetInt(difficultyKey);
            difficultyDropdown.value = savedDifficulty;
            SetDifficulty();
        }
        if (!PlayerPrefs.HasKey(faleKey))
        {
            faleDropdown.value = iloscFal_Lista[0];
            SetDifficulty();
        }
        else
        {

            int savedFale = PlayerPrefs.GetInt(difficultyKey);
            faleDropdown.value = savedFale;
            SetFale();
        }
        UpdateDropdownOptions();
        faleDropdown.onValueChanged.AddListener(delegate { SetFale(); });
        mapDropdown.onValueChanged.AddListener(delegate { SetSelectedMap(); });
        difficultyDropdown.onValueChanged.AddListener(delegate { SetDifficulty(); });
        powrot_button.onClick.AddListener(() =>
        {
            flowmanager.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });
        start_button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(mapy[mapDropdown.value]);
        });
    }
    private void UpdateDropdownOptions()
    {
        // Opcje dla map
        mapDropdown.ClearOptions();
        mapDropdown.AddOptions(new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("Mapa1", defaultMapSprite),
            new TMP_Dropdown.OptionData("Mapa2", defaultMapSprite),
            new TMP_Dropdown.OptionData("Mapa3", defaultMapSprite)
        });

        // Opcje dla poziomu trudno�ci.
        difficultyDropdown.ClearOptions();
        difficultyDropdown.AddOptions(new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("�atwy", defaultDifficultySprite),
            new TMP_Dropdown.OptionData("�redni", defaultDifficultySprite),
            new TMP_Dropdown.OptionData("Trudny", defaultDifficultySprite)
        });
       faleDropdown.ClearOptions();
        faleDropdown.AddOptions(new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData(iloscFal_Lista[0].ToString(), defaultMapSprite),
            new TMP_Dropdown.OptionData(iloscFal_Lista[1].ToString(), defaultMapSprite),
            new TMP_Dropdown.OptionData(iloscFal_Lista[2].ToString(), defaultMapSprite)
        });
        faleDropdown.RefreshShownValue();
        mapDropdown.RefreshShownValue();
        difficultyDropdown.RefreshShownValue();
    }
    public void SetSelectedMap()
    {
        int selectedMapIndex = mapDropdown.value;
        PlayerPrefs.SetInt(wybrana_mapa_String, selectedMapIndex);
        PlayerPrefs.Save();
        string selectedMap = mapDropdown.options[selectedMapIndex].text;
        Debug.Log("Wybrano map�: " + selectedMap);
        wyswietlenieWybranejMapy1.sprite = SpritewyswietlenieMapy[selectedMapIndex];
    }

    public void SetDifficulty()
    {
        int difficultyIndex = difficultyDropdown.value;
        PlayerPrefs.SetInt(difficultyKey, difficultyIndex);
        PlayerPrefs.Save();
        string selectedDifficulty = difficultyDropdown.options[difficultyIndex].text;
        Debug.Log("Wybrano poziom trudno�ci: " + selectedDifficulty);
    }
    public void SetFale()
    {
        int difficultyIndex = faleDropdown.value;
        PlayerPrefs.SetInt(faleKey, difficultyIndex);
        PlayerPrefs.Save();
        string selectedDifficulty = faleDropdown.options[difficultyIndex].text;
        Debug.Log("Wybrano fale : " + selectedDifficulty);
    }
}
