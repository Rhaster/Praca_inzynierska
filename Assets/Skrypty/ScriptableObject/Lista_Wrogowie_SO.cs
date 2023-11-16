using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("ScriptableObjects/Wrogowie_Lista"))]
public class Lista_Wrogowie_SO : ScriptableObject
{
    [SerializeField] public List<Wrogowie_SO> wrogowie_so_Lista;
}
