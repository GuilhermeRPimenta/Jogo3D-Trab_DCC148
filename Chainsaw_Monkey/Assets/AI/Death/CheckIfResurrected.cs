using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfResurrected : BehaviourTreeNode
{
    private AIController aIController;
    public CheckIfResurrected(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        return aIController.resurrected;
    }
}
