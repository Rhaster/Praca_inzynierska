using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Podswietlenie_budynku : MonoBehaviour
{
    private GeneratorSurowcow holder_Generator_surowcow;
    private GeneratorAmunicji holder_Generator_Amunicji;
    private Transform podswiellenie_Transform;
    // Start is called before the first frame update
    private void Awake()
    {
        podswiellenie_Transform = transform.Find("glow");
        podswiellenie_Transform.gameObject.SetActive(false);
        

    }
    void Start()
    {
        holder_Generator_surowcow = GetComponent<GeneratorSurowcow>();
        if (holder_Generator_surowcow != null)
        {
            holder_Generator_surowcow.ZmianaEnergiEvent += Instance_ZmianaEnergiEvent;
        }
        else
        {
            holder_Generator_Amunicji = GetComponent<GeneratorAmunicji>();
            if (holder_Generator_Amunicji !=null)
            {
                holder_Generator_Amunicji.ZmianaEnergiEvent += Holder_Generator_surowcow_ZmianaEnergiEvent;
            }
           
        }
    }

    private void Holder_Generator_surowcow_ZmianaEnergiEvent(object sender, System.EventArgs e)
    {
        if (holder_Generator_Amunicji.getIloscEnergi() == 0)
        {
            podswiellenie_Transform.gameObject.SetActive(false);
        }
        else
        {
            podswiellenie_Transform.gameObject.SetActive(true);
        }
    }

    private void Instance_ZmianaEnergiEvent(object sender, System.EventArgs e)
    {
        if(holder_Generator_surowcow.getIloscEnergi() ==0)
        {
            podswiellenie_Transform.gameObject.SetActive(false);
        }
        else
        {
            podswiellenie_Transform.gameObject.SetActive(true);
        }
    }


}
