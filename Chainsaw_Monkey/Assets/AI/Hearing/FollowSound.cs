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
        RaycastHit hit;
        Ray ray;
        Vector3 soundToEnemy;
        ray = new Ray(aIController.soundPosition, Vector3.down);
            soundToEnemy = (aIController.soundPosition - aIController.enemy.transform.position).normalized * 2;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
                
        aIController.agent.destination = new Vector3(hit.point.x, hit.point.y +0.1f, hit.point.z);

        aIController.agent.speed = aIController.runningSpeed;
        aIController.enemyAnimator.SetInteger("State", 1);
        aIController.heardSound = false;

        if(!aIController.headAudio.isPlaying){
            aIController.headAudio.Play();
        }
        
        return true;
    }
}
