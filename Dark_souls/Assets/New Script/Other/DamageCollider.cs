using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    private Collider damageCollider;
    public EnemyWeaponSlotManager enemyWeaponSlotManager;
    public WeaponSlotManager weaponSlotManager;

    private void Awake() {
        damageCollider = GetComponent<Collider>();

        damageCollider.gameObject.SetActive(true);
        damageCollider.enabled = false;
        damageCollider.isTrigger = true;
    }
    private void Start() {
        enemyWeaponSlotManager = GetComponentInParent<EnemyWeaponSlotManager>();
        weaponSlotManager = GetComponentInParent<WeaponSlotManager>();
    }
    //TODO Efficacy improve
    private void OnTriggerEnter(Collider col) {
        if(col.CompareTag("Player")){
            PlayerState playerState = FindObjectOfType<PlayerState>(); 
            if(playerState != null){
                playerState.TakeDamage(enemyWeaponSlotManager.rightweaponItem.AttackValue);
            }
        }
        else if(col.CompareTag("Enemy")){
            EnemyState enemyState = FindObjectOfType<EnemyState>();
            if(enemyState != null){
                enemyState.TakeDamage(weaponSlotManager.weaponItem.AttackValue);
            }
            
        }
    }
    public void EnableDamageCollider(){
        damageCollider.enabled = true;
    }
    public void DisableDamageCollider(){
        damageCollider.enabled = false;
    }

}
