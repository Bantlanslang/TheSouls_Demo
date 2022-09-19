using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public EquipmentWindowUI equipmentWindowUI;

    [Header("===== UI window =====")]
    public GameObject hudwindow;
    public GameObject selectWindow;
    public GameObject InventoryWindow;
    public GameObject EquipmentWindow;
    public GameObject FadeInOut;
    public GameObject DiedUI;
    public GameObject DefeatedUI;
    public FadeInOutUI fadeInOutUI;

    [Header("===== windowbool =====")]
    public bool IsOPenSelectWindow;
    public bool IsOpenInventoryWindow;
    public bool IsOpenEquipmentWindow;
    [Header("===== SelectWeapon =====")]

    public bool rightHandSlot01Select;
    public bool rightHandSlot02Select;
    public bool leftHandSlot01Select;
    public bool leftHandSlot02Select;

    [Header("===== weapon Inventory =====")]
    public GameObject weaponInventorySlotPrefab;
    public Transform weaponInventorySlotParent;
    WeaponInventorySlot[] weaponInventorySlots;

    private void Awake(){

        FadeInOut.SetActive(false);
    }

    private void Start() {
        //equipmentWindowUI = FindObjectOfType<EquipmentWindowUI>();
        weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
        fadeInOutUI = FadeInOut.GetComponent<FadeInOutUI>();
        equipmentWindowUI.LoadWeaponOnEquipmentwindow(playerInventory);
        EquipmentWindow.SetActive(false);
        UpdateUI();
    }
    public void UpdateUI()
    {
        for (int i = 0; i < weaponInventorySlots.Length; i++)
        {
            if (i < playerInventory.weaponInventory.Count)
            {
                if (weaponInventorySlots.Length < playerInventory.weaponInventory.Count)
                {
                    Instantiate(weaponInventorySlotPrefab, weaponInventorySlotParent);
                    weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
                }
                weaponInventorySlots[i].AddItem(playerInventory.weaponInventory[i]);
            }
            else
            {
                weaponInventorySlots[i].ClearInventorySlot();
            }
        }
    }

    public void OpenInventorywindow(){

        if(!IsOpenInventoryWindow){
            InventoryWindow.SetActive(true);
            IsOpenInventoryWindow = true;
        }
        else if(IsOpenInventoryWindow){
            InventoryWindow.SetActive(false);
            IsOpenInventoryWindow = false;
        }
    }
    public void OpenEquipmentwindow(){

        if(!IsOpenEquipmentWindow){
            EquipmentWindow.SetActive(true);
            IsOpenEquipmentWindow= true;
        }
        else if(IsOpenEquipmentWindow){
            EquipmentWindow.SetActive(false);
            IsOpenEquipmentWindow = false;
        }
    }

    public void OpenSelectWindow()
    {
        if (!IsOPenSelectWindow)
        {
            selectWindow.SetActive(true);
            IsOPenSelectWindow = true;
        }
        else if (IsOPenSelectWindow)
        {
            selectWindow.SetActive(false);
            IsOPenSelectWindow = false;
        }
    }
    public void ClearAllInventoryWindows(){
        ResetAllSelectSlot();
        InventoryWindow.SetActive(false);
        EquipmentWindow.SetActive(false);
    }
    public void ResetAllSelectSlot(){
        rightHandSlot01Select = false;
        rightHandSlot02Select = false;
        leftHandSlot01Select = false;
        leftHandSlot02Select = false;
    }

    public void FadeInOutControl(){

        FadeInOut.SetActive(true);
        if(!fadeInOutUI.Black){
            fadeInOutUI.DoFadeInOut(true);
            if(fadeInOutUI.FadeInOut.color.a > 0.95f){
                SceneManager.LoadScene("Loading");
                fadeInOutUI.Black = true;
            }
        }
        else if(fadeInOutUI.Black){
            fadeInOutUI.DoFadeInOut(false);
        }
    }
}
