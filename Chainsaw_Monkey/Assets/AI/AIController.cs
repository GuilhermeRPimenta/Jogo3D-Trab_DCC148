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
    public int groundLayer = 6;

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

    //Destination
    public bool lookedAround = false;
    public bool lookingAround = false;
    public float lookingAroundTimer = 0;
    public float lookingAroundDuration = 3;


    


    //END OF AI SPECIFIC VARIABLES
    private BehaviourTreeNode aliveActionSeletcor;

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

        //STUN TREE
        SequenceNode stunTree = new SequenceNode();
        stunTree.addChild(new CheckIfShouldStun(this));
        stunTree.addChild(new StunLogic(this));
        //END OF STUN TREE

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

        //SIGHT TREE
        SequenceNode sightTree = new SequenceNode();
        sightTree.addChild(new CheckIfPlayerIsVisible(this));
        sightTree.addChild(new FollowPlayer(this));
        //END OF SIGHT TREE

        //HEARING TREE
        SequenceNode hearingTree = new SequenceNode();
        hearingTree.addChild(new CheckSoundNearby(this));
        hearingTree.addChild(new FollowSound(this));
        // END OF HEARING TREE

        //DESTINATION TREE
        SequenceNode endOfLookingAround = new SequenceNode();
        endOfLookingAround.addChild(new CheckIfLookedAround(this));
        endOfLookingAround.addChild(new ChooseNewDestination(this));

        SequenceNode continueLookingAround = new SequenceNode();
        continueLookingAround.addChild(new CheckIfLookingAround(this));
        continueLookingAround.addChild(new LookingAroundLogic(this));

        SelectorNode lookingAroundSelector = new SelectorNode();
        lookingAroundSelector.addChild(endOfLookingAround);
        lookingAroundSelector.addChild(continueLookingAround);
        lookingAroundSelector.addChild(new StartLookingAround(this));


        SequenceNode destinationTree = new SequenceNode();
        destinationTree.addChild(new CheckIfIsInDestination(this));
        destinationTree.addChild(lookingAroundSelector);
        //END OF DESTINATION TREE

        //ALIVE TREE
        aliveActionSeletcor = new SelectorNode();
        aliveActionSeletcor.addChild(stunTree);
        aliveActionSeletcor.addChild(attackTree);
        aliveActionSeletcor.addChild(sightTree);
        aliveActionSeletcor.addChild(hearingTree);
        aliveActionSeletcor.addChild(destinationTree);

        //END OF ALIVE TREE
        

    }

    // Update is called once per frame
    public void UpdateBehaviourTreeProcess()
    {
        aliveActionSeletcor.process();
    }
}
