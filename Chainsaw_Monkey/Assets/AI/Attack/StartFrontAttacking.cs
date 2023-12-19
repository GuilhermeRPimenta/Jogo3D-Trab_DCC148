using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFrontAttacking : BehaviourTreeNode
{
    private AIController aIController;
    public StartFrontAttacking(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        aIController.frontAttacking = true;
        aIController.chainsawAttackSource.Play();
        aIController.lookingAroundTimer = 0;
        aIController.lookingAround = false;
        return true;
    }
}
