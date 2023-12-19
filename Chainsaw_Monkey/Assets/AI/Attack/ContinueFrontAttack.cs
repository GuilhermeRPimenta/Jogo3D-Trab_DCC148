using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ContinueFrontAttack : BehaviourTreeNode
{
    private AIController aIController;
    public ContinueFrontAttack(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process()
    {
        aIController.enemyAnimator.SetInteger("State", 2);
        aIController.agent.speed = aIController.frontAttackSpeed;
        aIController.agent.destination = new Vector3(aIController.player.transform.position.x, aIController.player.transform.position.y -1.7f, aIController.player.transform.position.z);
        aIController.frontAttackTimer += Time.deltaTime;
        
        if(aIController.frontAttackTimer >=aIController.frontAttackDuration/2){
            aIController.agent.speed = 0;
            if(aIController.frontAttackTimer >=aIController.frontAttackDuration){
                aIController.frontAttackTimer = 0;
                aIController.frontAttacking = false;
                aIController.chainsawAttackSource.Stop();
                aIController.agent.speed = aIController.runningSpeed;
                aIController.enemyAnimator.SetInteger("State", 1);
            }
        }
        
        

        return true;
    }
}
