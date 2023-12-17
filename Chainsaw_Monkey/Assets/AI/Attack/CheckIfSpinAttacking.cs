using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfSpinAttacking : BehaviourTreeNode
{
    private AIController aIController;
    public CheckIfSpinAttacking(AIController aIController){
        this.aIController = aIController;
    }
    public override bool process()
    {
        return aIController.spinAttacking;
    }
}
