using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfNotResurrected : BehaviourTreeNode
{
    private AIController aIController;
    public CheckIfNotResurrected(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        return !aIController.resurrected;
    }
}
