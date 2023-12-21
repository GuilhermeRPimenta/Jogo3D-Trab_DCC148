using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainsawScript : MonoBehaviour
{
    public AIController enemyAIController;
    public GameObject enemy;
    public PlayerScript playerScript;

    
    void OnTriggerEnter(Collider hit){
        if(hit.CompareTag("Player")){
            if (playerScript.invicibilityTimer < playerScript.invicibilityDuration) return;
            playerScript.gutsAudio.Play();
            if(enemyAIController.frontAttacking){
                playerScript.HP -= 15;
                playerScript.invicibilityTimer = 0;
            }
            else{
                playerScript.HP -= 10;
                playerScript.invicibilityTimer = 0;

            }
        }
    }
}
