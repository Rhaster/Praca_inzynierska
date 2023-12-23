using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadowaniePlayerPrefs : MonoBehaviour
{

    public static int GetDifficulty()
    {
        string difficultyKey = "Difficulty";

        if (PlayerPrefs.HasKey(difficultyKey))
        {
            //Debug.Log("zaladowano poziom trudnosci:" + PlayerPrefs.GetInt(difficultyKey).ToString());
            return PlayerPrefs.GetInt(difficultyKey);
        }
        else
        {
            return 1; 
        }
    }


    public static int GetLiczbaFal()
    {
        string faleKey = "FALE";

        if (PlayerPrefs.HasKey(faleKey))
        {
            //Debug.Log("zaladowano ilosc fal:" + PlayerPrefs.GetInt(faleKey).ToString());
            return PlayerPrefs.GetInt(faleKey);
        }
        else
        {
            return 3; 
        }
    }


 
}
