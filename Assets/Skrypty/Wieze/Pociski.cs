using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pociski : MonoBehaviour
{
    int czyobszarowe;
    int obrazenia;
    Amunicja_SO amunicja_Local;
    public static Pociski Create(Transform pocisk_prefab, Vector3 position, wrog enemy, int obrazenia, Amunicja_SO amunicja, int czyobszarowe = 0)
    {
        Transform arrowTransform = Instantiate(pocisk_prefab, position, Quaternion.identity);

        Pociski arrowProjectile = arrowTransform.GetComponent<Pociski>();
        arrowProjectile.SetTarget(enemy);
        arrowProjectile.czyobszarowe = czyobszarowe;
        arrowProjectile.obrazenia= obrazenia;
        arrowProjectile.amunicja_Local = amunicja;
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
        float mnoznik = collision.GetComponent<wrog>().amunicja_so[amunicja_Local];
        if (czyobszarowe == 0)
        {
            wrog enemy = collision.GetComponent<wrog>();
            if (enemy != null)
            {
                enemy.GetComponent<SystemHP>().Damage((int)(obrazenia* mnoznik));
                Debug.Log("obrazenia bez mnoznika" + obrazenia.ToString());
                Debug.Log("obrazenia z mnoznika" +( (int)(obrazenia * mnoznik)).ToString());
                Destroy(gameObject);
            }
        }
        else
        {
            Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, czyobszarowe);
            wrog enemy = null;
            foreach (Collider2D collider2D in collider2DArray)
            {
                enemy = collider2D.GetComponent<wrog>();
                if (enemy != null)
                {
                    enemy.GetComponent<SystemHP>().Damage((int)(obrazenia* mnoznik));
                   
                }
            }
            Destroy(gameObject);
        }
    }
}
