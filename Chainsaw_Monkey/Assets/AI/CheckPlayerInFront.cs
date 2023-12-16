using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerInFront : BehaviourTreeNode
{
    public GameObject player;
    public GameObject enemy;

    public override bool process(){
        Vector3 enemyToPlayerDirection = player.transform.position - enemy.transform.position;
        float enemyToPlayerAngle = Vector3.Angle(enemyToPlayerDirection, enemy.transform.forward);

        if(enemyToPlayerAngle < 20.0f){
            return true;
        }
        else return false;

    }
}
