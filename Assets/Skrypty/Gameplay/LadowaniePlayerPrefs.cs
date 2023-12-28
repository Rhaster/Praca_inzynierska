using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
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
    public static List<Wieze_SO> OdczytajListeWiez()
    {
        string faleKey = "MAPA";
        // Odczytaj wartoœæ z PlayerPrefs na podstawie podanego klucza
        if (PlayerPrefs.HasKey(faleKey))
        {
            //Debug.Log("zaladowano ilosc fal:" + PlayerPrefs.GetInt(faleKey).ToString());
            if (PlayerPrefs.GetInt(faleKey) == 0)
            {
                List<Wieze_SO> xd = Resources.Load<Lista_Wieze_SO>("Wieze_Lista_MAPA3").Wieze_Lista;
                return xd;
            }

            else if (PlayerPrefs.GetInt(faleKey) == 1)
            {
                List<Wieze_SO> xd = Resources.Load<Lista_Wieze_SO>("Wieze_Lista_MAPA1").Wieze_Lista;
                return xd;
            }
            else
            {
                List<Wieze_SO> xd = Resources.Load<Lista_Wieze_SO>("Wieze_Lista_MAPA2").Wieze_Lista;
                return xd;
            }
        }
        else
        {
            List<Wieze_SO> xd = Resources.Load<Lista_Wieze_SO>("Wieze_Lista_MAPA1").Wieze_Lista;
            return xd;
        }
    }


}
