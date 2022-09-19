using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public PursueTargetState pursueTargetState;
    public CharacterStats characterStats;
    public override State Tick(EnemyManager enemyManager, EnemyState enemyState, EnemyAnimatorManager enemyAnimatorManager)
    {
        //Look for a potential target 尋找目標
        //Switch to Purse Target State if target is found 切換到Purse Target State

        #region Handle Enemy target Detection

        //TODO Fix isDeath bug
        Collider[] cols = Physics.OverlapSphere(transform.position,enemyManager.delectionRadius,LayerMask.GetMask("Player"));
        //查找目標
        foreach (var col in cols)
        {
            if(!enemyState.isDeath){
                characterStats = col.transform.GetComponent<CharacterStats>();
            }

            if(characterStats != null){
                Vector3 targetDirection = characterStats.transform.position - transform.position;
                float ViewableAngle = Vector3.Angle(targetDirection, transform.forward);
                if(ViewableAngle > enemyManager.MinDetectionAngle && ViewableAngle < enemyManager.MaxDetectionAngle){
                    enemyManager.currentTarget = characterStats;
                }
            }
        }
        #endregion

        #region Handle Switch To Next State

        if(enemyManager.currentTarget != null){
            return pursueTargetState;
        }
        else{
            return this;
        }
        #endregion

    }
}
