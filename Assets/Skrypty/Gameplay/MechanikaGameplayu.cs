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
        CameraControl.Instance.Wylacz_kamere();
        Time.timeScale = 0;
        wynik = "Zwyciêstwo";
        Dezaktywacja();
        Dezaktywacja1();
        UI_ekran_Transform.gameObject.SetActive(true);
    }

    private void Inicjalizacja_Poziomu_trudnosci()
    {
        switch (poziomtrudnosci_int) // czas przed pierwsza fala 
        {
            case 1:
                MechanikaBossa.instance.wyznaczHP(25000);
                break;
            case 2:
                MechanikaBossa.instance.wyznaczHP(50000);
                break;
            case 3:
                MechanikaBossa.instance.wyznaczHP(100000);
                break;
            default:
                MechanikaBossa.instance.wyznaczHP(899);
                break;
        }
    }
    public string getWynik()
    {
        return wynik;
    }
    private void Instance_Fala_Bossa_event(object sender, EventArgs e)
    {
        UIController.instance.aktywacja_bossa_Event += Instance_aktywacja_bossa_Event;
        MechanikaBossa.instance.wygrana_event += Instance_wygrana_event;
        MusicManager_game.Instance.Wlacz_musyzke_bossa();
        Inicjalizacja_Poziomu_trudnosci();
    }

    private void Instance_aktywacja_bossa_Event(object sender, EventArgs e)
    {
        boss_Transform.gameObject.SetActive(true);
    }

    public void Dezaktywacja()
    {
        GameObject[] obiektyZTagiem = GameObject.FindGameObjectsWithTag("UstawienieWiezy");

        foreach (GameObject obiekt in obiektyZTagiem)
        {
            obiekt.SetActive(false);
        }
    }
    private void Instance_porazka_event(object sender, EventArgs e)
    {
        CameraControl.Instance.Wylacz_kamere();
        Dezaktywacja();
        Dezaktywacja1();
        wynik = "Pora¿ka";
        Time.timeScale = 0;
        
        UI_ekran_Transform.gameObject.SetActive(true);
    }
    public void Dezaktywacja1()
    {
        GameObject[] obiektyZTagiem = GameObject.FindGameObjectsWithTag("UI_wroga");

        foreach (GameObject obiekt in obiektyZTagiem)
        {
            obiekt.SetActive(false);
        }
    }
    // Update is called once per frame

}
