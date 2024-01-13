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
 

        SystemHP.Zadano_Obrazenia += SystemHP_ZadanoObrazenia;
        SystemHP.Uzdrowiono += SystemHP_Uleczono;
 

        Zaktualizuj_bar();
        Schowaj_Bar_lub_pokaz();
    }



    private void SystemHP_Uleczono(object sender, System.EventArgs e)
    {
        Zaktualizuj_bar();
        Schowaj_Bar_lub_pokaz();
    }

    private void SystemHP_ZadanoObrazenia(object sender, System.EventArgs e)
    {
        Zaktualizuj_bar();
        Schowaj_Bar_lub_pokaz();
    }



    private void Zaktualizuj_bar()
    {
        barTransform.localScale = new Vector3(SystemHP.Get_znormalizowane_hp(), 1, 1);
    }

    private void Schowaj_Bar_lub_pokaz()
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
