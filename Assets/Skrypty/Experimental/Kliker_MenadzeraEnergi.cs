using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kliker_MenadzeraEnergi : MonoBehaviour
{
    void OnMouseDown()
    {
        if(!Przycisk_rozwin.instance.UI_Menadzer_Energia_Object_transform.gameObject.activeSelf)
        {

            Przycisk_rozwin.instance.AktywujMenuEnergi();
        }
        else
        {
            Przycisk_rozwin.instance.DeaktywujMenuEnergi();
        }
    }
    
}
