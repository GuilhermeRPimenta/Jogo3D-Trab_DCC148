using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : BehaviourTreeNode
{
    private AIController aIController;
    public FollowPlayer(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        aIController.agent.destination = new Vector3(aIController.player.transform.position.x, aIController.player.transform.position.y -1.7f, aIController.player.transform.position.z);
        aIController.running = true;
        return true;
    }
}
