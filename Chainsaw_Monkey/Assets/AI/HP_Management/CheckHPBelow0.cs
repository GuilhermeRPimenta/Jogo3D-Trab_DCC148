using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHPBelow0 : BehaviourTreeNode
{
    private AIController aIController;
    public CheckHPBelow0(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        if(aIController.HP <=0 && !aIController.dead){
            return true;
        }
        else{
            return false;
        }
    }
}
