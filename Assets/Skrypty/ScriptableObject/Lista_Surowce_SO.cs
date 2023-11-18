using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = ("ScriptableObjects/Surowce_Lista"))]
public class Lista_Surowce_SO : ScriptableObject
{
    [SerializeField] public List<Surowce_SO> surowce_lista;
}
