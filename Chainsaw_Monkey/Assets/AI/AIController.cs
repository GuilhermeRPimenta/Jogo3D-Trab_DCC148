using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    //AI VARIABLES
    //public EnemyScript enemyScript;
    //Generic
    public GameObject player;
    public GameObject enemy;
    public GameObject enemySpine;
    public Animator enemyAnimator;
    public NavMeshAgent agent;
    public LayerMask Ground;

    //Movement
    public float walkingSpeed = 2;
    public float runningSpeed = 4.0f;
    public bool running = false;

    //Attack
    public float attackRange = 1.5f;
    public float frontAttackTimer = 0;
    public float frontAttackDuration = 2;
    public float frontAttackSpeed = 2;
    public bool frontAttacking = false;
    public bool spinAttacking = false;
    public float spinAttackTimer = 0;
    public float spinAttackDuration = 0.5f;
    public float spinAttackSpeed = 2;
    
    //Stun
    public bool stunned = false;
    public float SP;
    public float firstStunPoints = 50;
    public float secondStunPoints = 150;
    public float stunTimer = 0;
    public float stunDuration = 1.5f;

    //Vision
    public int enemyLayer;
    public GameObject enemyHead;

    //Hearing
    public bool heardSound = false;
    public Vector3 soundPosition;


    


    //END OF AI SPECIFIC VARIABLES
    private BehaviourTreeNode hearingTree;

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
        SP = firstStunPoints;
        enemyLayer = enemy.layer;
        enemyHead = GameObject.Find("CHR_Head");
        Ground = LayerMask.GetMask("Ground");

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

        SequenceNode attackTree = new SequenceNode();
        attackTree.addChild(checkIfShouldBeAttacking);
        attackTree.addChild(frontOrSpinAttack);
        // END OF ATTACK TREE

        //STUN TREE
        SequenceNode stunTree = new SequenceNode();
        stunTree.addChild(new CheckIfShouldStun(this));
        stunTree.addChild(new StunLogic(this));
        //END OF STUN TREE

        //SIGHT TREE
        SequenceNode sightTree = new SequenceNode();
        sightTree.addChild(new CheckIfPlayerIsVisible(this));
        sightTree.addChild(new FollowPlayer(this));

        //HEARING TREE
        hearingTree = new SequenceNode();
        hearingTree.addChild(new CheckSoundNearby(this));
        hearingTree.addChild(new FollowSound(this));
        

    }

    // Update is called once per frame
    public void UpdateBehaviourTreeProcess()
    {
        hearingTree.process();
    }
}
