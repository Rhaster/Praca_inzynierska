using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class GeneratorSurowcow : MonoBehaviour
{
    [SerializeField] public Surowce_SO surowiecGenerowany;
    [SerializeField] public int IloscEnergi;
    [SerializeField] public string nazwa_kopalni;
    [SerializeField] public int IloscEnergiMax;
    private float timer;
    private float timerMax;
    public static GeneratorSurowcow Instance { get; private set; }
    public event EventHandler ZmianaTimeraEvent;

    private void Awake()
    {
        
        Instance= this;

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
            ZmianaTimeraEvent?.Invoke(this, EventArgs.Empty);
            if (timer <= 0f)
            {
                timer += timerMax;
                MechanikaEkonomi.Instance.DodajSurowiec(surowiecGenerowany, 1);
            }
        }
    }
    

    public float GetTimerNormalized()
    {
        return timer / timerMax;
    }

    public float GetAmountGeneratedPerSecond()
    {
        return 1 / timerMax;
    }
    public float getTimerMax()
    {
        return IloscEnergi;
    }
    public float getIloscEnergi()
    {
        return IloscEnergi;
    }
    public void zmienIloscEnergi(int ilosc)
    {
        IloscEnergi += ilosc;
        if(IloscEnergi == 0)
        {
            timerMax = IloscEnergi;
        }
        else
        {
            timerMax = IloscEnergiMax -IloscEnergi;
        }
    }
}
