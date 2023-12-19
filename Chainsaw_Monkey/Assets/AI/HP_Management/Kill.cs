using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : BehaviourTreeNode
{
    private AIController aIController;
    public Kill(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        aIController.dead = true;
        aIController.agent.speed = 0;
        aIController.enemyAnimator.SetInteger("State", 5);
        foreach(Collider x in aIController.enemyColliders){
            x.enabled = false;
        }
        aIController.chainsawMotorAudio.Stop();
        aIController.chainsawAttackSource.Stop();

        return true;
    }
}
