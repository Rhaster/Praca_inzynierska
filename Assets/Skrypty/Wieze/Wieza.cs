using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wieza : MonoBehaviour
{
    public static Wieza instance { get; private set; }
    [SerializeField] private float shootTimerMax;
   
    private float shootTimer_Float;
    [SerializeField] private wrog targetEnemy_Wrog;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .1f; // odswiezanie skanowania w poszukiwaniu celów 
    private Vector3 projectileSpawnPosition_Vector3;
    public float zasieg_wiezy_Float;
    [SerializeField]private Transform wieza_sprite_rotacja_Transform;
    [SerializeField] private Transform Punkt_wystrzalu_Transform;
    [SerializeField] private Transform PrefabPocisku_Testy_Transform;
    [SerializeField] public Amunicja_SO amunicja_Wybrana_Amunicja_SO;
    [SerializeField] private bool czy_przeladowano_bool;
    public event EventHandler zmianaCzasuPrzeladowania;
    private Vector3 lastMoveDir;
    private void Awake()
    {
        czy_przeladowano_bool = false;
        wieza_sprite_rotacja_Transform = transform.Find("wieza");
        Punkt_wystrzalu_Transform = wieza_sprite_rotacja_Transform.Find("pozycja_strzalu");
        //projectileSpawnPosition = transform.Find("projectileSpawnPosition").position;
    }

    private void Update()
    {
        if(amunicja_Wybrana_Amunicja_SO!= null)
        {
            HandleTargeting();
            HandleShooting();

        }

    }

    private void HandleTargeting()
    {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer < 0f)
        {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTargets();
        }
    }

    private void HandleShooting()
    {
        zmianaCzasuPrzeladowania?.Invoke(this, EventArgs.Empty);

        if (czy_przeladowano_bool == false)
        {
            shootTimer_Float -= Time.deltaTime;

        }
            if (targetEnemy_Wrog != null)
            {
                rotacja();
            }
            if (shootTimer_Float <= 0f && czy_przeladowano_bool == false)
            {
                shootTimer_Float += shootTimerMax;
                czy_przeladowano_bool = true;
            zmianaCzasuPrzeladowania?.Invoke(this, EventArgs.Empty);
        }
            if (czy_przeladowano_bool)
            {
                if (targetEnemy_Wrog != null)
                {
                    //rotacja();
                    Debug.Log("strzelom");
                    //if(MechanikaAmunicji.Instance.CzyStac())
                    Pociski.Create(amunicja_Wybrana_Amunicja_SO.amunicja_Transform, Punkt_wystrzalu_Transform.position, targetEnemy_Wrog);
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
        return shootTimer_Float;
    }
    public bool GetCzyPRzeladowano()
    {
        return czy_przeladowano_bool;
    }
}
