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
    }

    // Update is called once per frame

    private void ZmianaKoloru()
    {
        
    }
    private void Instance_zmiana_ilosci_energi_event(object sender, System.EventArgs e)
    {
        wskaznik_transform.fillAmount = MechanikaEnergi.Instance.Get_Znormalizowana_ilosc_energi();
        energia_text.SetText(MechanikaEnergi.Instance.Get_Obecna_ilosc_energi().ToString());  
        Debug.Log("Update energia = " + MechanikaEnergi.Instance.Get_Znormalizowana_ilosc_energi() + MechanikaEnergi.Instance.Get_Obecna_ilosc_energi());
    }
}
