using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueTargetState : State
{
    public CombatStanceState combatStanceState;
    public RotateTowardTargetState rotateTowardTargetState;
    public override State Tick(EnemyManager enemyManager, EnemyState enemyState, EnemyAnimatorManager enemyAnimatorManager)
    {
        //Chase the target 追逐目標
        //if within attack range, switch to combat stance state 到一定距離，切換到combat stance state
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position,enemyManager.transform.position);
        float ViewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);
        
        RotateTowardTargetWithAttack(enemyManager);

        if(ViewableAngle > 65 || ViewableAngle < -65){
            return rotateTowardTargetState;
        }
        if(enemyManager.isPreforminAction){
            enemyManager.dummyIUserInput.UpdateDmagDvec(0,0);
            return this;
        }

        if(distanceFromTarget > enemyManager.maximumAttackRange){
            enemyManager.dummyIUserInput.UpdateDmagDvec(1.0f,0);
        }

        if(distanceFromTarget <= enemyManager.maximumAttackRange){
            enemyManager.dummyIUserInput.UpdateDmagDvec(0,0);
            return combatStanceState;
        }
        else{
            return this;
        }
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
        else{
            Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyManager.enemyRigid.velocity;
        
            enemyManager.navMeshAgent.enabled = true;
            enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.enemyRigid.velocity = targetVelocity;
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation,enemyManager.navMeshAgent.transform.rotation,enemyManager.rotationSpeed / Time.deltaTime);
        
        }
    }
}
