using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opcje_opis : MonoBehaviour
{
    private Transform t³o_transform;
    private Transform przycisk_transform;
    private Transform wyjscie_transform;
    private void Awake()
    {
        t³o_transform = transform.Find("T³o");
        wyjscie_transform = transform.Find("wyjscie");

        gameObject.SetActive(false);
    }
    private void Start()
    {


        wyjscie_transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            Podswietlenie_przyciskow1 pod_przyc = wyjscie_transform.GetComponent<Podswietlenie_przyciskow1>();
            pod_przyc.Reset();
            gameObject.SetActive(false);
            Kontroler_UI.Instance.gameObject.SetActive(true);
        });
    }


}
