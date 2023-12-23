using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfDead : BehaviourTreeNode
{
    private AIController aIController;
    public CheckIfDead(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        return aIController.dead;
    }
}
