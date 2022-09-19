using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvent : MonoBehaviour
{
    Animator anim;
    PlayerState playerState;

    private void Awake() {
        anim = GetComponent<Animator>();
        playerState = GetComponentInParent<PlayerState>();
    }
    public void ResetTrigger(string triggerName){
        anim.ResetTrigger(triggerName);
    }
    //無敵偵動畫
    public void EnableInvincible(){
        anim.SetBool("isInvulnerable",true);
    }
    public void DisableInvincible(){
        anim.SetBool("isInvulnerable",false);
    }
    public void isDiedPlayAudio(){
        anim.SetBool("isDiedPlayAudio",true);
        AudioManager.instance.Play("YouDied");
    }
}
