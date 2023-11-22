using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class GeneratorSurowcow : MonoBehaviour
{
    [SerializeField] public Surowce_SO surowiecGenerowany;
    [SerializeField] public int IloscEnergi;
    [SerializeField] private string nazwa_kopalni;
    private float timer;
    private float timerMax;
    public static GeneratorSurowcow Instance { get; private set; }

    private void Awake()
    {
        Instance= this;
        timerMax = IloscEnergi;
    }



    private void Update()
    {
        if (timerMax > 0)
        {
            timer -= Time.deltaTime;
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
    public float getIloscEnergi()
    {
        return timerMax;
    }
    public void zmienIloscEnergi(int ilosc)
    {
        timerMax= ilosc;
        IloscEnergi= ilosc;
    }
}
