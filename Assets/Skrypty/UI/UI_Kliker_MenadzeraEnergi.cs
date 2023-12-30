using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Kliker_MenadzeraEnergi : MonoBehaviour
{
    void OnMouseDown()
    {
        if(!Przycisk_rozwin.instance.UI_Menadzer_Energia_Object_transform.gameObject.activeSelf)
        {
            Debug.Log("kliknieto na menadzer energi");
            Przycisk_rozwin.instance.AktywujMenuEnergi();
        }
        else
        {
            Przycisk_rozwin.instance.DeaktywujMenuEnergi();
        }
    }
    
}
