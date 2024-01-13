using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_opcje : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Kontroler_UI.Instance.Rozwin_opcje += Instance_Rozwin_opcje;
    }

    private void Instance_Rozwin_opcje(object sender, System.EventArgs e)
    {
        transform.Find("OpcjeExpander").gameObject.SetActive(true);
    }

    // Update is called once per frame

}
