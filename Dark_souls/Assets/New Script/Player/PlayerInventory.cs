using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public WeaponSlotManager weaponSlotManager;
    public Transform actor;
    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;
    public ConsumableItem currentConsumable;

    //空手武器
    public WeaponItem unarmedWeapon;
    
    public WeaponItem[] weaponInRightHandSlots = new WeaponItem[1];
    public WeaponItem[] weaponInLeftHandSlots = new WeaponItem[1];

    public int currentRightWeaponIndex = 0;
    public int currentLeftWeaponIndex = 0;

    public List<WeaponItem> weaponInventory;


    private void AWake(){

    }
    private void OnEnable() {
        
        actor = transform.GetChild(0);
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
    }
    private void Start() {

        rightWeapon = weaponInRightHandSlots[currentRightWeaponIndex];
        leftWeapon = weaponInLeftHandSlots[currentLeftWeaponIndex];
        weaponSlotManager.LoadWeaponOnSlot(rightWeapon,false);
        weaponSlotManager.LoadWeaponOnSlot(leftWeapon,true);

    }
    //Change weapon
    public void ChangeRightWeapon(){
        currentRightWeaponIndex += 1;
        if(currentRightWeaponIndex == 0 && weaponInRightHandSlots[0] != null){
            rightWeapon = weaponInRightHandSlots[currentRightWeaponIndex];
            weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlots[currentRightWeaponIndex],false);
        }
        else if(currentRightWeaponIndex == 0 && weaponInRightHandSlots[0] == null){
            currentRightWeaponIndex += 1;
        }
        else if(currentRightWeaponIndex == 1 && weaponInRightHandSlots[1] != null){
            rightWeapon = weaponInRightHandSlots[currentRightWeaponIndex];
            weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlots[currentRightWeaponIndex],false);
        }
        else{
            currentRightWeaponIndex = currentRightWeaponIndex + 1;
        }
        if(currentRightWeaponIndex > weaponInRightHandSlots.Length -1){
            currentRightWeaponIndex = -1;
            rightWeapon = unarmedWeapon;
            weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon,false);
        }

    }
    public void ChangeLeftWeapon(){

        currentLeftWeaponIndex += 1;
        if(currentLeftWeaponIndex == 0 && weaponInLeftHandSlots[0] != null){
            leftWeapon = weaponInLeftHandSlots[currentLeftWeaponIndex];
            weaponSlotManager.LoadWeaponOnSlot(weaponInLeftHandSlots[currentLeftWeaponIndex],true);
        }
        else if(currentLeftWeaponIndex == 0 && weaponInLeftHandSlots[0] == null){
            currentLeftWeaponIndex += 1;
        }
        else if(currentLeftWeaponIndex == 1 && weaponInLeftHandSlots[1] != null){
            leftWeapon = weaponInLeftHandSlots[currentLeftWeaponIndex];
            weaponSlotManager.LoadWeaponOnSlot(weaponInLeftHandSlots[currentLeftWeaponIndex],true);
        }
        else{
            currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
        }
        if(currentLeftWeaponIndex > weaponInLeftHandSlots.Length -1){
            currentLeftWeaponIndex = -1;
            leftWeapon = unarmedWeapon;
            weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon,true);
        }
    }

}
