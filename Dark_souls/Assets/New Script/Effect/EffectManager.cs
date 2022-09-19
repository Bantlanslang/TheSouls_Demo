using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{   
    public static EffectManager instance;
    public AttackState attackState;
    private Animator animator;
    public GameObject Effect01;
    public GameObject Effect02;
    public GameObject Effect03;
    public Transform Effectpivot;
    public Transform model;



    private void Awake() {
        if(instance != null){
            Destroy(instance);
        }
        instance = this;
    }

    private void OpenEffect01(){

        InstantiateEffect(Effect01);
        
    }
    private void OpenEffect02(){

        InstantiateEffect(Effect02);
        
    }
    private void OpenEffect03(){

        InstantiateEffect(Effect03);
        
    }
    private void InstantiateEffect(GameObject Effect){

        Quaternion modelRotation = model.transform.rotation;
        Vector3 EulerRotation = modelRotation.eulerAngles;
        Vector3 EffectRotation = Effect.transform.localEulerAngles;

        Vector3 EffectVector = EulerRotation + EffectRotation;

        Quaternion SwordEffecrtRotation = Quaternion.Euler
        (EffectVector.x,EffectVector.y,EffectVector.z);

        var effect = Instantiate
        (Effect,Effectpivot.position,SwordEffecrtRotation);
        Destroy(effect,0.5f);
    }
}
