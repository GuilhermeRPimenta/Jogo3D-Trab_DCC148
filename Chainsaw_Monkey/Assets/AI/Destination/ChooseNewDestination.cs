using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseNewDestination : BehaviourTreeNode
{
    private AIController aIController;
    public ChooseNewDestination(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        bool validPosition = false;
        RaycastHit hit;
        int randFloor = Random.Range(0,2);
            int randY;
            if(randFloor == 0) randY = 1;
            else randY = 4;
        do{
            float randX = Random.Range(-12.5f,24f);
            
            float randZ = Random.Range(-36f,12f);
            Ray ray = new Ray(new Vector3(randX,randY,randZ), Vector3.down);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
                if(hit.collider.gameObject.layer == aIController.groundLayer){
                    validPosition = true;   
                }
            }
        } while(!validPosition);
        aIController.agent.destination = new Vector3(hit.point.x, hit.point.y +0.1f, hit.point.z);
        aIController.agent.speed = aIController.walkingSpeed;
        aIController.enemyAnimator.SetInteger("State", 0);

        

        aIController.lookingAround = false;
        aIController.lookedAround = false;
        aIController.lookingAroundTimer = 0;
        return true;

    }
}
