using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfLookedAround : BehaviourTreeNode
{
    private AIController aIController;
    public CheckIfLookedAround(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        return aIController.lookedAround;
    }
}
