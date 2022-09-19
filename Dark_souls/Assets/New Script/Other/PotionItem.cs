using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumable/Potion")]
public class PotionItem : ConsumableItem
{
    [Header("===== HeathRecover Amount=====")]
    public int HeathRecoverAmount = 30;

    [Header("===== Recover FX =====")]
    //回復粒子動畫
    public GameObject RecoverFX;

    public override void AttempTOConsumTime(PlayerInput playerInput,WeaponSlotManager weaponSlotManager,PlayerEffectManager playerEffectManager)
    {
        //ADD Health
        //播放粒子動畫
        base.AttempTOConsumTime(playerInput,weaponSlotManager,playerEffectManager);
        GameObject potion = Instantiate(ItemModel,weaponSlotManager.leftHandSlot.transform);
        playerEffectManager.currentParticle = RecoverFX;
        playerEffectManager.HeathAmount = HeathRecoverAmount;
        playerEffectManager.potionModel = potion;
        weaponSlotManager.leftHandSlot.UnloadWeapon();
    }
}
