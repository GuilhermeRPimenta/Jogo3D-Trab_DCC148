using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLookingAround : BehaviourTreeNode
{
    private AIController aIController;
    public StartLookingAround(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        aIController.agent.speed = 0;
        aIController.lookingAround = true;
        aIController.enemyAnimator.SetInteger("State", 3);
        return true;
    }
}
