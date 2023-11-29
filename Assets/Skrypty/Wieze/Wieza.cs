using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wieza : MonoBehaviour
{
    [SerializeField] private float shootTimerMax;

    private float shootTimer_Float;
    private wrog targetEnemy_Wrog;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .2f;
    private Vector3 projectileSpawnPosition_Vector3;
    public float zasieg_wiezy_Float = 30f;
    private Transform wieza_sprite_rotacja_Transform;
    private Vector3 lastMoveDir;
    private void Awake()
    {
        wieza_sprite_rotacja_Transform = transform.Find("wieza");
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
        if (shootTimer_Float <= 0f)
        {
            shootTimer_Float += shootTimerMax;
            if (targetEnemy_Wrog != null)
            {
                rotacja();
                Debug.Log("strzelom");
                //ArrowProjectile.Create(projectileSpawnPosition, targetEnemy);
            }
        }
    }
    private void rotacja()
    {
        Vector3 moveDir;
        if (targetEnemy_Wrog != null)
        {
            moveDir = (targetEnemy_Wrog.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else
        {
            moveDir = lastMoveDir;
        }
        wieza_sprite_rotacja_Transform.eulerAngles = new Vector3(0, 0, Uzyteczne.GetAngleFromVector(moveDir));
    }
    private void LookForTargets()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, zasieg_wiezy_Float);

        foreach (Collider2D collider2D in collider2DArray)
        {
            wrog enemy = collider2D.GetComponent<wrog>();
            if (enemy != null)
            {
                // Is a enemy!
                if (targetEnemy_Wrog == null)
                {
                    targetEnemy_Wrog = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, targetEnemy_Wrog.transform.position))
                    {
                        // Closer!
                        targetEnemy_Wrog = enemy;
                    }
                }
            }
        }
    }
}
