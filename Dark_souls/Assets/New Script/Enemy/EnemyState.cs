using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : CharacterStats
{
    private ActorManager actorManager;
    private EnemyBossManager enemyBossManager;

    [Header("===== Enemy Souls =====")]
    public int SoulsAwardOnDeath;

    private void Awake()
    {
        actorManager = GetComponent<ActorManager>();
        enemyBossManager = GetComponent<EnemyBossManager>();
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
    }
    
    public int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 60;
        return maxHealth;
    }
    public void TakeDamage(int Damage)
    {
        if (isDeath)
        {
            return;
        }
        currentHealth = currentHealth - Damage;
        enemyBossManager.updateBossHealthBar(currentHealth);
        //TODO make a toughness value to weapon , player and Enemy 
        //actorManager.DoDamge();

        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }
    private void HandleDeath(){
        currentHealth = 0;
        actorManager.TheDeath();
        isDeath = true;
        //scan for every player in the scene award them souls   給與場景內的所有玩家靈魂
        PlayerState playerState = FindObjectOfType<PlayerState>();
        SoulsCountBar soulsCountBar = FindObjectOfType<SoulsCountBar>();

        if(playerState != null){
            playerState.AddSouls(SoulsAwardOnDeath);
            
            if(soulsCountBar != null){
                soulsCountBar.SetSoulsCount(playerState.SoulsCount);
            }
        }
    }

}
