using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAnimation : MonoBehaviour
{
    EnemyManager enemyManager;
    private Animator anim;
    private void Awake() {
        anim = GetComponent<Animator>();
        enemyManager = GetComponentInParent<EnemyManager>();
    }
    private void OnAnimatorMove() {
        
        //SendMessageUpwards("OnUpdateRm",anim.deltaPosition);
        // float delta = Time.deltaTime;    
        // enemyManager.enemyRigid.drag = 0;
        // Vector3 deltaPosition = anim.deltaPosition;
        // deltaPosition.y = 0;
        // Vector3 velocity = deltaPosition/delta;
        // enemyManager.enemyRigid.velocity = velocity;

        if(anim.GetBool("isRotate")){
            Vector3 direction = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            //enemyManager.transform.LookAt(enemyManager.currentTarget.transform,Vector3.up);
            
            Quaternion rotation = Quaternion.LookRotation(direction,Vector3.up);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, rotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
    }
}
