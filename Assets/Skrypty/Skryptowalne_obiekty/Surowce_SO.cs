using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName =("ScriptableObjects/Surowce"))]
public class Surowce_SO : ScriptableObject
{
    [SerializeField] public string surowiec_nazwa_String;
    [SerializeField] public Transform surowiec_nazwa_Transform;
    [SerializeField] public Sprite surowiec_sprite;

}
