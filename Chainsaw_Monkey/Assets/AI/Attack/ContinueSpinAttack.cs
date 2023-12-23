using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ContinueSpinAttack : BehaviourTreeNode
{
    private AIController aIController;
    public ContinueSpinAttack(AIController aIController){
        this.aIController = aIController;
    }

    
    public override bool process()
    {
        aIController.enemyAnimator.SetInteger("State", 8);
        aIController.agent.speed = 1;
        aIController.agent.destination = new Vector3(aIController.player.transform.position.x, aIController.player.transform.position.y -1.7f, aIController.player.transform.position.z);
        aIController.spinAttackTimer += Time.deltaTime;

        if(aIController.spinAttackTimer >=aIController.spinAttackDuration){
            aIController.spinAttackTimer = 0;
            aIController.spinAttacking = false;
            aIController.chainsawAttackSource.Stop();
            aIController.agent.speed = aIController.runningSpeed;
            aIController.enemyAnimator.SetInteger("State", 1);
        }
        

        return true;
    }
}
