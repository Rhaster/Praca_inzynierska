using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class MechanikaEkonomi : MonoBehaviour
{
    public static MechanikaEkonomi Instance { get; private set; }


    public event EventHandler OnResourceAmountChanged;

    [SerializeField] private List<StartowaIloscSur> startingResourceAmountList;

    private Dictionary<Surowce_SO, int> resourceAmountDictionary;

    private void Awake()
    { 
      
        Instance = this;

        resourceAmountDictionary = new Dictionary<Surowce_SO, int>();

        Lista_Surowce_SO resourceTypeList = Resources.Load<Lista_Surowce_SO>(typeof(Lista_Surowce_SO).Name);

        foreach (Surowce_SO resourceType in resourceTypeList.surowce_lista)
        {
            resourceAmountDictionary[resourceType] = 0;
        }

        foreach (StartowaIloscSur resourceAmount in startingResourceAmountList)
        {
            AddResource(resourceAmount.surowiec, resourceAmount.ilosc);
        }
    }

    private void TestLogResourceAmountDictionary()
    {
        foreach (Surowce_SO resourceType in resourceAmountDictionary.Keys)
        {
            Debug.Log(resourceType.surowiec_nazwa_String + ": " + resourceAmountDictionary[resourceType]);
        }
    }
    private void Update()
    {
        TestLogResourceAmountDictionary();
    }
    public void AddResource(Surowce_SO resourceType, int amount)
    {
        resourceAmountDictionary[resourceType] += amount;

        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(Surowce_SO resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }

    public bool CanAfford(StartowaIloscSur[] resourceAmountArray)
    {
        foreach (StartowaIloscSur resourceAmount in resourceAmountArray)
        {
            if (GetResourceAmount(resourceAmount.surowiec) >= resourceAmount.ilosc)
            {
                // Can afford
            }
            else
            {
                // Cannot afford this!
                return false;
            }
        }

        // Can afford all
        return true;
    }

    public void SpendResources(StartowaIloscSur[] resourceAmountArray)
    {
        foreach (StartowaIloscSur resourceAmount in resourceAmountArray)
        {
            resourceAmountDictionary[resourceAmount.surowiec] -= resourceAmount.ilosc;
        }
    }
}
