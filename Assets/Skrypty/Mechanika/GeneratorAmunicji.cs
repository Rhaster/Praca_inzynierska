using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorAmunicji : MonoBehaviour
{
    [SerializeField] public Amunicja_SO amunicjaGenerowany;
    [SerializeField] public int IloscEnergi;
    [SerializeField] public string nazwa_kopalni;
    [SerializeField] public int IloscEnergiMax;
    private float elapsedTime = 0f;
    private float desiredTime = 0.1f; // Sekundy
    [SerializeField] private float timer;
    private float timerMax;
    public static GeneratorAmunicji Instance { get; private set; }
    public event EventHandler ZmianaTimeraEvent;
    //public event EventHandler<ZmianaElektrykiHolder> ZmianaElektrykiEvent;
  
    private bool flaga;
    private void Awake()
    {

        Instance = this;
        flaga = false;
        timerMax = IloscEnergi;
    }


    private void Start()
    {
        IloscEnergiMax = MechanikaEnergi.Instance.Get_Maxymalna_ilosc_energi();
    }
    private void Update()
    {
        if (timerMax > 0)
        {
            timer -= Time.deltaTime;
            elapsedTime += Time.deltaTime;
            if (CzyMineloJednaSekunda())
            {
                ZmianaTimeraEvent?.Invoke(this, EventArgs.Empty);
                elapsedTime = 0;
            }
            if (timer <= 0f && flaga)
            {
                timer += timerMax;
                // dodac gen amunicji
                if (amunicjaGenerowany != null)
                {
                    MechanikaAmunicji.Instance.DodajAmunicji(amunicjaGenerowany, 1);
                }
            }
        }
    }

    bool CzyMineloJednaSekunda()
    {
        return elapsedTime >= desiredTime;
    }
    public float GetTimerNormalized()
    {
        return timer / timerMax;
    }
    public float GetTimer()
    {
        return timer;
    }
    public float GetAmountGeneratedPerSecond()
    {
        return 1 / timerMax;
    }
    public float getTimerMax()
    {
        return timerMax;
    }
    public float getIloscEnergi()
    {
        return IloscEnergi;
    }
    public void zmienIloscEnergi(int ilosc)
    {
        IloscEnergi += ilosc;
        if (IloscEnergi == 0)
        {
            flaga = false;
            timerMax = 0;
            ZmianaTimeraEvent?.Invoke(this, EventArgs.Empty);
            UI_MenadzerEnergi.Instance.Aktualizuj_bar_UI_Menadzera_energi(null, this);
            Debug.Log("zmiana");
        }
        else
        {
            timer = IloscEnergiMax - IloscEnergi;
            flaga = true;
            timerMax = IloscEnergiMax - IloscEnergi;
            if (timerMax == 0)
            {
                timerMax = 0.5f;
            }
            ZmianaTimeraEvent?.Invoke(this, EventArgs.Empty);
            UI_MenadzerEnergi.Instance.Aktualizuj_bar_UI_Menadzera_energi(null,this);
            Debug.Log("zmiana");
        }
    }
    public bool OgraniczenieDlaGeneratorBool(int ilosc)
    {
        if (IloscEnergiMax - (IloscEnergi + ilosc) > 0)
        {
            return true;
        }
        return false;
    }

}
