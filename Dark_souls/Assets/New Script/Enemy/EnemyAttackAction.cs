using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Enemy Action/Attack Action")]
public class EnemyAttackAction : EnemyAction
{
    public bool canCombo;
    public EnemyAttackAction comboAction;
    public GameObject bloodSwordEffect;

    public int attackScore = 3;
    public float recoveryTime = 2.0f;

    //可攻擊角度
    public float maximumAttackAngle = 40;
    public float minimumAttackAngle = -40;

    //可攻擊距離
    public float minimumDistanceNeedToAttack = 0;
    public float maximumDistanceNeedToAttack = 3;
    
}
