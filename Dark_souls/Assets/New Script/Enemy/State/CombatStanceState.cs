using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStanceState : State
{
    public AttackState attackState;
    public EnemyAttackAction[] enemyAttack;
    public PursueTargetState pursueTargetState;
    //float VerticalMovementValue = 0;
    //float HorizontalMovementValue = 0;

    bool randomDestinationSet = false;
    public override State Tick(EnemyManager enemyManager, EnemyState enemyState, EnemyAnimatorManager enemyAnimatorManager)
    {
        //Check for attack range
        //potentially circle player or walk around them
        //if in attack range Switch AttackState
        //if we are in a cool down after attacking, return this state and comtinue circling player
        //if the player runs out of range return the pursuetarget state
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
        float distanceFromTarget = Vector3.Distance
        (enemyManager.currentTarget.transform.position,enemyManager.transform.position);
        float ViewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

        attackState.hasPerformAttack = false;
        
        if(enemyManager.isInteracting){
            enemyManager.dummyIUserInput.UpdateDmagDvec(0,0);
            return this;
        }   

        if(distanceFromTarget > enemyManager.maximumAttackRange){
            return pursueTargetState;
        }

        if(!randomDestinationSet){
            randomDestinationSet = true;
            //Decide circling action
            //DecideCirclingAction(enemyAnimatorManager);
        }
        if(distanceFromTarget <= enemyManager.maximumAttackRange && (ViewableAngle >=100 && ViewableAngle <=180)){
            return pursueTargetState;
        }

        if(enemyManager.currentRecoveryTime <= 0 && attackState.currentAttack != null && distanceFromTarget <= enemyManager.maximumAttackRange){
            randomDestinationSet = false;
            return attackState;
        }
        else{
            GetNewAttack(enemyManager);
        }
        return this;
    }

    //Different ation
    private void DecideCirclingAction(EnemyAnimatorManager enemyAnimatorManager){
        
        //WalkAroundTarget(enemyAnimatorManager);
    }

    private void WalkAroundTarget(EnemyAnimatorManager enemyAnimatorManager){
        // VerticalMovementValue = 0.5f;

        // HorizontalMovementValue = Random.Range(-1,1);

        // if(HorizontalMovementValue <= 1 && HorizontalMovementValue >= 0){
        //     HorizontalMovementValue = 0.5f;
        // }
        // else if(HorizontalMovementValue >= -1 && HorizontalMovementValue < 0){
        //     HorizontalMovementValue = -0.5f;
        // }
    }

    public void GetNewAttack(EnemyManager enemyManager)
    {
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

        int maxScore = 0;

        for (int i = 0; i < enemyAttack.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttack[i];


            if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeedToAttack
            && distanceFromTarget >= enemyAttackAction.minimumDistanceNeedToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle

                && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    maxScore += enemyAttackAction.attackScore;

                }
            }
        }

        int randomValue = Random.Range(0, maxScore);
        int tempScore = 0;

        for (int i = 0; i < enemyAttack.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttack[i];

            if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeedToAttack
            && distanceFromTarget >= enemyAttackAction.minimumDistanceNeedToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    if (attackState.currentAttack != null)
                        return;

                    tempScore += enemyAttackAction.attackScore;

                    if (tempScore > randomValue)
                        attackState.currentAttack = enemyAttackAction;
                }
            }
        }
    }
}
