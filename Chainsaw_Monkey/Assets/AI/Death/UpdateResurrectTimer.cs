using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateResurrectTimer : BehaviourTreeNode
{
    private AIController aIController;
    public UpdateResurrectTimer(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        aIController.enemyAnimator.SetInteger("State", 6);
        if(aIController.resurrectTimer >= aIController.resurrectDuration){
            aIController.resurrected = true;
        }
        aIController.resurrectTimer += Time.deltaTime;
        return true;
    }
}
