using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardTargetState : State
{
    public CombatStanceState combatStanceState;
    
    public override State Tick(EnemyManager enemyManager, EnemyState enemyState, EnemyAnimatorManager enemyAnimatorManager)
    {
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float viewableAngle = Vector3.SignedAngle(targetDirection,enemyManager.transform.forward,Vector3.up);

        if(enemyManager.isInteracting){
            return combatStanceState;
        }
        if(viewableAngle >= 100 && viewableAngle <= 180 && !enemyManager.isInteracting){
            //enemyAnimatorManager.RotateEnemyAnimation("Turn_Left180",true);
            enemyAnimatorManager.anim.SetBool("isRotate",true);
            return combatStanceState;
        }
        else if(viewableAngle <= -101 && viewableAngle >= -180 && !enemyManager.isInteracting){
            //enemyAnimatorManager.RotateEnemyAnimation("Turn_Left180",true);
            enemyAnimatorManager.anim.SetBool("isRotate",true);
            return combatStanceState;
        }
        else if(viewableAngle <= -45 && viewableAngle >= -100 && !enemyManager.isInteracting){
            //enemyAnimatorManager.RotateEnemyAnimation("Turn_Right90",true);
            enemyAnimatorManager.anim.SetBool("isRotate",true);
            return combatStanceState;
        }
        else if(viewableAngle >= 45 && viewableAngle <= 100 && !enemyManager.isInteracting){
            //enemyAnimatorManager.RotateEnemyAnimation("Turn_Left90",true);
            enemyAnimatorManager.anim.SetBool("isRotate",true);
            return combatStanceState;
        }
        return combatStanceState;
    }
}
