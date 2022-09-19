using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEventManager : MonoBehaviour
{
    //fog Wall
    public List<FogDoor> fogDoors;
    public static WorldEventManager instance;
    public UIBossHealthBar uIBossHealthBar;
    public EnemyBossManager enemyBossManager;

    public bool bossFightIsActive;      //active Boss Fight
    public bool bossHasBeenAwaked;      //Has Been play animation
    public bool bossHasBeenDefeated;    //boss is ded

    private void Awake() {

        if(instance != null){
            Destroy(gameObject);
        }
        instance = this;
        
        uIBossHealthBar = FindObjectOfType<UIBossHealthBar>();
    }
    public void ActiveBossFight(){

        bossFightIsActive = true;
        bossHasBeenAwaked = true;
        uIBossHealthBar.SetUIHealthToActive();
        //Active fog wall
        
        foreach (var fogDoor in fogDoors)
        {
            fogDoor.ActiveFogWall();
        }
    }
    public void BossDefeated(){

        bossHasBeenDefeated = true;
        bossFightIsActive = false;
        bossHasBeenAwaked = false;

        foreach (var fogDoor in fogDoors)
        {
            fogDoor.DeactivateFogWall();
        }
    }
}
