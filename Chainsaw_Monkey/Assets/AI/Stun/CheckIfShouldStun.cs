using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfShouldStun : BehaviourTreeNode
{
    private AIController aIController;
    public CheckIfShouldStun(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        if(aIController.SP<=0){
            return true;
        }
        else return false;
    }
}
