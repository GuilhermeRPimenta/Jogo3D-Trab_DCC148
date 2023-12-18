using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSound : BehaviourTreeNode
{
    private AIController aIController;
    public FollowSound(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        Ray ray = new Ray(aIController.soundPosition, Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity, aIController.Ground);
        aIController.agent.destination = new Vector3(hit.point.x, hit.point.y +0.1f, hit.point.z);
        aIController.agent.speed = aIController.runningSpeed;
        aIController.running = true;
        aIController.enemyAnimator.SetInteger("State", 1);
        return true;
    }
}
