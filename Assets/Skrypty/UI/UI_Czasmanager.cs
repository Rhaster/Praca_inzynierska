using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class UI_Czasmanager : MonoBehaviour
{
    #region zmienne pod instancje oraz debugging + color 
    public static UI_Czasmanager instance { get; private set; }
    [SerializeField] private Color podswietlenie_Color;
    [SerializeField] float Debug_czasu_Float;
    [SerializeField] float holder_czasu_Int;
    #endregion
    #region zmienne pod transformy przycisków 
    private Transform start_transform;
    private Transform stop_transform;
    private Transform doublestart_transform;
    #endregion
    #region Zmienne pod tekstury przycisków 
    private Image start_przycisk_Image;
    private Image stop_przycisk_Image;
    private Image podwojna_predkosc_start_przycisk_Image;
    #endregion
    #region grabbery przyciskow 
    Button stop_przycisk_Button;
    Button start_przycisk_Button;
    Button podwojna_predkosc_start_przycisk_Button;

    #endregion
    private void Awake()
    {
        
        instance = this;
    }
    void Start()
    {
        #region  Szukanie obiektów przyciskow na scenie 
        stop_transform = transform.Find("stop");
        start_transform = transform.Find("start");
        doublestart_transform = transform.Find("doublestart");
        #endregion
        #region Pobierz referencje do przycisków za pomoc¹ getcomponent
        stop_przycisk_Button = stop_transform.GetComponent<Button>();
        start_przycisk_Button = start_transform.GetComponent<Button>();
        podwojna_predkosc_start_przycisk_Button = doublestart_transform.GetComponent<Button>();
        #endregion
        #region Pobierz referencje do obrazów przycisków za pomoc¹ getcomponent
        start_przycisk_Image = start_transform.GetComponent<Image>(); 
        stop_przycisk_Image = stop_transform.GetComponent<Image>();
        podwojna_predkosc_start_przycisk_Image = doublestart_transform.GetComponent<Image>();
        #endregion
        #region Dodaj obs³ugê zdarzeñ dla przycisków
        stop_przycisk_Button.onClick.AddListener(zatrzymaj_Czas);
        start_przycisk_Button.onClick.AddListener(Wznow_Czas);
        podwojna_predkosc_start_przycisk_Button.onClick.AddListener(Wznow_podwojny_Czas);
        #endregion
        #region podswietlenie 
        start_przycisk_Image.color = podswietlenie_Color;
        start_przycisk_Image.color = new Color(start_przycisk_Image.color.r, start_przycisk_Image.color.g, start_przycisk_Image.color.b, 0.39f);
        Debug_czasu_Float = Time.timeScale;
        #endregion
    }

    void zatrzymaj_Czas()
    {
        Resetuj_kolor_Przyciskow();
        // Zatrzymaj czas
        Time.timeScale = 0;
        Debug_czasu_Float = Time.timeScale;
        stop_przycisk_Image.color = podswietlenie_Color;
        stop_przycisk_Image.color = new Color(start_przycisk_Image.color.r, start_przycisk_Image.color.g, start_przycisk_Image.color.b, 0.39f);
    }

    void Wznow_Czas()
    {
        Resetuj_kolor_Przyciskow();
        // Wznów czas
        Time.timeScale = 1;
        Debug_czasu_Float = Time.timeScale;
        start_przycisk_Image.color = podswietlenie_Color;
        start_przycisk_Image.color = new Color(start_przycisk_Image.color.r, start_przycisk_Image.color.g, start_przycisk_Image.color.b, 0.39f);
    }

    void Wznow_podwojny_Czas()
    {
        Resetuj_kolor_Przyciskow();
        // Ustaw czas na dwukrotnie szybszy
        Time.timeScale = 2;
        Debug_czasu_Float = Time.timeScale;
        podwojna_predkosc_start_przycisk_Image.color = podswietlenie_Color;
        podwojna_predkosc_start_przycisk_Image.color = new Color(start_przycisk_Image.color.r, start_przycisk_Image.color.g, start_przycisk_Image.color.b, 0.39f);
    }
    private void Resetuj_kolor_Przyciskow()
    {
        start_przycisk_Image.color= Color.white;
        stop_przycisk_Image.color = Color.white;
        podwojna_predkosc_start_przycisk_Image.color = Color.white;
    }
    public float getCurrentTimeScale()
    {
        return Debug_czasu_Float;
    }
    public void StopCzas_Przycisk()
    {
        stop_przycisk_Button.onClick.Invoke();
    }
    public void StartCzas_Przycisk(float x)
    {
        if (x == 1)
        { 
            start_przycisk_Button.onClick.Invoke();
        }
        else if(x == 2) 
        {
            Wznow_podwojny_Czas();
        }
        else
        {
            StopCzas_Przycisk();
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) {
            Debug.Log("Wcisnieto P");

            if (Time.timeScale == 1)
            {
                holder_czasu_Int = 1;
                stop_przycisk_Button.onClick.Invoke();
            }
            else if(Time.timeScale ==2)
            {
                holder_czasu_Int=2;
                stop_przycisk_Button.onClick.Invoke();
            }
            else if(Time.timeScale ==0) {
                if (holder_czasu_Int == 1)
                {
                    start_przycisk_Button.onClick.Invoke();
                }
                else
                {
                    Wznow_podwojny_Czas();
                }
            }
        }
    }

}
