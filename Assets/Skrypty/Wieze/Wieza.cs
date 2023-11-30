using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wieza : MonoBehaviour
{
    [SerializeField] private float shootTimerMax;
   
    private float shootTimer_Float;
    [SerializeField] private wrog targetEnemy_Wrog;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .1f;
    private Vector3 projectileSpawnPosition_Vector3;
    public float zasieg_wiezy_Float;
    [SerializeField]private Transform wieza_sprite_rotacja_Transform;
    [SerializeField] private Transform Punkt_wystrzalu_Transform;
    [SerializeField] private Transform PrefabPocisku_Testy_Transform;
    private Vector3 lastMoveDir;
    private void Awake()
    {
        wieza_sprite_rotacja_Transform = transform.Find("wieza");
        Punkt_wystrzalu_Transform = wieza_sprite_rotacja_Transform.Find("pozycja_strzalu");
        //projectileSpawnPosition = transform.Find("projectileSpawnPosition").position;
    }

    private void Update()
    {
        HandleTargeting();
        HandleShooting();
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

        shootTimer_Float -= Time.deltaTime;
        if(targetEnemy_Wrog!= null)
        {
            rotacja();
        }
        if (shootTimer_Float <= 0f)
        {
            shootTimer_Float += shootTimerMax;
            if (targetEnemy_Wrog != null)
            {
                rotacja();
                Debug.Log("strzelom");
                Pociski.Create(PrefabPocisku_Testy_Transform,Punkt_wystrzalu_Transform.position, targetEnemy_Wrog);
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
}
