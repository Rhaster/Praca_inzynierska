using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanikaFal : MonoBehaviour
{
    public static MechanikaFal Instance { get; private set; }


    [SerializeField] private Lista_Wrogowie_SO wrog_lista;
    [SerializeField] public event EventHandler OnWaveNumberChanged;

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
    private void Awake()
    {
        holderPozycji = new Transform[spawnPositionTransformList.Count];
        wrog_lista = Resources.Load<Lista_Wrogowie_SO>("Wrogowie_Lista");
        Instance = this;
        int i = 0;
        foreach(Transform t in spawnPositionTransformList)
        {
            holderPozycji[i] = t;
            i++;
        }
        foreach(Wrogowie_SO x in wrog_lista.wrogowie_so_Lista)
        {
            Debug.Log(x.wrog_Nazwa);
        }
    }
    private void WyznaczFale()
    {
        // oblicz jesli gracz ma wiecej wiez obszarowych to wyslij wiecej silniejszych jednostek 

    }
    private void Start()
    {
        state = State.Oczekiwanie;
        spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;
        nextWaveSpawnTimer = 10f;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Oczekiwanie:
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
                        wrog.Create(spawnPosition  * UnityEngine.Random.Range(0f, 5f),"pf_wrog_"+wrog_lista.wrogowie_so_Lista[0].wrog_Nazwa , holderPozycji,PredkoscWroga); //UtilsClass.GetRandomDir() 
                        remainingEnemySpawnAmount--;
                        if (remainingEnemySpawnAmount <= 0)
                        {
                            state = State.Oczekiwanie;
                            spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;
                            nextWaveSpawnTimer = Mathf.Clamp(30f - 4f * waveNumber, 10f, 30f);
                        }
                    }
                }
                break;
        }
    }
    private void SpawnWave()
    {
        remainingEnemySpawnAmount = 3 + 2 * waveNumber;
        state = State.TworzenieFali;
        waveNumber++;
        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public float GetNextWaveSpawnTimer()
    {
        return nextWaveSpawnTimer;
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }

}


