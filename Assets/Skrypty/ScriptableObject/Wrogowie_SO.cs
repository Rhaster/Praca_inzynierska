using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName =("ScriptableObjects/Wrogowie"))]
public class Wrogowie_SO : ScriptableObject
{
    [SerializeField] private string wrog_Nazwa;
    [SerializeField] private Transform wrog_Transform;
    [SerializeField] private List<float> wrog_rozdaj_pancerza_Lista;
}
