using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushState : State
{

    //埋伏、醒來狀態
    public bool isSleeping;
    public float delectionRadius = 2.0f;
    public PursueTargetState pursueTargetState;
    public string sleepAnimation;
    public string wakeupAnimation;

    public override State Tick(EnemyManager enemyManager, EnemyState enemyState, EnemyAnimatorManager enemyAnimatorManager)
    {
        if(isSleeping && enemyManager.isPreforminAction == false){
            enemyAnimatorManager.EnemyAnimation(sleepAnimation , true);
        }
        
        #region Handle target Detection
        Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position,delectionRadius,LayerMask.GetMask("Player"));

        foreach (var cols in colliders)
        {
            CharacterStats characterState = cols.transform.GetComponent<CharacterStats>();
            
            if(characterState != null){
                Vector3 targetDirection = characterState.transform.position - enemyManager.transform.position;
                float viewableAngle = Vector3.Angle(targetDirection,enemyManager.transform.forward);
                if(viewableAngle > enemyManager.MinDetectionAngle
                && viewableAngle < enemyManager.MaxDetectionAngle){
                    enemyManager.currentTarget = characterState;
                    isSleeping = false;
                    enemyAnimatorManager.EnemyAnimation(wakeupAnimation , true);
                }

            }
        }
        #endregion

        #region Handle State Change
        if(enemyManager.currentTarget != null){
            return pursueTargetState;
        }
        else{
            return this;
        }

        #endregion
    }
}
