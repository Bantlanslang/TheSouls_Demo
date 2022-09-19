using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBeginBossFightEvent : MonoBehaviour
{
    public PlayerState playerState;
    public EnemyState enemyState;

    public bool isPlayBackgroundAudio;

    private void Awake() {

        playerState = FindObjectOfType<PlayerState>();
        enemyState = FindObjectOfType<EnemyState>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            WorldEventManager.instance.ActiveBossFight();
            if(!isPlayBackgroundAudio){
                AudioManager.instance.Play("fight");
            }
            isPlayBackgroundAudio = true;
        }
    }

}
