using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scrakeScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator;
    private float attackTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.destination = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Mathf.Abs(player.transform.position.x - transform.position.x) < 2 &&  Mathf.Abs(player.transform.position.z - transform.position.z) < 2){
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
            
        }
        

        
    }   
}
