using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    public CapsuleCollider Capcol;
    [SerializeField] private AnimatorController anim;

    [SerializeField]private float offect = 0.1f;
    private Vector3 point1;
    private Vector3 point2;
    private float radius;

    private void Awake() {

        radius = Capcol.radius - 0.05f;
    }

    void FixedUpdate() {

        point1 = transform.position + transform.up * (radius - offect);
        point2 = transform.position + transform.up * (Capcol.height-offect) - transform.up * radius;

        Collider[] outputCols = Physics.OverlapCapsule(point1,point2,radius,LayerMask.GetMask("Ground"));
        
        if(outputCols.Length != 0){
            
            anim.isGround();
        }
        else{
            anim.notGround();
        }
    }

}
