using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/weapon Item")]
public class WeaponItem : Item
{
    public GameObject modelPrefab;
    public bool isUnarmed;

    //[Header("One Hand Attack animation")]
    //public string OH_Light_Attack1;
    //public string OH_Heavy_Attack1;
    [Header("Stamina Costs")]
    public int BaseStamina;
    public int AttackValue;
    public float lightAttackCost;
    public float heavyAttackCost;

}
