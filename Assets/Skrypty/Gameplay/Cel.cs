using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class Cel : MonoBehaviour
{
    public static Cel instance;
    public event EventHandler porazka_event;
    public SystemHP syshp;
    private void Awake()
    {
        instance= this;
        syshp = GetComponent<SystemHP>();
    }
    private void Start()
    {
        syshp.Zgon += Syshp_OnDied;
    }
    private void Syshp_OnDied(object sender, System.EventArgs e)
    {
        porazka_event?.Invoke(this, EventArgs.Empty);
    }
}
