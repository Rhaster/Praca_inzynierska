using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_UI : MonoBehaviour
{
    [SerializeField] private SystemHP SystemHP;

    private Transform barTransform;
    private Transform separatorContainer;

    private void Awake()
    {
        barTransform = transform.Find("maska");
    }

    private void Start()
    {
 

        SystemHP.OnDamaged += HealthSystem_OnDamaged;
        SystemHP.OnHealed += HealthSystem_OnHealed;
        SystemHP.OnHealthAmountMaxChanged += HealthSystem_OnHealthAmountMaxChanged;

        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnHealthAmountMaxChanged(object sender, System.EventArgs e)
    {
        
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
        barTransform.localScale = new Vector3(SystemHP.GetHealthAmountNormalized(), 1, 1);
    }

    private void UpdateHealthBarVisible()
    {
        if (SystemHP.IsFullHealth())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
