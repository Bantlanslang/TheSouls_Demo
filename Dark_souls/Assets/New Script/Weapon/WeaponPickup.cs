using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Interactable
{
    public WeaponItem weaponItem;
    public override void Interact(PlayerManager playerManager)
    {
        base.Interact(playerManager);
        PickupItem(playerManager);
    }

    private void PickupItem(PlayerManager playerManager){

        PlayerInventory playerInventory;
        InteractableUI interactableUI;

        playerInventory = playerManager.GetComponent<PlayerInventory>();
        interactableUI = playerManager.GetComponent<InteractableUI>();
        
        playerManager.interactableUI.ItemText.text = weaponItem.itemname;
        playerManager.interactableUI.Itemimage.texture = weaponItem.itemIcon.texture;
        playerInventory.weaponInventory.Add(weaponItem);
        Destroy(gameObject);
    }

}
