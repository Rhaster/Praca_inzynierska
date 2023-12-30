using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Kontroler_UI : MonoBehaviour
{
    public event EventHandler Rozwin_opcje;

    private Transform Nowagra_Transform;
    private Transform Opcje_Transform;
    private Transform jak_grac_Transform;
    private Transform Wyjscie_Transform;
    public Transform Wybor_Mapy_Transform;
    public Transform jak_grac_szcze_Transform;
    public static Kontroler_UI Instance { get; private set; }
    private void Awake()
    {
        jak_grac_Transform = transform.Find("jak_grac_btn");
        Nowagra_Transform = transform.Find("Nowa_gra_btn");
        Opcje_Transform = transform.Find("Opcje_btn");
        Wyjscie_Transform = transform.Find("Wyjscie_btn");
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Nowagra_Transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            Wybor_Mapy_Transform.gameObject.SetActive(true);
            gameObject.SetActive(false);

        });
        jak_grac_Transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            jak_grac_szcze_Transform.gameObject.SetActive(true);

        });
        Opcje_Transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            Opcje_Transform.GetComponent<Podswietlenie_przyciskow>().Reset();
            gameObject.SetActive(false);
            Rozwin_opcje?.Invoke(this, EventArgs.Empty);
        });
        Wyjscie_Transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
    
}
