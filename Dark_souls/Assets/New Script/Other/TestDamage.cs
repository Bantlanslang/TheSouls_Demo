using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamage : MonoBehaviour
{
    public int damage = 10;
    public PlayerState playerstate;
    private void OnTriggerEnter(Collider other) {
        if(playerstate != null){
            if(other.CompareTag("Player")){
                playerstate.TakeDamage(damage);
            }
        }
    }
}
