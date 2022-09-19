using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : Item
{
    [Header("===== Item Quantity =====")]
    public int MaxItemAmount;
    public int currentItemAmount;
    
    [Header("===== Item Model =====")]
    public GameObject ItemModel;

    public virtual void AttempTOConsumTime(PlayerInput playerInput , WeaponSlotManager weaponSlotManager , PlayerEffectManager playerEffectManager){
        
    }
}
