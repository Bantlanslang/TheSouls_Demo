using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public EnemyAttackAction currentAttack;
    public CombatStanceState combatStanceState;
    public PursueTargetState pursueTargetState;
    public RotateTowardTargetState rotateTowardTargetState;

    public bool willDoComboOnNextAttack;
    public bool hasPerformAttack = true;   //has not attack

    public override State Tick(EnemyManager enemyManager, EnemyState enemyState, EnemyAnimatorManager enemyAnimatorManager)
    {
        //Select one of our many attacks based on attack scores
        //if the select attack is not able to be used because of bad angle or distance, select a new attack
        //if the attack is viable, stop our movement and attack our target
        //set our recovery timer to the attacks recovery time
        //return the combat stance state

        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        RotateTowardTargetWithAttack(enemyManager);

        if (distanceFromTarget > enemyManager.maximumAttackRange)
        {
            return pursueTargetState;
        }
        if (willDoComboOnNextAttack)
        {
            //Attack with Combo
            AttackTargetWithCombo(enemyAnimatorManager,enemyManager);
            return combatStanceState;
            //Set cool down time
        }
        if (!hasPerformAttack)
        {
            //Attack
            AttackTarget(enemyAnimatorManager,enemyManager);
            if(currentAttack.comboAction != null && currentAttack.canCombo){
                willDoComboOnNextAttack = true;
            }
            else{
                currentAttack = null;
                return combatStanceState;
            }
                
        }
        // if (willDoComboOnNextAttack && hasPerformAttack)
        // {
        //     Combo Attack
        //     return this;
        // }

        return rotateTowardTargetState;
    }

    private void AttackTarget(EnemyAnimatorManager enemyAnimatorManager , EnemyManager enemyManager){
        enemyAnimatorManager.EnemyAnimation(currentAttack.actionAnimation,true);
        enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
        if(!currentAttack.canCombo){
            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
        }
        hasPerformAttack = true;
    }
    
    //Attack Combo
    private void AttackTargetWithCombo(EnemyAnimatorManager enemyAnimatorManager,EnemyManager enemyManager){
        willDoComboOnNextAttack = false;
        currentAttack = currentAttack.comboAction;
        enemyAnimatorManager.EnemyAnimation(currentAttack.actionAnimation,true);
        enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
        currentAttack = null;
    }
    private void RotateTowardTargetWithAttack(EnemyManager enemyManager)
    {
        if (enemyManager.canRotate && enemyManager.isInteracting)
        {
            Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();
            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
    }

    //敵人攻擊時，攻擊轉向
    private void RollForComboChance(EnemyManager enemyManager){
        float comboChance = Random.Range(0,100);
        //TODO comboChance <= enemyManager.comboLikeHood
        if(enemyManager.allowAIToPerformCombo){
            if(currentAttack.comboAction != null){
                willDoComboOnNextAttack = true;
                currentAttack = currentAttack.comboAction;
            }
            else{
                willDoComboOnNextAttack = false;
                currentAttack = null;
            }
        }
    }

}
