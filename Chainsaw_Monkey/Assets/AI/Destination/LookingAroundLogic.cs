using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingAroundLogic : BehaviourTreeNode
{
    private AIController aIController;
    public LookingAroundLogic(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        if(aIController.lookingAroundTimer >= aIController.lookingAroundDuration/2){
            aIController.enemyAnimator.SetInteger("State", 4);
            if(aIController.lookingAroundTimer > aIController.lookingAroundDuration){
                aIController.lookedAround = true;
                aIController.lookingAround = false;
                aIController.lookingAroundTimer = 0;
            }
        }
        
        aIController.lookingAroundTimer += Time.deltaTime;
        return true;

    }
}
