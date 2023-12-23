using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyScript : MonoBehaviour
{
    private GameObject player;
    private Collider playerCollider;
    private AIController enemyAIController;
    private GameObject aIControllerHolder;
    private Collider[] enemyColliders;

    public float health = 50f;
    private bool isDead = false;
    public PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerCollider = player.GetComponent<Collider>();
        enemyColliders = GetComponentsInChildren<Collider>();

        aIControllerHolder = GameObject.Find("AIControllerHolder");
        enemyAIController = aIControllerHolder.GetComponent<AIController>();
        enemyAIController.DeclareAIVariables();
        enemyAIController.BuildBehaviourTree();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            enemyAIController.UpdateBehaviourTreeProcess();
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log(health);
        if(health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        //completar logica de quando morre
    }

    /*void OnTriggerEnter(Collider hit){
        Debug.Log("ENTRO");
        if(hit.CompareTag("Player")){
            if (playerScript.invicibilityTimer < playerScript.invicibilityDuration) return;
            playerScript.gutsAudio.Play();
            if(enemyAIController.frontAttacking){
                playerScript.hit = true;
                playerScript.HP -= 15;
                playerScript.invicibilityTimer = 0;
            }
            else{
                playerScript.hit = true;
                playerScript.HP -= 10;
                playerScript.invicibilityTimer = 0;

            }
        }
    }*/
}
