using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSoundNearby : BehaviourTreeNode
{
    private AIController aIController;
    public CheckSoundNearby(AIController aIController){
        this.aIController = aIController;
    }

    public override bool process(){
        if(aIController.playerScript.isRunning  
        && Mathf.Abs(aIController.enemy.transform.position.x - aIController.player.transform.position.x) < 30
        && Mathf.Abs(aIController.enemy.transform.position.y - aIController.player.transform.position.y) < 3
        && Mathf.Abs(aIController.enemy.transform.position.x - aIController.player.transform.position.x) <30){
            aIController.soundPosition = aIController.player.transform.position;
            aIController.heardSound = true;
        }
        else if(!aIController.playerScript.isRunning  
        && Mathf.Abs(aIController.enemy.transform.position.x - aIController.player.transform.position.x) < 5
        && Mathf.Abs(aIController.enemy.transform.position.y - aIController.player.transform.position.y) < 3
        && Mathf.Abs(aIController.enemy.transform.position.x - aIController.player.transform.position.x) <5){
            aIController.soundPosition = aIController.player.transform.position;
            aIController.heardSound = true;
        }
        else if(aIController.gunController.shot){
            aIController.soundPosition = aIController.player.transform.position;
            aIController.heardSound = true;
        }
        return aIController.heardSound;
    }
}
