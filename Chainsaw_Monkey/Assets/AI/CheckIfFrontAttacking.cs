using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfFrontAttacking : BehaviourTreeNode
{
    private AIController aIController;
    public CheckIfFrontAttacking(AIController aIController){
        this.aIController = aIController;
    }
    public override bool process()
    {
        return aIController.frontAttacking;
    }
}
