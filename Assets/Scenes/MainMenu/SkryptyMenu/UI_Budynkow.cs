using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Budynkow : MonoBehaviour
{
    private Transform templateTransform;
    public Button przycisk_dodaj_energia_button;
    public Button przycisk_odejmij_energia_button;
    public Surowce_SO surowiec;
    public TextMeshProUGUI nazwa_kopalni_Text;
    // Start is called before the first frame update
    void Start()
    {
        templateTransform = transform.Find("Sekcja_zasilania");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
