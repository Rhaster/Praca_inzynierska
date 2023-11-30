using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pociski : MonoBehaviour
{
    public static Pociski Create(Transform pocisk_prefab,Vector3 position, wrog enemy)
    {
        Transform arrowTransform = Instantiate(pocisk_prefab, position, Quaternion.identity);

        Pociski arrowProjectile = arrowTransform.GetComponent<Pociski>();
        arrowProjectile.SetTarget(enemy);

        return arrowProjectile;
    }



    private wrog targetEnemy;
    private Vector3 lastMoveDir;
    private float timeToDie = 4f;

    private void Update()
    {
        Vector3 moveDir;

        if (targetEnemy != null)
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else
        {
            moveDir = lastMoveDir;
        }

        float moveSpeed = 30f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, 0, Uzyteczne.GetAngleFromVector(moveDir));

        timeToDie -= Time.deltaTime;
        if (timeToDie < 0f)
        {
            Destroy(gameObject);
        }
    }

    private void SetTarget(wrog targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        wrog enemy = collision.GetComponent<wrog>();
        if (enemy != null)
        {
            // Hit an enemy!
            int damageAmount = 100;
            enemy.GetComponent<SystemHP>().Damage(damageAmount);

            Destroy(gameObject);
        }
    }
}
