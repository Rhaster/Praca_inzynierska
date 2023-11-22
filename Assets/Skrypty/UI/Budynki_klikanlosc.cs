using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Budynki_klikanlosc : MonoBehaviour
{

    public Transform ui_budowy_transform;
    // Przypisz przycisk UI do tej zmiennej w inspektorze Unity
    public Button przyciskUI;
    void Start()
    {
        // Dodaj metodê do obs³ugi zdarzenia naciœniêcia przycisku
        //GetComponent<Button>().onClick.AddListener(() => { InstancjonujObiekt(); });
    }
    void InstancjonujObiekt()
    {
        // Instancjonuj obiekt przy u¿yciu pozycji i rotacji aktualnego Transforma
        ui_budowy_transform.gameObject.SetActive(true);
    }
    void OnMouseDown()
    {
        InstancjonujObiekt();
    }
}
