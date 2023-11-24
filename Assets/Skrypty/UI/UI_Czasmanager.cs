using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class UI_Czasmanager : MonoBehaviour
{
    public static UI_Czasmanager instance { get; private set; }
    [SerializeField] private Color podswietlenie_Color;
    [SerializeField] float Debug_czasu;
    private Transform start_transform;
    private Transform stop_transform;
    private Transform doublestart_transform;
    private Image start_Image;
    private Image stop_Image;
    private Image doublestart_Image;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        stop_transform = transform.Find("stop");
        start_transform = transform.Find("start");
        doublestart_transform = transform.Find("doublestart");
        // Pobierz referencje do przycisków za pomoc¹ Transform.Find
        Button stopButton = stop_transform.GetComponent<Button>();
        Button startButton = start_transform.GetComponent<Button>();
        Button doubleStartButton = doublestart_transform.GetComponent<Button>();

        start_Image = start_transform.GetComponent<Image>(); 
        stop_Image = stop_transform.GetComponent<Image>();
        doublestart_Image = doublestart_transform.GetComponent<Image>();
        // Dodaj obs³ugê zdarzeñ dla przycisków
        stopButton.onClick.AddListener(StopTime);
        startButton.onClick.AddListener(StartTime);
        doubleStartButton.onClick.AddListener(DoubleStartTime);
        start_Image.color = podswietlenie_Color.WithAlpha(100);
        Debug_czasu = 1;

    }

    void StopTime()
    {
        Reset();
        // Zatrzymaj czas
        Time.timeScale = 0;
        Debug_czasu = Time.timeScale;
        stop_Image.color = podswietlenie_Color.WithAlpha(100);
    }

    void StartTime()
    {
        Reset();
        // Wznów czas
        Time.timeScale = 1;
        Debug_czasu = Time.timeScale;
        start_Image.color = podswietlenie_Color.WithAlpha(100); ;
    }

    void DoubleStartTime()
    {
        Reset();
        // Ustaw czas na dwukrotnie szybszy
        Time.timeScale = 2;
        Debug_czasu = Time.timeScale;
        doublestart_Image.color= podswietlenie_Color.WithAlpha(100); ;
    }
    private void Reset()
    {
        start_Image.color= Color.white;
        stop_Image.color = Color.white;
        doublestart_Image.color = Color.white;
    }
    public float getCurrentTimeScale()
    {
        return Debug_czasu;
    }

}
