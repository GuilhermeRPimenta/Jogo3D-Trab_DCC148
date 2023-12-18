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
        bool validPosition = false;
        RaycastHit hit;
        int n =0;
        Ray ray;
        do{
            ray = new Ray(aIController.soundPosition, Vector3.down);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
                if(hit.collider.gameObject.layer == aIController.groundLayer){
                    validPosition = true;   
                }

                else {
                    aIController.soundPosition.x += 0.5f;
                    aIController.soundPosition.z += 0.5f;
                }   
            }
            n++;
        }while(!validPosition && n< 40);
        aIController.agent.destination = new Vector3(hit.point.x, hit.point.y +0.1f, hit.point.z);
        aIController.agent.speed = aIController.runningSpeed;
        aIController.running = true;
        aIController.enemyAnimator.SetInteger("State", 1);
        aIController.heardSound = false;
        Debug.Log(aIController.agent.destination);
        return true;
    }
}
