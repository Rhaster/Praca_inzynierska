using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = ("ScriptableObjects/Wieze"))]
public class Wieze_SO : ScriptableObject
{
    [SerializeField] public string wieza_Nazwa;
    [SerializeField] public Transform wieza_Transform;
    [SerializeField] public float wieza_zasiegataku_float;
    [SerializeField] public RodzajAtaku wieza_rodzajataku_Enum;
    [SerializeField] public List<StartowaIloscSur> koszt_StartowaIloscSur_Lista;
    [SerializeField] public float Obrazenia_wiezy_Float;
    [SerializeField] public float Czas_przeladowania_wiezy_Float;
    public Sprite wieza_Sprite;


    public enum RodzajAtaku
    {
        PojedynczyCel,
        Obszarowy,
    }
}
