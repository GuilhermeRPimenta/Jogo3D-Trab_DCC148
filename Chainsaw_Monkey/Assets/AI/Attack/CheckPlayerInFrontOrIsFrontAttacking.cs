using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerInFrontOrIsFrontAttacking : BehaviourTreeNode
{
    private AIController aIController;
    public CheckPlayerInFrontOrIsFrontAttacking(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        Vector3 enemyToPlayerDirection = aIController.player.transform.position - aIController.enemySpine.transform.position;
        float enemyToPlayerAngle = Vector3.Angle(enemyToPlayerDirection, aIController.enemySpine.transform.forward);
        //Debug.Log(enemyToPlayerAngle);

        
        if(!aIController.spinAttacking){
            if(enemyToPlayerAngle < aIController.frontAttackAngle){
                return true;
            }
            else if(aIController.frontAttacking){
                return true;
            }
            else return false;
        }
        else return false;

    }
}
