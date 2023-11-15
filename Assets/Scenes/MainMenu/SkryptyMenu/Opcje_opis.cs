using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opcje_opis : MonoBehaviour
{
    private Transform t�o_transform;
    private Transform przycisk_transform;
    private Transform wyjscie_transform;
    private void Awake()
    {
        t�o_transform = transform.Find("T�o");
        wyjscie_transform = transform.Find("wyjscie");
        //przycisk_transform = transform.Find("")
        gameObject.SetActive(false);
    }
    private void Start()
    {
        //transform.Find("T�o").gameObject.SetActive(false);

        wyjscie_transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            ButtonHandler xd = wyjscie_transform.GetComponent<ButtonHandler>();
            xd.Reset();
            gameObject.SetActive(false);
            FlowManager.Instance.gameObject.SetActive(true);
        });
    }


}
