using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scrakeScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.destination = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Mathf.Abs(player.transform.position.x - transform.position.x));
        if(Mathf.Abs(player.transform.position.x - transform.position.x) < 1){
            animator.SetInteger("State", 1);
        }
    }
}
