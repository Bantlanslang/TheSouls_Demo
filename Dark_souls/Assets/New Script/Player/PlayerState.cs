using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : CharacterStats
{
    public ActorManager actorManager;
    
    [Header("===== Bar =====")]

    public HealthBar healthBar;
    public StaminaBar staminaBar;
    PlayerManager playerManager;
    public float StaminaRecoverAmount = 30.0f;
    public float StaminaRecoverTime = 0;

    [Header("===== Souls Count =====")]
    public int SoulsCount;


    private void Awake() {
        actorManager = GetComponent<ActorManager>();
        healthBar = FindObjectOfType<HealthBar>();
        staminaBar = FindObjectOfType<StaminaBar>();
        playerManager = GetComponent<PlayerManager>();
    }
    private void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        maxStamina = SetMaxStaminaFromHealthLevel();
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);

    }

    public int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }
    public int SetMaxStaminaFromHealthLevel()
    {
        maxStamina = StaminaLevel * 10;
        return maxStamina;
    }

    public void TakeDamage(int Damage)
    {
        if(playerManager.isInvulnerable){
            return;
        }
        if(isDeath){
            return;
        }
        currentHealth = currentHealth - Damage;
        healthBar.SetCurrentHealth(currentHealth);
        actorManager.DoDamge();

        if(currentHealth <=0){
            currentHealth = 0;
            actorManager.TheDeath();
            isDeath = true;
        }
    }

    public void TakeStamina(float Stamina){
        currentStamina = currentStamina - Stamina;
        if(currentStamina < 0){
            currentStamina = 0;
        }
        staminaBar.SetCurrentStamina(currentStamina);
    }

    public void RecoverStamina(){
        if(playerManager.isInteracting){
            StaminaRecoverTime = 0;
        }
        else{
            StaminaRecoverTime += Time.deltaTime;
            if(currentStamina < maxStamina && StaminaRecoverTime >1f){
                currentStamina += StaminaRecoverAmount * Time.deltaTime;
                staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
            }
        }
        
    }

    public void AddSouls(int souls){
        SoulsCount = SoulsCount += souls;
    }
    
    public void HealPlayer(int healAmount){
        
        if(isDeath){
            return;
        }
        currentHealth = currentHealth + healAmount;
        healthBar.SetCurrentHealth(currentHealth);
    }

}
