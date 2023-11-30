using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Wieze_Zasieg_klikalnosc : MonoBehaviour
{
    public Transform ui_wiezy_zasieg_transform;


    void Start()
    {
        ui_wiezy_zasieg_transform = transform.Find("Zasieg");
        ui_wiezy_zasieg_transform.gameObject.SetActive(false);
    }

    private void Awake()
    {

    }

    void OnMouseDown()
    {
        if (!ui_wiezy_zasieg_transform.gameObject.activeSelf)
        {
            ui_wiezy_zasieg_transform.gameObject.SetActive(true);

        }
        else
        {
            ui_wiezy_zasieg_transform.gameObject.SetActive(false);
        }
    }
    private void OnMouseExit()
    {
        if (ui_wiezy_zasieg_transform.gameObject.activeSelf)
        {
            ui_wiezy_zasieg_transform.gameObject.SetActive(false);

        }
    }
}
