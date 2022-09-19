using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : AnimatorController
{
    EnemyManager enemyManager;
    private void OnEnable() {
        enemyManager = GetComponent<EnemyManager>();
    }
    // public void EnemyAttack(EnemyAction currentAttack){
    //     anim.Play(currentAttack.actionAnimation);
    // }
    public void EnemyAnimation(string animation , bool Interacting){
        anim.Play(animation);
        anim.SetBool("EnemyInteracting", Interacting);
        anim.SetBool("CanRotate",true);
    }
    public void RotateEnemyAnimation(string animation , bool Interacting){
        anim.Play(animation);
        anim.SetBool("EnemyInteracting", Interacting);
        anim.SetBool("isRotate",true);
        anim.SetBool("CanRotate",true);
    }
}
