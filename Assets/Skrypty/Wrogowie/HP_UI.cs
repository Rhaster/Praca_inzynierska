using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_UI : MonoBehaviour
{
    [SerializeField] private SystemHP SystemHP;

    private Transform barTransform;

    private void Awake()
    {
        barTransform = transform.Find("maska");
    }

    private void Start()
    {
 

        SystemHP.Zadano_Obrazenia += HealthSystem_OnDamaged;
        SystemHP.Uzdrowiono += HealthSystem_OnHealed;
 

        UpdateBar();
        UpdateHealthBarVisible();
    }



    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }



    private void UpdateBar()
    {
        barTransform.localScale = new Vector3(SystemHP.Get_znormalizowane_hp(), 1, 1);
    }

    private void UpdateHealthBarVisible()
    {
        if (SystemHP.Czy_pelne_hp())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
