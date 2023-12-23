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
        if(aIController.playerScript.verticalInput == 0 && aIController.playerScript.horizontalInput == 0 && !aIController.soundByGenerator){
            aIController.heardSound = false;
        }
        else if(aIController.playerScript.isRunning  
        && Mathf.Abs(aIController.enemy.transform.position.x - aIController.player.transform.position.x) < 5
        && Mathf.Abs(aIController.enemy.transform.position.y - aIController.player.transform.position.y) < 2f
        && Mathf.Abs(aIController.enemy.transform.position.x - aIController.player.transform.position.x) < 5){
            aIController.soundPosition = aIController.player.transform.position;
            aIController.heardSound = true;
        }
        else if(!aIController.playerScript.isRunning  
        && Mathf.Abs(aIController.enemy.transform.position.x - aIController.player.transform.position.x) < 2.5f
        && Mathf.Abs(aIController.enemy.transform.position.y - aIController.player.transform.position.y) < 2f
        && Mathf.Abs(aIController.enemy.transform.position.x - aIController.player.transform.position.x) < 2.5f){
            aIController.soundPosition = aIController.player.transform.position;
            aIController.heardSound = true;
        }
        else if(aIController.gunController.shot){
            aIController.soundPosition = aIController.player.transform.position;
            aIController.heardSound = true;
        }
        aIController.soundByGenerator = false;
        return aIController.heardSound;
    }
}
