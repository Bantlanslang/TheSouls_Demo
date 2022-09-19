using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    public WeaponHolderSlot leftHandSlot;
    public WeaponHolderSlot rightHandSlot;
    DamageCollider leftHandDamageCollider;
    DamageCollider RightHandDamageCollider;
    ActorManager actorManager;
    PlayerManager playerManager;
    PlayerInventory playerInventory;
    public WeaponItem weaponItem;

    QuickSlotUI quickSlotUI;

    PlayerState playerState;


    private void Awake(){
        //seach model weapon...
        //TODO Enemy Attack
        quickSlotUI = FindObjectOfType<QuickSlotUI>();
        playerState = GetComponentInParent<PlayerState>();
        playerManager = GetComponentInParent<PlayerManager>();
        actorManager = GetComponentInParent<ActorManager>();
        playerInventory = GetComponentInParent<PlayerInventory>();

        WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
        foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
        {   
            if(weaponSlot.isLeftHandSlot){
                leftHandSlot = weaponSlot;
            }
            else if(weaponSlot.isRightHandSlot){
                rightHandSlot = weaponSlot;
            }
        }
    }

    public void LoadBothWeaponOnSlot(){
        LoadWeaponOnSlot(playerInventory.rightWeapon ,false);
        LoadWeaponOnSlot(playerInventory.leftWeapon ,true);
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isleft){
        if(isleft){
            leftHandSlot.LoadWeaponModel(weaponItem);
            LoadLeftHandDamageCollider();
            quickSlotUI.UpdateWeaponQuickSlotUI(weaponItem,true);
        }
        else if(isleft == false){
            rightHandSlot.LoadWeaponModel(weaponItem);
            LoadRightHandDamageCollider();
            quickSlotUI.UpdateWeaponQuickSlotUI(weaponItem,false);
        }
    }

    public void LoadLeftHandDamageCollider(){
        leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }
    public void LoadRightHandDamageCollider(){
        RightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }


    //Weapon Open&Close
    #region WeaponDamageCollider...

    public void OpenDamageCollider(){
        if(playerManager.isUsingLeftHand){
            leftHandDamageCollider.EnableDamageCollider();
        }
        else if(playerManager.isUsingRightHand){
            RightHandDamageCollider.EnableDamageCollider();
        }
    }
    
    public void CloseDamageCollider(){
        leftHandDamageCollider.DisableDamageCollider();
        RightHandDamageCollider.DisableDamageCollider();
    }
    

    #endregion

    #region DrainStamina
    public void DrainlightAttackStamina(){
        
        playerState.TakeStamina(Mathf.RoundToInt(weaponItem.BaseStamina * weaponItem.lightAttackCost));
        
    }
    public void DrainheavyAttackStamina(){
        
        playerState.TakeStamina(Mathf.RoundToInt(weaponItem.BaseStamina * weaponItem.heavyAttackCost));
        
    }
    
    #endregion


}
