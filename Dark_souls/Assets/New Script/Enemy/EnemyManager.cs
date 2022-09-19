using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public bool isPreforminAction;
    EnemyLocomotionManager enemyLocomotionManager;
    EnemyAnimatorManager enemyAnimatorManager;
    public EnemyState enemyState;

    public  CharacterStats currentTarget;
    public DummyIUserInput dummyIUserInput;
    public NavMeshAgent navMeshAgent;
    public State currentState;
    public Rigidbody enemyRigid;


    //public float distanceFromTarget;
    public float rotationSpeed = 25.0f;
    public float maximumAttackRange = 1.5f;

    //public float viewableAngle;


    [Header("AI Setting")]
    public float delectionRadius = 20;
    public float MaxDetectionAngle = 50;
    public float MinDetectionAngle = -50;
    public float currentRecoveryTime = 0;


    public bool canDoCombo;
    public bool canRotate;
    public bool allowAIToPerformCombo;
    public bool isInteracting;
    public Animator animator;


    private void Awake()
    {
        enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
        enemyState = GetComponent<EnemyState>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        dummyIUserInput = GetComponent<DummyIUserInput>();
        enemyRigid = GetComponent<Rigidbody>();
        navMeshAgent.enabled = false;


        animator = GetComponentInChildren<Animator>();

    }
    private void Start() {
        isPreforminAction = false;
    }

    private void Update()
    {
        HandleStateMachine();
        RecoveryTimer();

        isInteracting = animator.GetBool("EnemyInteracting");
        canDoCombo = animator.GetBool("CanDoCombo");
        canRotate = animator.GetBool("CanRotate");
    }

    private void LateUpdate() {
        navMeshAgent.transform.localPosition = Vector3.zero;
        navMeshAgent.transform.localRotation = Quaternion.identity;
    }

    private void HandleStateMachine()
    {
        if(!enemyState.isDeath){
            if(currentState != null){
                State nextState = currentState.Tick(this,enemyState,enemyAnimatorManager);
                if(nextState != null){
                    SwitchToNextState(nextState);
                }
            }
        }
        
    }
    private void SwitchToNextState(State state){
        currentState = state;
    }


    #region Attack

    // public void GetNewAttack()
    // {
    //     Vector3 targetDirection = enemyLocomotionManager.currentTarget.transform.position
    //     - this.transform.position;
    //     float ViewableAngle = Vector3.Angle(targetDirection, transform.forward);
    //     enemyLocomotionManager.distanceFromTarget = Vector3.Distance(enemyLocomotionManager.currentTarget.transform.position, transform.position);

    //     int maxScore = 0;

    //     for (int i = 0; i < enemyAttack.Length; i++)
    //     {
    //         EnemyAttackAction enemyAttackAction = enemyAttack[i];

            
    //         if (enemyLocomotionManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeedToAttack
    //         && enemyLocomotionManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeedToAttack)
    //         {
    //             print("ajdjaskdkadxcxicxcxcixcix");
    //             if (ViewableAngle <= enemyAttackAction.maximumAttackAngle
    //             && ViewableAngle >= enemyAttackAction.minimumAttackAngle)
    //             {
    //                 maxScore += enemyAttackAction.attackScore;
    //             }
    //         }
    //     }

    //     int randomValue = Random.Range(0,maxScore);
    //     int tempScore = 0;

    //     for(int i=0; i<enemyAttack.Length;i++){

    //         EnemyAttackAction enemyAttackAction = enemyAttack[i];

    //         if (enemyLocomotionManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeedToAttack
    //         && enemyLocomotionManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeedToAttack)
    //         {
    //             if (ViewableAngle <= enemyAttackAction.maximumAttackAngle
    //             && ViewableAngle >= enemyAttackAction.minimumAttackAngle)
    //             {
    //                 if(currentAttack != null)
    //                     return;

    //                 tempScore += enemyAttackAction.attackScore;

    //                 if(tempScore > randomValue)
    //                     currentAttack = enemyAttackAction;
    //             }
    //         }
    //     }


    // }

    #endregion

    private void AttackTarget(){

        // if(isPreforminAction){
        //     return;
        // }

        // if(currentAttack == null){
        //     GetNewAttack();
        // }
        // else{
        //     isPreforminAction = true;
        //     currentRecoveryTime = currentAttack.recoveryTime;
        //     enemyAnimatorManager.EnemyAttack(currentAttack);
        //     currentAttack = null;
        // }
    }
    

    private void RecoveryTimer(){
        if(currentRecoveryTime >0){
            currentRecoveryTime -= Time.deltaTime;
        }
        if(isPreforminAction){
            if(currentRecoveryTime <= 0){
                isPreforminAction = false;
            }
        }
    }

}
