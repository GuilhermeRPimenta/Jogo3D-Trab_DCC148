using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent agent;
    
    private Animator animator;
    private float attackTimer = 0f;
    private GameObject player;
    private AIController enemyAIController;
    private GameObject aIControllerHolder;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        //agent.destination = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        
        //enemyAIController = new AIController(gameObject, player,3);
        aIControllerHolder = GameObject.Find("AIControllerHolder");
        enemyAIController = aIControllerHolder.AddComponent<AIController>();
        enemyAIController.DeclareAIVariables();
        enemyAIController.BuildBehaviourTree();
        

    }

    // Update is called once per frame
    void Update()
    {
        
        /*if(Mathf.Abs(player.transform.position.x - transform.position.x) < 2 &&  Mathf.Abs(player.transform.position.z - transform.position.z) < 2){
            if(attackTimer <=0){
                animator.SetInteger("State", 1);
                //agent.destination = transform.position;
                agent.speed = 0;
                attackTimer = 2f;
            }
            else{
                attackTimer -= Time.deltaTime;
            }
            
        }
        else{
            if(attackTimer <= 0){
                animator.SetInteger("State", 0);
                agent.speed = 4;
                agent.destination = new Vector3(player.transform.position.x, player.transform.position.y -1.70f, player.transform.position.z);
            }
            else{
                attackTimer -= Time.deltaTime;
            }
            
        }*/
        
        
        if(Input.inputString != ""){
            int number;
            bool is_a_number = Int32.TryParse(Input.inputString, out number);
            if (is_a_number && number >= 0 && number < 10){
                animator.SetInteger("State", number);
                //Debug.Log(number);
                
            }
        }

        enemyAIController.UpdateBehaviourTreeProcess();

        

        
    }   
}
