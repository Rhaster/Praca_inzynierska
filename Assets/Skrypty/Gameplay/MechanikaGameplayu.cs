using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MechanikaGameplayu : MonoBehaviour
{
    public static MechanikaGameplayu instance;
    [SerializeField] private Transform mechanikaSpawnuFali_Transform;
    [SerializeField] private Transform UI_ekran_Transform;
    [SerializeField] private int poziomtrudnosci_int;
    [SerializeField] private int iloscFal_int;
    [SerializeField] private Transform boss_Transform;
    private string wynik;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        boss_Transform.gameObject.SetActive(false);
        poziomtrudnosci_int = LadowaniePlayerPrefs.GetDifficulty();
        iloscFal_int = LadowaniePlayerPrefs.GetLiczbaFal();
        Cel_handler.instance.porazka_event += Instance_porazka_event;
        MechanikaFal.Instance.Fala_Bossa_event += Instance_Fala_Bossa_event;

    }

    private void Instance_wygrana_event(object sender, EventArgs e)
    {
        Time.timeScale = 0;
        wynik = "Zwyciêstwo";

        UI_ekran_Transform.gameObject.SetActive(true);
    }

    private void Inicjalizacja_Poziomu_trudnosci()
    {
        switch (poziomtrudnosci_int) // czas przed pierwsza fala 
        {
            case 1:
                MechanikaBossa.instance.wyznaczHP(800);
                break;
            case 2:
                MechanikaBossa.instance.wyznaczHP(1200);
                break;
            case 3:
                MechanikaBossa.instance.wyznaczHP(1800);
                break;
            default:
                MechanikaBossa.instance.wyznaczHP(400);
                break;
        }
    }
    public string getWynik()
    {
        return wynik;
    }
    private void Instance_Fala_Bossa_event(object sender, EventArgs e)
    {
        boss_Transform.gameObject.SetActive(true);
        MechanikaBossa.instance.wygrana_event += Instance_wygrana_event;
        Inicjalizacja_Poziomu_trudnosci();
    }

    private void Instance_porazka_event(object sender, EventArgs e)
    {
        wynik = "Pora¿ka";
        Time.timeScale = 0;
        UI_ekran_Transform.gameObject.SetActive(true);
    }

    // Update is called once per frame

}
