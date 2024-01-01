using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pociski : MonoBehaviour
{
    int czyobszarowe;
    int obrazenia;
    int numerFali;
    
    Amunicja_SO amunicja_Local;
    public static Pociski Create(Transform pocisk_prefab, Vector3 position, wrog enemy, int obrazenia, Amunicja_SO amunicja, int czyobszarowe = 0)
    {
        Transform pocisk_Transform = Instantiate(pocisk_prefab, position, Quaternion.identity);

        Pociski Pocisk = pocisk_Transform.GetComponent<Pociski>();
        Pocisk.SetTarget(enemy);
        Pocisk.czyobszarowe = czyobszarowe;
        if(czyobszarowe> 0)
        {
            Pocisk.transform.localScale *= 3;
        }
        Pocisk.numerFali = MechanikaFal.Instance.GetNumerFali();
        Pocisk.obrazenia= (int)(obrazenia + (obrazenia* (Pocisk.numerFali * 0.04f)));
        Debug.Log(Pocisk.obrazenia);
        Pocisk.amunicja_Local = amunicja;
        return Pocisk;
    }



    private wrog cel_Wrog;
    private Vector3 ostatni_kierunek_Vector3;
    private float czas_do_exploji_float = 4f;

    private void Update()
    {
        Vector3 moveDir;

        if (cel_Wrog != null)
        {
            moveDir = (cel_Wrog.transform.position - transform.position).normalized;
            ostatni_kierunek_Vector3 = moveDir;
        }
        else
        {
            moveDir = ostatni_kierunek_Vector3;
        }

        float moveSpeed = 30f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, 0, Uzyteczne.GetAngleFromVector(moveDir));

        czas_do_exploji_float -= Time.deltaTime;
        if (czas_do_exploji_float < 0f)
        {
            Destroy(gameObject);
        }
    }

    private void SetTarget(wrog targetEnemy)
    {
        this.cel_Wrog = targetEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float mnoznik = collision.GetComponent<wrog>().amunicja_so[amunicja_Local];
        if (czyobszarowe == 0)
        {
            wrog enemy = collision.GetComponent<wrog>();
            if (enemy != null)
            {
                enemy.GetComponent<SystemHP>().Obrazenia((int)(obrazenia* mnoznik));
                //Debug.Log("obrazenia bez mnoznika" + obrazenia.ToString());
                //Debug.Log("obrazenia z mnoznika" +( (int)(obrazenia * mnoznik)).ToString());
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
                    enemy.GetComponent<SystemHP>().Obrazenia((int)(obrazenia* mnoznik));
                   
                }
            }
            Destroy(gameObject);
        }
    }
}
