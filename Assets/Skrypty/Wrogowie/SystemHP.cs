using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemHP : MonoBehaviour
{
    public event EventHandler Zmiana_Max_HP;
    public event EventHandler Zadano_Obrazenia;
    public event EventHandler Uzdrowiono;
    public event EventHandler Zgon;

    [SerializeField] private int maxymalne_hp_Int;
    [SerializeField] private int aktualne_hp_Int;

    private void Awake()
    {

        aktualne_hp_Int = maxymalne_hp_Int;
    }

    public void Obrazenia(int damageAmount)
    {
        aktualne_hp_Int -= damageAmount;
        aktualne_hp_Int = Mathf.Clamp(aktualne_hp_Int, 0, maxymalne_hp_Int);

        Zadano_Obrazenia?.Invoke(this, EventArgs.Empty);

        if (IsDead())
        {
            if(tag.Equals("wrog"))
            {
                MechanikaStatystyk.instance.IncreaseKilledUnits();
            }
            Zgon?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }
    }

    public void Heal(int healAmount)
    {
        aktualne_hp_Int += healAmount;
        aktualne_hp_Int = Mathf.Clamp(aktualne_hp_Int, 0, maxymalne_hp_Int);

        Uzdrowiono?.Invoke(this, EventArgs.Empty);
    }

    public void HealFull()
    {
        aktualne_hp_Int = maxymalne_hp_Int;

        Uzdrowiono?.Invoke(this, EventArgs.Empty);
    }

    public bool IsDead()
    {
        return aktualne_hp_Int == 0;
    }

    public bool Czy_pelne_hp()
    {
        return aktualne_hp_Int == maxymalne_hp_Int;
    }

    public int Get_aktualne_hp()
    {
        return aktualne_hp_Int;
    }

    public int Get_maxymalne_hp()
    {
        return maxymalne_hp_Int;
    }

    public float Get_znormalizowane_hp()
    {
        return (float)aktualne_hp_Int / maxymalne_hp_Int;
    }

    public void Set_maxymalne_hp(int nowe_hp_max_Int, bool czy_zaktualizowac_Bool)
    {
        this.maxymalne_hp_Int = nowe_hp_max_Int;

        if (czy_zaktualizowac_Bool)
        {
            aktualne_hp_Int = nowe_hp_max_Int;
        }

        Zmiana_Max_HP?.Invoke(this, EventArgs.Empty);
    }
}
