using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wieza : MonoBehaviour
{
    public static Wieza instance { get; private set; }
    [SerializeField] private float czas_przeladowania_Timermax;
    public Wieze_SO holder_Wieza_SO;
    [SerializeField] private float shootTimer_Float;
    [SerializeField] private wrog targetEnemy_Wrog;
    [SerializeField] private float szukanie_celu_TimerMax =1;
    [SerializeField] private float lookForTargetTimerMax = .1f; // odswiezanie skanowania w poszukiwaniu celów 
    private Vector3 projectileSpawnPosition_Vector3;
    public float zasieg_wiezy_Float;
    [SerializeField]private Transform wieza_sprite_rotacja_Transform;
    [SerializeField] private Transform Punkt_wystrzalu_Transform;
    [SerializeField] private Transform PrefabPocisku_Testy_Transform;
    [SerializeField] public Amunicja_SO amunicja_Wybrana_Amunicja_SO;
    [SerializeField] private bool czy_przeladowano_bool;
    private float ograniczenie_sprawdzania_amunicji_float;
    private float ograniczenie_sprawdzania_amunicji_wart_normalna_float;
    private bool flaga_czy_stac_bool;
    private bool flaga_czy_oplacono_bool;
    public event EventHandler<Status> zmianaCzasuPrzeladowania;
    [SerializeField] private float OBrazenia_wiezy_Float;
    [SerializeField] private float rodzaj_wiezy_Float;
    private UI_Wieze_Zasieg_klikalnosc instancja_ui_wiezy;
    private Amunicja_SO poprzednia_amunicja_holder;
    public class Status
    { 
      public String status_Wiezy;
    };

private Vector3 lastMoveDir;
    private void Awake()
    {
        
        flaga_czy_oplacono_bool = false;
        ograniczenie_sprawdzania_amunicji_wart_normalna_float = 0;
        shootTimer_Float = czas_przeladowania_Timermax;
        czy_przeladowano_bool = false;
        wieza_sprite_rotacja_Transform = transform.Find("wieza");
        Punkt_wystrzalu_Transform = wieza_sprite_rotacja_Transform.Find("pozycja_strzalu");
        holder_Wieza_SO = GetComponent<HolderRodzajuWiezy>().holderWiezy;
        OBrazenia_wiezy_Float = holder_Wieza_SO.Obrazenia_wiezy_Float;
        czas_przeladowania_Timermax = holder_Wieza_SO.Czas_przeladowania_wiezy_Float;
        rodzaj_wiezy_Float = holder_Wieza_SO.wieza_zasieg_ataku_amunicji_Float;
        zasieg_wiezy_Float = holder_Wieza_SO.wieza_zasiegataku_float;
        instancja_ui_wiezy = GetComponent<UI_Wieze_Zasieg_klikalnosc>();
        //projectileSpawnPosition = transform.Find("projectileSpawnPosition").position;
       
    }

    private void Update()
    {
        if (amunicja_Wybrana_Amunicja_SO != null)
        {
            HandleTargeting();
            HandleShooting();

        }

    }

    private void HandleTargeting()
    {
        szukanie_celu_TimerMax -= Time.deltaTime;
        if (szukanie_celu_TimerMax < 0f)
        {
            szukanie_celu_TimerMax += lookForTargetTimerMax;
            LookForTargets();
        }
    }
    private void Start()
    {
        instancja_ui_wiezy.zmianaAmunicji += Instancja_ui_wiezy_zmianaAmunicji;
    }

    private void Instancja_ui_wiezy_zmianaAmunicji(object sender, EventArgs e)
    {
        if(czy_przeladowano_bool)
        {
            czy_przeladowano_bool = false;
            flaga_czy_oplacono_bool= false;
            MechanikaAmunicji.Instance.strzel(poprzednia_amunicja_holder, -1);
        
        }
    }

    private void HandleShooting()
    {

        if (czy_przeladowano_bool ==false)
        {
            if (MechanikaAmunicji.Instance.CzyStac_na_Strzal(amunicja_Wybrana_Amunicja_SO) || flaga_czy_oplacono_bool)
            {
                if(flaga_czy_oplacono_bool == false)
                {
                    MechanikaAmunicji.Instance.strzel(amunicja_Wybrana_Amunicja_SO,1);
                    poprzednia_amunicja_holder = amunicja_Wybrana_Amunicja_SO;
                    flaga_czy_oplacono_bool = true;
                }
                if (czy_przeladowano_bool == false)
                {
                    zmianaCzasuPrzeladowania?.Invoke(this, new Status { status_Wiezy = "Przeładowanie" });
                    shootTimer_Float -= Time.deltaTime;
                    if(amunicja_Wybrana_Amunicja_SO != poprzednia_amunicja_holder) // Flaga by nie oszukiwac zmieniajac amunicje
                    {
                        flaga_czy_oplacono_bool = false;
                        MechanikaAmunicji.Instance.strzel(poprzednia_amunicja_holder, -1);
                        shootTimer_Float = czas_przeladowania_Timermax;
                    }

                }
                if (targetEnemy_Wrog != null)
                {
                    rotacja();
                }
                if (shootTimer_Float <= 0f && czy_przeladowano_bool == false)
                {
                    
                        shootTimer_Float += czas_przeladowania_Timermax;
                        czy_przeladowano_bool = true;
                        zmianaCzasuPrzeladowania?.Invoke(this, new Status { status_Wiezy = "Załadowana" });
                        flaga_czy_oplacono_bool = false;
                    
    

                }
            }
            else
            {
                Debug.Log("brak amunicji");
                zmianaCzasuPrzeladowania?.Invoke(this, new Status { status_Wiezy = "Brak amunicji" });
            }
        }
        if (czy_przeladowano_bool)
        {
            if (targetEnemy_Wrog != null)
            {


                Debug.Log("strzelom");

                Pociski.Create(amunicja_Wybrana_Amunicja_SO.amunicja_Transform, Punkt_wystrzalu_Transform.position, targetEnemy_Wrog,(int) OBrazenia_wiezy_Float, (int)rodzaj_wiezy_Float);
                czy_przeladowano_bool = false;
            }

        }
    }

    private void rotacja()
    {
        Vector3 moveDir;

        if (targetEnemy_Wrog == null)
        {
            return;
        }
        if (targetEnemy_Wrog != null)
        {
            moveDir = (targetEnemy_Wrog.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else
        {
            moveDir = lastMoveDir;
        }

        if (moveDir != Vector3.zero)
        {
            float angle = Uzyteczne.GetAngleFromVector(moveDir);
            wieza_sprite_rotacja_Transform.eulerAngles = new Vector3(0, 0, angle - 90);

        }
        
    }
    private void UstawUzywanaAmunicje(Amunicja_SO amu)
    {

    }
    private void LookForTargets()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, zasieg_wiezy_Float);
        bool check = false;
        wrog enemy=null;
        foreach (Collider2D collider2D in collider2DArray)
        {
            enemy = collider2D.GetComponent<wrog>();
            if (enemy != null)
            {
                // Is a enemy!
                if (targetEnemy_Wrog == null)
                {
                    check = true;
                    targetEnemy_Wrog = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, targetEnemy_Wrog.transform.position))
                    {
                        // Closer!
                        targetEnemy_Wrog = enemy;
                        check = true;
                    }
                }
            }
        }
        if(check == false)
        {
            targetEnemy_Wrog = null;
        }
    }
    public float GetCzasPrzeladowania()
    {
        return shootTimer_Float/ czas_przeladowania_Timermax;
    }
    public bool GetCzyPRzeladowano()
    {
        return czy_przeladowano_bool;
    }
}
