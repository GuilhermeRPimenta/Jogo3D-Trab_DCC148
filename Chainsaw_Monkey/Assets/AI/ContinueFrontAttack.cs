using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ContinueFrontAttack : BehaviourTreeNode
{
    public float frontAttackTimer;
    public float frontAttackDuration;
    public bool attacking;
    public NavMeshAgent agent;
    public float frontAttackSpeed;
    public float walkingSpeed;

    public override bool process()
    {
        agent.speed = frontAttackSpeed;
        frontAttackTimer -= Time.deltaTime;

        if(frontAttackTimer <=0){
            frontAttackTimer = frontAttackDuration;
            attacking = false;
            agent.speed = walkingSpeed;
        }
        

        return true;
    }
}
