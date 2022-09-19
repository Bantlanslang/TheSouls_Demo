using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventorySlot : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public WeaponSlotManager weaponSlotManager;
    public UIManager uIManager;
    public Image icon;
    WeaponItem Item;

    public void AddItem(WeaponItem weaponItem)
    {
        Item = weaponItem;
        icon.sprite = weaponItem.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }
    public void ClearInventorySlot()
    {
        Item = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }
    public void EquipThisItem(){

        // if(uIManager.rightHandSlot01Select){
        //     playerInventory.weaponInventory.Add(playerInventory.weaponInRightHandSlots[0]);
        //     playerInventory.weaponInRightHandSlots[0] = Item;
        //     playerInventory.weaponInventory.Remove(Item);
        // }
        // else if(uIManager.rightHandSlot02Select){
        //     playerInventory.weaponInventory.Add(playerInventory.weaponInRightHandSlots[1]);
        //     playerInventory.weaponInRightHandSlots[1] = Item;
        //     playerInventory.weaponInventory.Remove(Item);
        // }
        // else if(uIManager.leftHandSlot01Select){
        //     playerInventory.weaponInventory.Add(playerInventory.weaponInLeftHandSlots[0]);
        //     playerInventory.weaponInLeftHandSlots[0] = Item;
        //     playerInventory.weaponInventory.Remove(Item);
        // }
        // else if(uIManager.leftHandSlot02Select){
        //     playerInventory.weaponInventory.Add(playerInventory.weaponInLeftHandSlots[1]);
        //     playerInventory.weaponInLeftHandSlots[1] = Item;
        //     playerInventory.weaponInventory.Remove(Item);
        // }
        // else{
        //     return;
        // }
        // playerInventory.rightWeapon = playerInventory.weaponInRightHandSlots[playerInventory.currentRightWeaponIndex];
        // playerInventory.leftWeapon = playerInventory.weaponInLeftHandSlots[playerInventory.currentLeftWeaponIndex];
        
        // weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon,false);
        // weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon,true);

        // uIManager.equipmentWindowUI.LoadWeaponOnEquipmentwindow(playerInventory);
        // uIManager.ResetAllSelectSlot();
    }
    
}
