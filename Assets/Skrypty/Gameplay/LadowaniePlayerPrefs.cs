using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadowaniePlayerPrefs : MonoBehaviour
{
   
    private void Start()
    {
        DisplayAllPlayerPrefs();
    }
    public static int GetDifficulty()
    {
        string difficultyKey = "Difficulty";

        if (PlayerPrefs.HasKey(difficultyKey))
        {
            Debug.Log("zaladowano poziom trudnosci:" + PlayerPrefs.GetInt(difficultyKey).ToString());
            return PlayerPrefs.GetInt(difficultyKey);
        }
        else
        {
            return 1; 
        }
    }

    public static void SetDifficulty(int difficulty)
    {
        string difficultyKey = "Difficulty";
        PlayerPrefs.SetInt(difficultyKey, difficulty);
        PlayerPrefs.Save();
    }

    public static int GetNumberOfWaves()
    {
        string faleKey = "FALE";

        if (PlayerPrefs.HasKey(faleKey))
        {
            Debug.Log("zaladowano ilosc fal:" + PlayerPrefs.GetInt(faleKey).ToString());
            return PlayerPrefs.GetInt(faleKey);
        }
        else
        {
            return 3; 
        }
    }

    public static void SetNumberOfWaves(int numberOfWaves)
    {
        string faleKey = "FALE";
        PlayerPrefs.SetInt(faleKey, numberOfWaves);
        PlayerPrefs.Save();
    }
    public void DisplayAllPlayerPrefs()
    {
        Debug.Log("PlayerPrefs data:");

        // Iteracja po wszystkich kluczach
        foreach (var key in PlayerPrefs.GetString("FALE"))
        {
            // Wyœwietlanie klucza i odpowiadaj¹cej mu wartoœci
            Debug.Log(key + ": " + PlayerPrefs.GetString(key.ToString()));
        }
    }
}
