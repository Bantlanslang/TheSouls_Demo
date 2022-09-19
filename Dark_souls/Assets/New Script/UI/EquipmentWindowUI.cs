using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentWindowUI : MonoBehaviour
{
    public bool rightHandSlot01Select;
    public bool rightHandSlot02Select;
    public bool LeftHandSlot01Select;
    public bool LeftHandSlot02Select;

    HandEquipmentSlotUI[] handEquipmentSlotUIs;
    private void Start()
    {
        handEquipmentSlotUIs = GetComponentsInChildren<HandEquipmentSlotUI>();
    }
    public void LoadWeaponOnEquipmentwindow(PlayerInventory playerInventory)
    {
        for (int i = 0; i < handEquipmentSlotUIs.Length; i++)
        {
            if (handEquipmentSlotUIs[i].rightHandSlot01){
                handEquipmentSlotUIs[i].AddItem(playerInventory.weaponInRightHandSlots[0]);
            }
            else if (handEquipmentSlotUIs[i].rightHandSlot02){
                handEquipmentSlotUIs[i].AddItem(playerInventory.weaponInRightHandSlots[1]);
            }
            else if (handEquipmentSlotUIs[i].LeftHandSlot01){
                handEquipmentSlotUIs[i].AddItem(playerInventory.weaponInLeftHandSlots[0]);
            }
            else if (handEquipmentSlotUIs[i].LeftHandSlot02){
                handEquipmentSlotUIs[i].AddItem(playerInventory.weaponInLeftHandSlots[1]);
            }
        }
    }

    public void SelectrightHandSlot01()
    {

    }
    public void SelectrightHandSlot02()
    {

    }
    public void SelectLeftHandSlot01()
    {

    }
    public void SelectLeftHandSlot02()
    {

    }
}
