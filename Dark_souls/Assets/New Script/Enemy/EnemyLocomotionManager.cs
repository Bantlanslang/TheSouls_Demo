using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocomotionManager : MonoBehaviour
{
    
    EnemyManager enemyManager;
    EnemyAnimatorManager enemyAnimatorManager;

    private void Awake() {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
    }

    private void Start() {
        //enemyRigid.isKinematic = false;
    }

    public void HandleDetection(){
        // Collider[] cols = Physics.OverlapSphere(transform.position,enemyManager.delectionRadius,LayerMask.GetMask("Player"));

        // //查找目標
        // foreach (var col in cols)
        // {
        //     CharacterStats characterStats = col.transform.GetComponent<CharacterStats>();
        //     if(characterStats != null){
        //         Vector3 targetDirection = characterStats.transform.position - transform.position;
        //         float ViewableAngle = Vector3.Angle(targetDirection, transform.forward);

        //         if(ViewableAngle > enemyManager.MinDetectionAngle && ViewableAngle < enemyManager.MaxDetectionAngle){
        //             currentTarget = characterStats;

        //         }
        //     }
        // }
    }

    public void HandleMoveToTarget(){
        
        // if(enemyManager.isPreforminAction)
        //     return;

        // Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
        // distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position,this.transform.position);
        // float ViewableAngle = Vector3.Angle(targetDirection, transform.forward);

        // if(enemyManager.isPreforminAction){
        //     dummyIUserInput.UpdateDmagDvec(0,0);
        //     navMeshAgent.enabled = false;
        // }
        // else{
        //     if(distanceFromTarget > stoppingDistance){
        //         dummyIUserInput.UpdateDmagDvec(1.0f,0);
        //     }
        //     else if(distanceFromTarget <= stoppingDistance){
        //         dummyIUserInput.UpdateDmagDvec(0,0);
        //     }
        // }
        // HandleRotateToTarget();

        // navMeshAgent.transform.localPosition = Vector3.zero;
        // navMeshAgent.transform.localRotation = Quaternion.identity;
    }

    public void HandleRotateToTarget(){

        
    }

}
