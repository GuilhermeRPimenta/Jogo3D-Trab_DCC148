using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpinAttacking : BehaviourTreeNode
{
    private AIController aIController;
    public StartSpinAttacking(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        aIController.spinAttacking = true;
        aIController.chainsawAttackSource.Play();
        aIController.lookingAroundTimer = 0;
        return true;
    }
}
