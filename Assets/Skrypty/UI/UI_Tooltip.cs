using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_Tooltip : MonoBehaviour
{
    #region Transformy 
    private Transform tlo_transform;
    #endregion
    private Dictionary<Wieze_SO, Transform> wieze_Slownik;
    private RectTransform tooltip_polozenie_RectTransform;
    // Start is called before the first frame update

        private void Awake()
        {
            Transform btnTemplate = transform.Find("Tooltip_template");
            btnTemplate.gameObject.SetActive(false);


        List<Wieze_SO> wieze_ = LadowaniePlayerPrefs.OdczytajListeWiez();
        wieze_Slownik = new Dictionary<Wieze_SO, Transform>();

            foreach (Wieze_SO buildingType in wieze_)
            {
            if (buildingType != null)
            {
                Transform btnTransform = Instantiate(btnTemplate, transform);
                btnTransform.GetComponent<UI_tooltip_content>().wieza_Wieza_SO = buildingType;
                btnTransform.gameObject.SetActive(false);
                wieze_Slownik[buildingType] = btnTransform;
            }
            }
        
    }

    // Update is called once per frame
    private void Start()
    {
        MechanikaBudowania.Instance.OnActiveBuildingTypeChanged += Instance_OnActiveBuildingTypeChanged;
    }

    private void Instance_OnActiveBuildingTypeChanged(object sender, MechanikaBudowania.OnActiveBuildingTypeChangedEventArgs e)
    {
        foreach (Wieze_SO wieza in wieze_Slownik.Keys)
        {
            wieze_Slownik[wieza].gameObject.SetActive(false);
        }
        if (e.aktywna_wieza_so!=null)
        {
            wieze_Slownik[e.aktywna_wieza_so].gameObject.SetActive(true);
        }


    }
}
