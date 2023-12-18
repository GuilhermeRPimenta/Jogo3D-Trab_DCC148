using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSoundNearby : BehaviourTreeNode
{
    private AIController aIController;
    public CheckSoundNearby(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        return aIController.heardSound;
    }
}
