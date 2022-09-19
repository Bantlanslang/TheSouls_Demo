using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : MonoBehaviour
{
    PlayerState playerState;
    WeaponSlotManager weaponSlotManager;
    public GameObject currentParticle;      //粒子效果
    public GameObject potionModel;    //回復瓶
    public int HeathAmount;


    private void Awake() {
        playerState = GetComponentInParent<PlayerState>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
    }
    public void HealPlayerEffect(){
        playerState.HealPlayer(HeathAmount);
        //粒子效果
        //GameObject healparticle = Instantiate(currentParticle,playerState.transform);
        Destroy(potionModel.gameObject);
        weaponSlotManager.LoadBothWeaponOnSlot();
    }

}
