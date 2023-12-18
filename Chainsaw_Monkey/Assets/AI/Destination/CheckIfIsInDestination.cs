using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfIsInDestination : BehaviourTreeNode
{
    private AIController aIController;
    public CheckIfIsInDestination(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        if(Mathf.Abs(aIController.enemy.transform.position.x - aIController.agent.destination.x) <= 0.01f && Mathf.Abs(aIController.enemy.transform.position.y - aIController.agent.destination.y) <= 0.5f && Mathf.Abs(aIController.enemy.transform.position.z - aIController.agent.destination.z) <= 0.01f ){
            return true;
        }
        else return false;
    }
}
