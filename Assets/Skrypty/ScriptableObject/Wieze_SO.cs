using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(menuName = ("ScriptableObjects/Wieze"))]
public class Wieze_SO : ScriptableObject
{
    [SerializeField] private string wieza_Nazwa;
    [SerializeField] private Transform wieza_Transform;
    [SerializeField] private bool wieza_czyobsadzona_bool;
    [SerializeField] private float wieza_zasiegataku_float;
    [SerializeField] private RodzajAtaku wieza_rodzajataku_Enum;


    public enum RodzajAtaku
    {
        PojedynczyCel,
        Obszarowy,
    }
}
