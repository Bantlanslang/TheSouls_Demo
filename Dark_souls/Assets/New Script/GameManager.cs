using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public EnemyManager enemyManager;
    public UIManager uIManager;
    public float LoadingTimer = 0;
    private void Awake() {

        playerManager = FindObjectOfType<PlayerManager>();
        enemyManager = FindObjectOfType<EnemyManager>();
        uIManager = FindObjectOfType<UIManager>();
    }
    private void Start() {

        AudioManager.instance.Play("newArea");
    }

    private void Update() {

        YouDied();
        isOver();
    }

    public void YouDied(){
        if(playerManager.playerState.currentHealth <=0){
            if(playerManager.isDiedPlayAudio){
                uIManager.DiedUI.SetActive(true);
            }

            LoadingTimer += Time.deltaTime;
            if(LoadingTimer > 8.0f){
                uIManager.FadeInOutControl();
            }
        }
    }
    
    public void isOver(){
        if(enemyManager.enemyState.currentHealth <=0){
            //TODO you win this Demo is Over
            uIManager.DefeatedUI.SetActive(true);
            LoadingTimer += Time.deltaTime;
            if(LoadingTimer > 5.0f){
                uIManager.DefeatedUI.SetActive(false);
            }
        }
    }
}
