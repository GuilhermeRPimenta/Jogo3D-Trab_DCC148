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
    [SerializeField] private float maxSpeed = 3;
    [SerializeField] private float groundDrag = 5f;
    private float playerHeight = 1.7f;
    [SerializeField] private LayerMask Ground;
    public bool grounded;
    public float staminaPoints = 10;
    public float defaultStamina = 10;
    public float recoverStaminaTimer = 0;
    public float minimumTimeToRecoverStamina = 5;

    //HP
    public float HP = 50;
    public float invicibilityTimer =0;
    public float invicibilityDuration = 2;

    //ENEMY AI
    public GameObject enemy;
    public AIController[] enemyAIController;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        baseOrientation = transform.localRotation; 
        cameraBaseOrientation = playerCamera.transform.localRotation;
        playerController = gameObject.GetComponent<CharacterController>();
        feet = GameObject.Find("Feet");

        
        Ground = LayerMask.GetMask("Ground");

        staminaPoints = defaultStamina;


        enemy = GameObject.Find("scrake_circus");
        enemyAIController = enemy.GetComponentsInChildren<AIController>();

        
    }

    // Update is called once per frame
    void Update()
    {
        

        UpdateRotation();
        GetInputs();
        Locomotion();
        //CheckVelocityLimit();
        invicibilityTimer += Time.deltaTime;

    }

    void FixedUpdate(){
        grounded = Physics.CheckSphere(feet.transform.position, 0.4f, Ground);
    }

    void GetInputs(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;
        if(horizontalInput != 0 && verticalInput != 0){
            horizontalInput = horizontalInput * 0.70710678f;
            verticalInput = verticalInput * 0.70710678f;
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
                staminaPoints -= Time.fixedDeltaTime;
                if(staminaPoints < 0) staminaPoints = 0;
                recoverStaminaTimer = 0;
            }
            else{
                maxSpeed = walkSpeed;
                if(recoverStaminaTimer < minimumTimeToRecoverStamina){
                    recoverStaminaTimer += Time.fixedDeltaTime;
                }
                
            }
        }
        else{
            maxSpeed = walkSpeed;
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
