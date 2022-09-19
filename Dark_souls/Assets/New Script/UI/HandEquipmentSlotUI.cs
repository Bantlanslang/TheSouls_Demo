using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HandEquipmentSlotUI : MonoBehaviour
{
    public UIManager uIManager;
    public Image icon;
    WeaponItem weapon;

    public bool rightHandSlot01;
    public bool rightHandSlot02;
    public bool LeftHandSlot01;
    public bool LeftHandSlot02;

    public void AddItem(WeaponItem weaponItem){
        weapon = weaponItem;
        icon.sprite = weapon.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void ClearItem(){
        weapon = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }
    public void SelectThisSlot(){
        if(rightHandSlot01){
            uIManager.rightHandSlot01Select = true;
        }
        else if(rightHandSlot02){
            uIManager.rightHandSlot02Select = true;
        }
        else if(LeftHandSlot01){
            uIManager.leftHandSlot01Select = true;
        }
        else if(LeftHandSlot02){
            uIManager.leftHandSlot02Select = true;
        }
    }

}
