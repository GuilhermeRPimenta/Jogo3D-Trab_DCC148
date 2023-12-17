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

    public float attackRange = 1.5f;
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
    private BehaviourTreeNode attackTree;

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
        //CREATING NODES WICH REPEAT
        BehaviourTreeNode checkIfFrontAttacking = new CheckIfFrontAttacking(this);
        BehaviourTreeNode checkIfSpinAttacking = new CheckIfSpinAttacking(this);
        //END OF NODES WICH REPEAT


        //ATTACK TREE
        SequenceNode frontAttacking = new SequenceNode();
        frontAttacking.addChild(checkIfFrontAttacking);
        frontAttacking.addChild(new ContinueFrontAttack(this));

        SelectorNode frontAttackingOrStartFrontAttacking = new SelectorNode();
        frontAttackingOrStartFrontAttacking.addChild(frontAttacking);
        frontAttackingOrStartFrontAttacking.addChild(new StartFrontAttacking(this));

        SequenceNode checkIfShouldFrontAttack = new SequenceNode();
        checkIfShouldFrontAttack.addChild(new CheckPlayerInFrontOrIsFrontAttacking(this));
        checkIfShouldFrontAttack.addChild(frontAttackingOrStartFrontAttacking);

        SequenceNode spinAttacking = new SequenceNode();
        spinAttacking.addChild(checkIfSpinAttacking);
        spinAttacking.addChild(new ContinueSpinAttack(this));

        SelectorNode spinAttackingOrStartSpinAttacking = new SelectorNode();
        spinAttackingOrStartSpinAttacking.addChild(spinAttacking);
        spinAttackingOrStartSpinAttacking.addChild(new StartSpinAttacking(this));

        SelectorNode frontOrSpinAttack = new SelectorNode();
        frontOrSpinAttack.addChild(checkIfShouldFrontAttack);
        frontOrSpinAttack.addChild(spinAttackingOrStartSpinAttacking);

        SelectorNode checkIfShouldBeAttacking = new SelectorNode();
        checkIfShouldBeAttacking.addChild(new CheckIfPlayerIsInAttackRange(this));
        checkIfShouldBeAttacking.addChild(checkIfFrontAttacking);
        checkIfShouldBeAttacking.addChild(checkIfSpinAttacking);

        attackTree = new SequenceNode();
        attackTree.addChild(checkIfShouldBeAttacking);
        attackTree.addChild(frontOrSpinAttack);
        // END OF ATTACK TREE

        

    }

    // Update is called once per frame
    public void UpdateBehaviourTreeProcess()
    {
        attackTree.process();
    }
}
