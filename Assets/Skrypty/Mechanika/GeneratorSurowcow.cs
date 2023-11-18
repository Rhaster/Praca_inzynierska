using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class GeneratorSurowcow : MonoBehaviour
{
    [SerializeField] public Surowce_SO surowiecGenerowany;
    [SerializeField] public int IloscEnergi;
    private float timer;
    private float timerMax;


    private void Awake()
    {
        timerMax = IloscEnergi;
    }

    private void Start()
    {
        
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
}
