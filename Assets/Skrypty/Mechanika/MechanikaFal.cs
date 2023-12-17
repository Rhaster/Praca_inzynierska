using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanikaFal : MonoBehaviour
{
    public Wyznacznik_fali wyznacznik_fali;
    public static MechanikaFal Instance { get; private set; }
    [SerializeField] public event EventHandler zmianaFali_event;
    [SerializeField] public event EventHandler Fala_Bossa_event;
    [SerializeField] private Lista_Wrogowie_SO wrog_lista;
    [SerializeField] private List<string> AktualnySk³adFali;
    private enum State
    {
        Oczekiwanie,
        TworzenieFali,
    }

    [SerializeField] private List<Transform> spawnPositionTransformList;
    [SerializeField] private Transform[] holderPozycji;
    [SerializeField] private State state;
    [SerializeField] private int waveNumber;
    [SerializeField] private float nextWaveSpawnTimer;
    [SerializeField] private float nextEnemySpawnTimer;
    [SerializeField] private int remainingEnemySpawnAmount;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private float PredkoscWroga;
    [SerializeField] private int ZczytanyPoziomTrudnosci_Int;
    [SerializeField] private int ZczytanyiloscFal_Int;
    private void Awake()
    {
        ZczytanyPoziomTrudnosci_Int = LadowaniePlayerPrefs.GetDifficulty();
        ZczytanyiloscFal_Int = LadowaniePlayerPrefs.GetNumberOfWaves();
        Instance = this;
        holderPozycji = new Transform[spawnPositionTransformList.Count];
        wrog_lista = Resources.Load<Lista_Wrogowie_SO>("Wrogowie_Lista");
        int i = 0;
        foreach(Transform t in spawnPositionTransformList)
        {
            holderPozycji[i] = t;
            i++;
        }
        //Debug.Log("lista wrogów");
        foreach(Wrogowie_SO x in wrog_lista.wrogowie_so_Lista)
        {
            //Debug.Log(x.wrog_Nazwa);
        }
    }
    private void WyznaczFale()
    {
        // oblicz jesli gracz ma wiecej wiez obszarowych to wyslij wiecej silniejszych jednostek 

    }
    
    private void Start()
    {
        waveNumber = 1;
        state = State.Oczekiwanie;
        spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;
        switch (ZczytanyPoziomTrudnosci_Int) // czas przed pierwsza fala 
        {
            case 1:
                nextWaveSpawnTimer = 5f;
                break;
            case 2:
                nextWaveSpawnTimer = 40f;
                break;
            case 3:
                nextWaveSpawnTimer = 30f;
                break;
            default:
                nextWaveSpawnTimer = 30f;
                break;
        }

        zmianaFali_event?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
        switch (state)
        {
            case State.Oczekiwanie:
                if(waveNumber == ZczytanyiloscFal_Int+1)
                {
                    Debug.Log("osiagnieto fale bossa");
                    Fala_Bossa_event?.Invoke(this,EventArgs.Empty);
                    gameObject.SetActive(false);
                }
                nextWaveSpawnTimer -= Time.deltaTime;
                if (nextWaveSpawnTimer < 0f)
                {
                    SpawnWave();
                }
                break;
            case State.TworzenieFali:
                if (remainingEnemySpawnAmount > 0)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;
                    if (nextEnemySpawnTimer < 0f)
                    {
                        nextEnemySpawnTimer = UnityEngine.Random.Range(1f, 3f);
                        wrog.Create(spawnPosition * UnityEngine.Random.Range(0f, 5f), "pf_wrog_" + AktualnySk³adFali[0],
                            holderPozycji, PredkoscWroga); ; //UtilsClass.GetRandomDir() 
                        AktualnySk³adFali.RemoveAt(0);
                        //old wrog.Create(spawnPosition  * UnityEngine.Random.Range(0f, 5f),"pf_wrog_"+wrog_lista.wrogowie_so_Lista[0].wrog_Nazwa ,
                        //holderPozycji,PredkoscWroga); //UtilsClass.GetRandomDir() 
                        remainingEnemySpawnAmount--;
                        if (remainingEnemySpawnAmount <= 0)
                        {
                            state = State.Oczekiwanie;
                            
                            spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;
                            nextWaveSpawnTimer = Mathf.Clamp(30f - 4f * waveNumber, 10f, 30f);
                            zmianaFali_event?.Invoke(this, EventArgs.Empty);
                        }
                    }
                }
                break;
        }
    }
    private void SpawnWave()
    {
        remainingEnemySpawnAmount = 3 + 2 * waveNumber;
        AktualnySk³adFali = wyznacznik_fali.ustalfale(remainingEnemySpawnAmount,1); // holder do zmiany przy imp budynków 
        state = State.TworzenieFali;
        waveNumber++;
        //zmianaFali_event?.Invoke(this, EventArgs.Empty);
        //OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetNumerFali()
    {
        return waveNumber;
    }

    public float GetCzasSpawnuFali()
    {
        return nextWaveSpawnTimer;
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }

}


