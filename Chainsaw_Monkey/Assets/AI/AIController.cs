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
    public float HP;

    //Movement
    public float walkingSpeed = 2;
    public float runningSpeed = 4.0f;

    //Attack
    public float attackRange = 1.0f;
    public float frontAttackAngle = 15;
    public float frontAttackTimer = 0;
    public float frontAttackDuration = 0.8f;
    public float frontAttackSpeed = 6;
    public bool frontAttacking = false;
    public bool spinAttacking = false;
    public float spinAttackTimer = 0;
    public float spinAttackDuration = 0.8f;
    public float spinAttackSpeed = 4.5f;
    public GameObject chainsawAttackAudioHolder;
    
    //Stun
    public float SP;
    public float firstStunPoints = 50;
    public float secondStunPoints = 150;
    public float stunTimer = 0;
    public float stunDuration = 1.5f;

    //Vision
    public int enemyLayer;
    public GameObject enemyHead;
    public float maxSightDistance = 50;

    //Hearing
    public bool heardSound = false;
    public Vector3 soundPosition;
    public PlayerScript playerScript;
    public GameObject gun;
    public GunController gunController;

    //Destination
    public bool lookedAround = false;
    public bool lookingAround = false;
    public float lookingAroundTimer = 0;
    public float lookingAroundDuration = 2.5f;

    //Dead
    public bool dead = false;
    public bool resurrecting = false;
    public bool resurrected = false;
    public float healthValue = 200;
    public float deathDuration = 10;
    public float deathTimer = 0;
    public float resurrectDuration = 3;
    public float resurrectTimer = 0;
    public Collider[] enemyColliders;
    //END OF AI VARIABLES
    public AudioSource chainsawAttackSource;
    public AudioSource headAudio;
    //AUDIO
    public GameObject chainsawMotor;
    public AudioSource chainsawMotorAudio;
    //END OF AUDIO VARIABLES
    private BehaviourTreeNode mainTree;

    
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
        HP = healthValue;
        enemyColliders = enemy.GetComponentsInChildren<Collider>();
        chainsawAttackAudioHolder = GameObject.Find("ChainsawAttackSoundHolder");
        chainsawAttackSource = chainsawAttackAudioHolder.GetComponent<AudioSource>();
        headAudio = enemyHead.GetComponent<AudioSource>();
        chainsawMotor = GameObject.Find("CHR_RArmPalm");
        chainsawMotorAudio = chainsawMotor.GetComponent<AudioSource>();
        playerScript = player.GetComponent<PlayerScript>();
        gun = GameObject.Find("P1911");
        gunController = gun.GetComponent<GunController>();

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
        SelectorNode aliveActionSeletcor = new SelectorNode();
        aliveActionSeletcor.addChild(stunTree);
        aliveActionSeletcor.addChild(attackTree);
        aliveActionSeletcor.addChild(sightTree);
        aliveActionSeletcor.addChild(hearingTree);
        aliveActionSeletcor.addChild(destinationTree);
        //END OF ALIVE TREE
        
        //DEATH TREE
        SequenceNode updatingResurrectTime = new SequenceNode();
        updatingResurrectTime.addChild(new CheckIfNotResurrected(this));
        updatingResurrectTime.addChild(new UpdateResurrectTimer(this));

        SequenceNode isResurrecting = new SequenceNode();
        isResurrecting.addChild(new CheckIfResurrected(this));
        isResurrecting.addChild(new ResurrectLogic(this));

        SelectorNode resurrectSelector = new SelectorNode();
        resurrectSelector.addChild(new NotResurrecting(this));
        resurrectSelector.addChild(updatingResurrectTime);
        resurrectSelector.addChild(isResurrecting);

        SequenceNode deathTree = new SequenceNode();
        deathTree.addChild(new CheckIfDead(this));
        deathTree.addChild(new UpdateDeathTimer(this));
        deathTree.addChild(resurrectSelector);
        //END OF DEATH TREE

        //HP MANAGEMENT TREE
        SequenceNode HPManagement = new SequenceNode();
        HPManagement.addChild(new CheckHPBelow0(this));
        HPManagement.addChild(new Kill(this));
        //END OF HP MANAGEMENT TREE

        //ALIVE MAIN TREE
        mainTree = new SelectorNode();
        mainTree.addChild(HPManagement);
        mainTree.addChild(deathTree);
        mainTree.addChild(aliveActionSeletcor);
        //END OF MAIN TREE

        

    }

    // Update is called once per frame
    public void UpdateBehaviourTreeProcess()
    {
        mainTree.process();
    }
}
