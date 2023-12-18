using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfPlayerIsVisible : BehaviourTreeNode
{
    private AIController aIController;
    public CheckIfPlayerIsVisible(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        Vector3 playerToEnemyDirection = aIController.enemyHead.transform.position - aIController.player.transform.position;
        Ray ray = new Ray(aIController.player.transform.position, (playerToEnemyDirection).normalized);
        RaycastHit hit;
        Debug.DrawLine(aIController.enemyHead.transform.position, aIController.player.transform.position, Color.green);
        
        if (Physics.Raycast(ray, out hit, aIController.maxSightDistance)){
            if (hit.collider.gameObject.layer == aIController.enemyLayer){
                float headToPlayerAngle = Vector3.Angle(-aIController.enemyHead.transform.up, playerToEnemyDirection);
                //Debug.Log(headToPlayerAngle);
                if(headToPlayerAngle < 80){
                    return true;
                }
                else return false;
            }
            return false;
        }
        return false;
    }
}
