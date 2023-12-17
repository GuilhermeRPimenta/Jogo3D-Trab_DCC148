using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    //AI VARIABLES
    //public EnemyScript enemyScript;
    public GameObject player;
    public GameObject enemy;
    public GameObject enemySpine;
    public Animator enemyAnimator;

    public float frontAttackTimer = 0;
    public float frontAttackDuration = 2;
    public float frontAttackSpeed = 2;
    public bool frontAttacking = false;
    public bool spinAttacking = false;
    public float spinAttackTimer = 0;
    public float spinAttackDuration = 0.5f;
    public NavMeshAgent agent;
    public float spinAttackSpeed = 2;
    public float walkingSpeed = 2;
    public int number;


    


    //END OF AI SPECIFIC VARIABLES
    private BehaviourTreeNode frontOrSpinAttack;

    //CONSTRUCTOR
    /*public AIController(){
        this.player = GameObject.Find("Player");
        this.enemy = GameObject.Find("scrake_circus");
        this.agent = this.player.GetComponent<NavMeshAgent>();
        //this.number = number;
    }*/
    
    public void DeclareAIVariables(){
        player = GameObject.Find("Player");
        enemy = GameObject.Find("scrake_circus");
        agent = enemy.GetComponent<NavMeshAgent>();
        enemySpine = GameObject.Find("Spine");
        enemyAnimator = enemy.GetComponent<Animator>();
    }
    public void BuildBehaviourTree()
    {



        SequenceNode frontAttacking = new SequenceNode();
        frontAttacking.addChild(new CheckIfFrontAttacking(this));
        frontAttacking.addChild(new ContinueFrontAttack(this));

        SelectorNode frontAttackingOrStartFrontAttacking = new SelectorNode();
        frontAttackingOrStartFrontAttacking.addChild(frontAttacking);
        frontAttackingOrStartFrontAttacking.addChild(new StartFrontAttacking(this));

        SequenceNode checkIfShouldFrontAttack = new SequenceNode();
        checkIfShouldFrontAttack.addChild(new CheckPlayerInFrontOrIsFrontAttacking(this));
        checkIfShouldFrontAttack.addChild(frontAttackingOrStartFrontAttacking);

        SequenceNode spinAttacking = new SequenceNode();
        spinAttacking.addChild(new CheckIfSpinAttacking(this));
        spinAttacking.addChild(new ContinueSpinAttack(this));

        SelectorNode spinAttackingOrStartSpinAttacking = new SelectorNode();
        spinAttackingOrStartSpinAttacking.addChild(spinAttacking);
        spinAttackingOrStartSpinAttacking.addChild(new StartSpinAttacking(this));

        frontOrSpinAttack = new SelectorNode();
        frontOrSpinAttack.addChild(checkIfShouldFrontAttack);
        frontOrSpinAttack.addChild(spinAttackingOrStartSpinAttacking);

    }

    // Update is called once per frame
    public void UpdateBehaviourTreeProcess()
    {
        frontOrSpinAttack.process();
    }
}
