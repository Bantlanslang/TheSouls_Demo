using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponSlotManager : MonoBehaviour
{

    public WeaponItem rightweaponItem;
    public WeaponItem leftweaponItem;

    WeaponHolderSlot rightHandSlot;
    WeaponHolderSlot leftHandSlot;

    DamageCollider leftHandDamageCollider;
    DamageCollider rightHandDamageCollider;

    private void Awake() {

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

    private void Start() {
        LoadWeaponOnHands();
    }

    public void LoadWeaponOnHands(){
        if(rightweaponItem != null){
            LoadWeaponOnSlot(rightweaponItem,false);
        }
        else if(leftweaponItem != null){
            LoadWeaponOnSlot(leftweaponItem,true);
        }
    }
    public void LoadWeaponOnSlot(WeaponItem weapon, bool isleft){
        if(isleft){
            leftHandSlot.LoadWeaponModel(weapon);
            LoadWeaponDamageCollider(true);
        }
        else{
            rightHandSlot.LoadWeaponModel(weapon);
            LoadWeaponDamageCollider(false);
        }
    }
    public void LoadWeaponDamageCollider(bool isleft){
        if(isleft){
            leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }
        else{
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }
    }
    public void OpenDamageCollider(){
        rightHandDamageCollider.EnableDamageCollider();
    }
    public void CloseDamageCollider(){
        rightHandDamageCollider.DisableDamageCollider();
    }
}
