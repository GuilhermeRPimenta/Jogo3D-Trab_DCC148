using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfLookingAround : BehaviourTreeNode
{
    private AIController aIController;
    public CheckIfLookingAround(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        return aIController.lookingAround;
    }
}
