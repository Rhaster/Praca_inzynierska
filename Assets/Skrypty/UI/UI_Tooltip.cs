using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_Tooltip : MonoBehaviour
{

    private Dictionary<Wieze_SO, Transform> wieze_Slownik;
    // Start is called before the first frame update

        private void Awake()
        {
            Transform btnTemplate_Transform = transform.Find("Tooltip_template");
            btnTemplate_Transform.gameObject.SetActive(false);


        List<Wieze_SO> wieze_ = LadowaniePlayerPrefs.OdczytajListeWiez();
        wieze_Slownik = new Dictionary<Wieze_SO, Transform>();

            foreach (Wieze_SO typ_budoli_Wieze_SO in wieze_)
            {
            if (typ_budoli_Wieze_SO != null)
            {
                Transform btnTransform = Instantiate(btnTemplate_Transform, transform);
                btnTransform.GetComponent<UI_tooltip_zawartosc>().wieza_Wieza_SO = typ_budoli_Wieze_SO;
                btnTransform.gameObject.SetActive(false);
                wieze_Slownik[typ_budoli_Wieze_SO] = btnTransform;
            }
            }
        
    }

    // Update is called once per frame
    private void Start()
    {
        MechanikaBudowania.Instance.Zmiana_aktywnego_typu_wiezy += Instance_Zmiana_aktywnego_typu_wiezy;
    }

    private void Instance_Zmiana_aktywnego_typu_wiezy(object sender, MechanikaBudowania.Holder_Typu_Budowli e)
    {
        foreach (Wieze_SO wieza in wieze_Slownik.Keys)
        {
            wieze_Slownik[wieza].gameObject.SetActive(false);
        }
        if (e.aktywna_wieza_so!=null)
        {
            wieze_Slownik[e.aktywna_wieza_so].gameObject.SetActive(true);
        }


    }
}
