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
        aIController.enemyAnimator.SetInteger("State", 7);
        aIController.agent.speed = 0;
        if(aIController.stunTimer >= aIController.stunDuration){
            aIController.stunTimer = 0;
            aIController.SP = aIController.secondStunPoints;
            aIController.enemyAnimator.SetInteger("State", 1);
            aIController.agent.speed = aIController.runningSpeed;
        }
        aIController.stunTimer += Time.deltaTime;
        return true;
    }
}
