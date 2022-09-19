using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBossManager : MonoBehaviour
{
    //Change Attack model
    UIBossHealthBar  uIBossHealthBar;
    EnemyState enemyState;
    public string BossName;

    private void Awake() {
        uIBossHealthBar = FindObjectOfType<UIBossHealthBar>();
        enemyState = GetComponent<EnemyState>();
    }
    private void Start() {
        
        uIBossHealthBar.SetBossName(BossName);
        uIBossHealthBar.SetBossMaxHealth(enemyState.maxHealth);
    }
    public void updateBossHealthBar(int currentHealth){

        uIBossHealthBar.SetBossCurrentHealth(currentHealth);
    }
}
