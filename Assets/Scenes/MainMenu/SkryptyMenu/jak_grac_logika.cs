using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class jak_grac_logika : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    void Start()
    {
        Transform wyjscie = transform.Find("wyjscie");
        wyjscie.GetComponent<Button>().onClick.AddListener(() =>
        {
            gameObject.SetActive(false);

        });
    }
    private void OnDisable()
    {
        gameObject.SetActive(false);
    }


}
