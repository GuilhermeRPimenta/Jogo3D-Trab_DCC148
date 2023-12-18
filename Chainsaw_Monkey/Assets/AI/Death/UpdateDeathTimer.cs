using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateDeathTimer : BehaviourTreeNode
{
    private AIController aIController;
    public UpdateDeathTimer(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        if(aIController.deathTimer >= aIController.deathDuration){
            aIController.resurrecting = true;
        }
        aIController.deathTimer += Time.deltaTime;
        return true;
    }
}
