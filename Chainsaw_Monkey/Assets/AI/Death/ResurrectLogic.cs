using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectLogic : BehaviourTreeNode
{
    private AIController aIController;
    public ResurrectLogic(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        aIController.dead = false;
        aIController.resurrecting = false;
        aIController.resurrected = false;
        aIController.deathTimer = 0;
        aIController.resurrectTimer = 0;
        aIController.stunTimer = 0;
        aIController.frontAttackTimer = 0;
        aIController.spinAttackTimer = 0;
        aIController.lookingAroundTimer = 0;
        aIController.SP = aIController.firstStunPoints;
        aIController.HP = aIController.healthValue;
        aIController.heardSound = false;
        aIController.lookedAround = false;
        aIController.lookingAround = false;
        aIController.enemyAnimator.SetInteger("State", 0);
        aIController.agent.destination = aIController.enemy.transform.position;
        foreach(Collider x in aIController.enemyColliders){
            x.enabled = true;
        }
        
        return true;
    }
}
