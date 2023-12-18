using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunLogic : BehaviourTreeNode
{
    private AIController aIController;
    public StunLogic(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        aIController.stunned = true;
        aIController.enemyAnimator.SetInteger("State", 7);
        aIController.agent.speed = 0;
        if(aIController.stunTimer >= aIController.stunDuration){
            aIController.stunned = false;
            aIController.stunTimer = 0;
            aIController.SP = aIController.secondStunPoints;
        }
        aIController.stunTimer += Time.deltaTime;
        return true;
    }
}
