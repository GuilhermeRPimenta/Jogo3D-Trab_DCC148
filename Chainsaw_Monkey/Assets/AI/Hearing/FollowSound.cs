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
        Vector3 soundToEnemy;
        ray = new Ray(aIController.soundPosition, Vector3.down);
            soundToEnemy = (aIController.soundPosition - aIController.enemy.transform.position).normalized * 2;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))//{
                /*if(hit.collider.gameObject.layer == aIController.groundLayer){
                    validPosition = true;   
                }*/ 
            //}
        /*do{
            
            n++;
        }while(!validPosition && n< 50);*/
        /*if(validPosition){
        aIController.agent.destination = new Vector3(hit.point.x, hit.point.y +0.1f, hit.point.z);

        }*/
        aIController.agent.destination = new Vector3(hit.point.x, hit.point.y +0.1f, hit.point.z);

        aIController.agent.speed = aIController.runningSpeed;
        aIController.running = true;
        aIController.enemyAnimator.SetInteger("State", 1);
        aIController.heardSound = false;
        //Debug.Log(aIController.agent.destination);
        return true;
    }
}
