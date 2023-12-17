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
        separatorContainer = transform.Find("separator");
        PostawSeparatory();

        SystemHP.OnDamaged += HealthSystem_OnDamaged;
        SystemHP.OnHealed += HealthSystem_OnHealed;
        SystemHP.OnHealthAmountMaxChanged += HealthSystem_OnHealthAmountMaxChanged;

        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnHealthAmountMaxChanged(object sender, System.EventArgs e)
    {
        PostawSeparatory();
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

    private void PostawSeparatory()
    {
        Transform separatorTemplate = separatorContainer.Find("Template");
        separatorTemplate.gameObject.SetActive(false);

        foreach (Transform separatorTransform in separatorContainer)
        {
            if (separatorTransform == separatorTemplate) continue;
            Destroy(separatorTransform.gameObject);
        }


        int healthAmountPerSeparator = 10;
        float barSize = 3f;
        float barOneHealthAmountSize = barSize / SystemHP.GetHealthAmountMax();
        int healthSeparatorCount = Mathf.FloorToInt(SystemHP.GetHealthAmountMax() / healthAmountPerSeparator);

        for (int i = 1; i < healthSeparatorCount; i++)
        {
            Transform separatorTransform = Instantiate(separatorTemplate, separatorContainer);
            separatorTransform.gameObject.SetActive(true);
            separatorTransform.localPosition = new Vector3(barOneHealthAmountSize * i * healthAmountPerSeparator, 0, 0);
        }
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
