using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName =("ScriptableObjects/Wrogowie"))]
public class Wrogowie_SO : ScriptableObject
{
    [SerializeField] public string wrog_Nazwa;
    [SerializeField] private Transform wrog_Transform;
    [SerializeField] private List<Sprite> wrog_Lista_animacji_Lista;
}
