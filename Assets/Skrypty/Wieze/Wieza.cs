using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wieza : MonoBehaviour
{
    [SerializeField] private float shootTimerMax;

    private float shootTimer;
    private wrog targetEnemy;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .2f;
    private Vector3 projectileSpawnPosition;

    private void Awake()
    {
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
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            shootTimer += shootTimerMax;
            if (targetEnemy != null)
            {
                Debug.Log("strzelom");
                //ArrowProjectile.Create(projectileSpawnPosition, targetEnemy);
            }
        }
    }

    private void LookForTargets()
    {
        float targetMaxRadius = 30f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            wrog enemy = collider2D.GetComponent<wrog>();
            if (enemy != null)
            {
                // Is a enemy!
                if (targetEnemy == null)
                {
                    targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        // Closer!
                        targetEnemy = enemy;
                    }
                }
            }
        }
    }
}
