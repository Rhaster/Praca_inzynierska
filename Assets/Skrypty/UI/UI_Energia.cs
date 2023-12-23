using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Energia : MonoBehaviour
{
    private Transform Komponent_Transform;
    private Transform Maska_transform;
    private Image wskaznik_transform;
    private TextMeshProUGUI energia_text;
    public Color lowVolumeColor = Color.red;
    public Color mediumVolumeColor = Color.green;
    public Color highVolumeColor = Color.yellow;
    private int maxiloscenergi;
    // Start is called before the first frame update dziffko 
    private void Awake()
    {
       
        Komponent_Transform = transform.Find("WskaznikEnergi");
        Maska_transform = Komponent_Transform.Find("mask");
        wskaznik_transform = Maska_transform.Find("image").GetComponent<Image>();
        energia_text = Komponent_Transform.Find("energia_text").GetComponent<TextMeshProUGUI>();
       
    }
    void Start()
    { 
        MechanikaEnergi.Instance.zmiana_ilosci_energi_event += Instance_zmiana_ilosci_energi_event;
        energia_text.SetText(MechanikaEnergi.Instance.Get_Obecna_ilosc_energi().ToString());
        maxiloscenergi = MechanikaEnergi.Instance.Get_Maxymalna_ilosc_energi();
    }

    // Update is called once per frame

    private void ZmianaKoloru(float wartosc)
    {
        float normalizedVolume = Mathf.InverseLerp(0, maxiloscenergi, wartosc);

        Color kolor = Color.Lerp(lowVolumeColor, highVolumeColor, normalizedVolume);

        wskaznik_transform.color = kolor;
    }

    private void Instance_zmiana_ilosci_energi_event(object sender, System.EventArgs e)
    {
        float holder = MechanikaEnergi.Instance.Get_Obecna_ilosc_energi();
        wskaznik_transform.fillAmount = MechanikaEnergi.Instance.Get_Znormalizowana_ilosc_energi();
        energia_text.SetText(holder.ToString());
        ZmianaKoloru(holder);
        //Debug.Log("Update energia = " + MechanikaEnergi.Instance.Get_Znormalizowana_ilosc_energi() + MechanikaEnergi.Instance.Get_Obecna_ilosc_energi());
    }
}
