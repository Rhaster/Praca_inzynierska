using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Wieze_SO;

[CreateAssetMenu(menuName =("ScriptableObjects/Amunicja_SO"))]
public class Amunicja_SO : ScriptableObject
{
    public string amunicja_nazwa_String;
    public Transform amunicja_Transform;
    public RodzajObrazen_Enum amunicja_rodzaj_obrazen_Enum;
    public enum RodzajObrazen_Enum
    {
        Wybuchowy,
        Sieczny,
        Chemiczny,
    }
    public Sprite amunicja_Sprite;
}
