using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotResurrecting : BehaviourTreeNode
{
    private AIController aIController;
    public NotResurrecting(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        return !aIController.resurrecting;
    }
}
