using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public AnimatorController ac;
    public bool IsAI;
    
    //public BattleManager bm;
    private void Awake() {
        ac = GetComponent<AnimatorController>();
    }
    public void DoDamge(){
        ac.IssueTrigger("hit");
    }
    public void TheDeath(){
        ac.IssueTrigger("Ded");
    }


}
