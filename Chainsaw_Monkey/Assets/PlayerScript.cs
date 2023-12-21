using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Rotation variables
    [SerializeField] private float eyeSpeed = 2f;
    private Quaternion baseOrientation;
    private float mouseH = 0;
    private float mouseV = 0;
    [SerializeField] private GameObject playerCamera;
    private Quaternion cameraBaseOrientation;

    //Movement variables
    public CharacterController playerController;
    public Vector3 velocity;
    public float gravity = 9.8f;
    public GameObject feet;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    [SerializeField] private float walkSpeed = 3;
    [SerializeField] private float runningSpeed = 6;
    public bool isRunning = false;
    [SerializeField] private float maxSpeed = 3;
    [SerializeField] private float groundDrag = 5f;
    private float playerHeight = 1.7f;
    [SerializeField] private LayerMask Ground;
    public bool grounded;
    public float staminaPoints = 10;
    public float defaultStamina = 10;
    public float recoverStaminaTimer = 0;
    public float minimumTimeToRecoverStamina = 5;
    public CharacterController characterController;

    //HP
    public float HP = 50;
    public float invicibilityTimer =0;
    public float invicibilityDuration = 0.5f;
    public bool hit = false;
    public bool dead = false;

    //ENEMY AI
    public GameObject enemy;
    public AIController[] enemyAIController;

    //AUDIO
    public GameObject guts;
    public AudioSource feetAudio;
    public AudioSource headAudio;
    public AudioSource gutsAudio;
    public bool playedDeathScream = false;
    public bool playingFeetAudio = false;
    public AudioClip walkingAudio;
    public AudioClip runningAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        baseOrientation = transform.localRotation; 
        cameraBaseOrientation = playerCamera.transform.localRotation;
        playerController = gameObject.GetComponent<CharacterController>();
        feet = GameObject.Find("Feet");
        characterController = gameObject.GetComponent<CharacterController>();

        
        Ground = LayerMask.GetMask("Ground");

        staminaPoints = defaultStamina;


        enemy = GameObject.Find("scrake_circus");
        enemyAIController = enemy.GetComponentsInChildren<AIController>();

        guts = GameObject.Find("Guts");
        gutsAudio = guts.GetComponent<AudioSource>();
        feetAudio = feet.GetComponent<AudioSource>();
        headAudio = playerCamera.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!dead){
            
            GetInputs();
            Locomotion();
        }
        UpdateRotation();
        //CheckVelocityLimit();
        invicibilityTimer += Time.deltaTime;
        if(invicibilityTimer >= invicibilityDuration && hit){
            hit = false;
        }
        if(HP <=0){
            dead = true;
            if(!playedDeathScream){
                headAudio.Play();
                playedDeathScream = true;
            }

            mouseV += 100 * Time.deltaTime;
            if(mouseV > 45) mouseV = 45;
            
            Vector3 camPos = playerCamera.transform.localPosition;
            if(camPos.y >= -1.3f){
                camPos.y =  camPos.y -1f * Time.deltaTime;
                playerCamera.transform.localPosition = new Vector3(camPos.x, camPos.y, camPos.z);
            }
        }

    }

    void FixedUpdate(){
        grounded = Physics.CheckSphere(feet.transform.position, 0.4f, Ground);
    }

    void GetInputs(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;

        if((horizontalInput != 0 || verticalInput !=0)){
            if(!isRunning){
                if(feetAudio.clip == runningAudio){
                    feetAudio.Stop();
                }
                feetAudio.clip = walkingAudio;
            }
            else{
                if(feetAudio.clip == walkingAudio){
                    feetAudio.Stop();
                }
                feetAudio.clip = runningAudio;
            }
            if(!feetAudio.isPlaying){
                feetAudio.Play();
            }
            
            playingFeetAudio = true;
            if(horizontalInput != 0 && verticalInput != 0){
            horizontalInput = horizontalInput * 0.70710678f;
            verticalInput = verticalInput * 0.70710678f;
            }
        }
        else if(horizontalInput == 0 && verticalInput ==0){
            feetAudio.Stop();
        }
        

        mouseH += Input.GetAxis("Mouse X");
        mouseV += Input.GetAxis("Mouse Y");
        if(mouseV < -45) mouseV = -45;
        else if(mouseV > 45) mouseV = 45;
    }
    void UpdateRotation(){
        Quaternion rotX, rotY;
        float angleY = mouseH * eyeSpeed;
        float angleX = mouseV * eyeSpeed;
        rotY = Quaternion.AngleAxis(angleY, Vector3.up);
        rotX = Quaternion.AngleAxis(angleX, Vector3.left);
        transform.localRotation = baseOrientation*rotY;
        playerCamera.transform.localRotation = cameraBaseOrientation * rotX;
    }

    void Locomotion(){
        
        
        if(grounded && velocity.y < 0){
            velocity.y = -2;
        }
        
        if (Input.GetKey(KeyCode.LeftShift)){
            if(staminaPoints >= 0.1f){
                maxSpeed = runningSpeed;
                isRunning = true;
                staminaPoints -= Time.fixedDeltaTime;
                if(staminaPoints < 0) staminaPoints = 0;
                recoverStaminaTimer = 0;
            }
            else{
                maxSpeed = walkSpeed;
                isRunning = false;
                if(recoverStaminaTimer < minimumTimeToRecoverStamina){
                    recoverStaminaTimer += Time.fixedDeltaTime;
                }
                
            }
        }
        else{
            maxSpeed = walkSpeed;
            isRunning = false;
            if(recoverStaminaTimer < minimumTimeToRecoverStamina){
                recoverStaminaTimer += Time.fixedDeltaTime;
            }
            
        }
        if(recoverStaminaTimer >= minimumTimeToRecoverStamina){
            if(staminaPoints < defaultStamina){
                staminaPoints += Time.fixedDeltaTime;
            }
            
        }
        playerController.Move(moveDirection * maxSpeed * Time.deltaTime);
        velocity.y -= gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);

    }


    

    /*void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        if(hit.gameObject.tag == "Chainsaw"){
            if(invicibilityTimer < invicibilityDuration) return;
            invicibilityTimer = 0;
            HP -= 10;
            /*if(enemyAIController[0].frontAttacking){
                HP -= 15;
            }
            else if(enemyAIController[0].spinAttacking){
                HP -= 10;
            }
        }
    }   */
}
