using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfPlayerIsInAttackRange : BehaviourTreeNode
{
    private AIController aIController;
    public CheckIfPlayerIsInAttackRange(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        if(Mathf.Abs(aIController.player.transform.position.x - aIController.enemy.transform.position.x) <= aIController.attackRange && Mathf.Abs(aIController.player.transform.position.y - aIController.enemyHead.transform.position.y) <=1 && Mathf.Abs(aIController.player.transform.position.z - aIController.enemy.transform.position.z) <= aIController.attackRange){
            return true;
        }
        else return false;
    }
}
