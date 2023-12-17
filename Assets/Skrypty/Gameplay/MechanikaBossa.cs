using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanikaBossa : MonoBehaviour
{
    public static MechanikaBossa instance;
    public event EventHandler wygrana_event;
    public SystemHP syshp;
    private void Awake()
    {
        instance= this;
    }
    // Start is called before the first frame update
    void Start()
    {
        syshp= GetComponent<SystemHP>();
        syshp.OnDied += Syshp_OnDied;
    }

    private void Syshp_OnDied(object sender, EventArgs e)
    {
        wygrana_event?.Invoke(this, EventArgs.Empty);
    }

    public void wyznaczHP(int x)
    {
        syshp.SetHealthAmountMax(x,true);
    }

}
