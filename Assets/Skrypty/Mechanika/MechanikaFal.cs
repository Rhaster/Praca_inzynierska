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
    private enum status_wavemanager_enum
    {
        Oczekiwanie,
        TworzenieFali,
    }

    [SerializeField] private List<Transform> pozycja_spawnu_List;
    [SerializeField] private Transform[] holder_Pozycji_Lista;
    [SerializeField] private status_wavemanager_enum status_Enum;
    [SerializeField] private int Numer_Fali_INT;
    [SerializeField] private float czas_spawnu_nast_Fali_Float;
    [SerializeField] private float odstep_miedzy_spawnem_wroga_Float;
    [SerializeField] private int pozostala_ilosc_wrogow_do_utworzenia_Int;
    [SerializeField] private Vector3 pozycja_spawnu_Vector3;
    [SerializeField] private float PredkoscWroga;
    [SerializeField] private int ZczytanyPoziomTrudnosci_Int;
    [SerializeField] private int ZczytanyiloscFal_Int;
    [SerializeField] private bool flaga_Aktywna_mechanika_Bool;
    private bool flaga_do_kontroli_eventu_Bool = false;
    private bool flaga_do_kontroli_eventu_spawnu_fali_Bool = false;
    [RuntimeInitializeOnLoadMethod]
    private void Awake()
    {
        flaga_Aktywna_mechanika_Bool = true;
       flaga_do_kontroli_eventu_spawnu_fali_Bool = false;
       ZczytanyPoziomTrudnosci_Int = LadowaniePlayerPrefs.GetDifficulty();
        ZczytanyiloscFal_Int = LadowaniePlayerPrefs.GetLiczbaFal();
        Debug.Log("Zczytana liczba fal z MechanikiFal :"+ ZczytanyiloscFal_Int.ToString());
        Instance = this;
        holder_Pozycji_Lista = new Transform[pozycja_spawnu_List.Count];
        wrog_lista = Resources.Load<Lista_Wrogowie_SO>("Wrogowie_Lista");
        int i = 0;
        foreach(Transform t in pozycja_spawnu_List)
        {
            holder_Pozycji_Lista[i] = t;
            i++;
        }
        Numer_Fali_INT = 1;
        status_Enum = status_wavemanager_enum.Oczekiwanie;
        pozycja_spawnu_Vector3 = pozycja_spawnu_List[UnityEngine.Random.Range(0, pozycja_spawnu_List.Count)].position;
        switch (ZczytanyPoziomTrudnosci_Int) // czas przed pierwsza fala 
        {
            case 1:
                czas_spawnu_nast_Fali_Float = 50f;
                break;
            case 2:
                czas_spawnu_nast_Fali_Float = 45f;
                break;
            case 3:
                czas_spawnu_nast_Fali_Float = 40f;
                break;
            default:
                czas_spawnu_nast_Fali_Float = 50f;
                break;
        }
        Debug.Log(czas_spawnu_nast_Fali_Float);

    }
   


    private void Update()
    {
        if (flaga_Aktywna_mechanika_Bool)
        {
            switch (status_Enum)
            {
                case status_wavemanager_enum.Oczekiwanie:
                    if (Numer_Fali_INT == ZczytanyiloscFal_Int + 1)
                    {
                        Debug.Log("osiagnieto fale bossa");
                        Fala_Bossa_event?.Invoke(this, EventArgs.Empty);
                        zmianaFali_event?.Invoke(this, EventArgs.Empty);
                        flaga_Aktywna_mechanika_Bool = false;
                        AktualnySk³adFali = wyznacznik_fali.ustalfale(pozostala_ilosc_wrogow_do_utworzenia_Int, true);
                        break;
                    }
                    if (flaga_do_kontroli_eventu_spawnu_fali_Bool == false)
                    {
                        if(ZczytanyPoziomTrudnosci_Int== 0)
                        {
                            pozostala_ilosc_wrogow_do_utworzenia_Int = 1 + 2 * Numer_Fali_INT;
                        }
                        else{
                            pozostala_ilosc_wrogow_do_utworzenia_Int = 1 + 2 * Numer_Fali_INT * ZczytanyPoziomTrudnosci_Int;
                        }
                        AktualnySk³adFali = wyznacznik_fali.ustalfale(pozostala_ilosc_wrogow_do_utworzenia_Int,false); // holder do zmiany przy imp budynków 
                        pozostala_ilosc_wrogow_do_utworzenia_Int = AktualnySk³adFali.Count;
                        flaga_do_kontroli_eventu_spawnu_fali_Bool = true;
                    }
                    if (flaga_do_kontroli_eventu_Bool == false)
                    {
                        zmianaFali_event?.Invoke(this, EventArgs.Empty);
                        flaga_do_kontroli_eventu_Bool = true;
                    }
                    czas_spawnu_nast_Fali_Float -= Time.deltaTime;
                    if (czas_spawnu_nast_Fali_Float < 0f)
                    {
                        TworzenieFali();
                    }
                    break;
                case status_wavemanager_enum.TworzenieFali:
                    if (pozostala_ilosc_wrogow_do_utworzenia_Int > 0)
                    {
                        odstep_miedzy_spawnem_wroga_Float -= Time.deltaTime;
                        if (odstep_miedzy_spawnem_wroga_Float < 0f)
                        {
                            odstep_miedzy_spawnem_wroga_Float = 0.5f;
                            wrog.Stworz(pozycja_spawnu_Vector3 * UnityEngine.Random.Range(0f, 5f), "pf_wrog_" + AktualnySk³adFali[0],
                                holder_Pozycji_Lista, PredkoscWroga); ; //UtilsClass.GetRandomDir() 
                            AktualnySk³adFali.RemoveAt(0);
                            //old wrog.Create(spawnPosition  * UnityEngine.Random.Range(0f, 5f),"pf_wrog_"+wrog_lista.wrogowie_so_Lista[0].wrog_Nazwa ,
                            //holderPozycji,PredkoscWroga); //UtilsClass.GetRandomDir() 
                            pozostala_ilosc_wrogow_do_utworzenia_Int--;
                            if (pozostala_ilosc_wrogow_do_utworzenia_Int <= 0)
                            {
                                flaga_do_kontroli_eventu_spawnu_fali_Bool = false;
                                pozycja_spawnu_Vector3 = pozycja_spawnu_List[UnityEngine.Random.Range(0, pozycja_spawnu_List.Count)].position;
                                czas_spawnu_nast_Fali_Float = Mathf.Clamp(30f - 4f * Numer_Fali_INT, 10f, 30f);
                                flaga_do_kontroli_eventu_Bool = false;
                                status_Enum = status_wavemanager_enum.Oczekiwanie;

                            }
                        }
                    }
                    break;
              
            }
        }
    }
    private void TworzenieFali()
    {
        
        status_Enum = status_wavemanager_enum.TworzenieFali;
        Numer_Fali_INT++;
        //zmianaFali_event?.Invoke(this, EventArgs.Empty);
        //OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetNumerFali()
    {
        return Numer_Fali_INT;
    }

    public float GetCzasSpawnuFali()
    {
        return czas_spawnu_nast_Fali_Float;
    }

    public Vector3 GetSpawnPosition()
    {
        return pozycja_spawnu_Vector3;
    }

}


