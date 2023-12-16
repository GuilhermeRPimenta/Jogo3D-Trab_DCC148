using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ContinueSpinAttack : BehaviourTreeNode
{
    public float spinAttackTimer;
    public float spinAttackDuration;
    public bool attacking;
    public NavMeshAgent agent;
    public float spinAttackSpeed;
    public float walkingSpeed;

    public override bool process()
    {
        agent.speed = spinAttackSpeed;
        spinAttackTimer -= Time.deltaTime;

        if(spinAttackTimer <=0){
            spinAttackTimer = spinAttackDuration;
            attacking = false;
            agent.speed = walkingSpeed;
        }
        

        return true;
    }
}
