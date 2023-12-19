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
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody rigidBody;
    [SerializeField] private float walkSpeed = 20f;
    [SerializeField] private float runningSpeed = 40f;
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float groundDrag = 5f;
    private float playerHeight = 1.7f;
    [SerializeField] private LayerMask Ground;
    private bool grounded;
    public float staminaPoints;
    public float defaultStamina = 10;
    public float recoverStaminaTimer = 0;
    public float minimumTimeToRecoverStamina = 5;

    //HP
    public float HP = 50;

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

        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
        Ground = LayerMask.GetMask("Ground");

        staminaPoints = defaultStamina;


        enemy = GameObject.Find("scrake_circus");
        enemyAIController = enemy.GetComponentsInChildren<AIController>();

        
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight + 0.1f, Ground);
        
    
        UpdateRotation();
        GetInputs();
        CheckVelocityLimit();

    }

    void FixedUpdate(){
        Locomotion();
    }

    void GetInputs(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

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
        if(grounded) {rigidBody.drag = groundDrag;}
        else rigidBody.drag = 0;
        if (Input.GetKey(KeyCode.LeftShift)){
            if(staminaPoints >= 0.1f){
                maxSpeed = runningSpeed;
                Running();
                staminaPoints -= Time.fixedDeltaTime;
                if(staminaPoints < 0) staminaPoints = 0;
                recoverStaminaTimer = 0;
            }
            else{
                maxSpeed = walkSpeed;
                Walking();
                if(recoverStaminaTimer < minimumTimeToRecoverStamina){
                    recoverStaminaTimer += Time.fixedDeltaTime;
                }
                
            }
        }
        else{
            maxSpeed = walkSpeed;
            Walking();
            if(recoverStaminaTimer < minimumTimeToRecoverStamina){
                recoverStaminaTimer += Time.fixedDeltaTime;
            }
            
        }
        if(recoverStaminaTimer >= minimumTimeToRecoverStamina){
            if(staminaPoints < defaultStamina){
                staminaPoints += Time.fixedDeltaTime;
            }
            
        }

    }
    void Walking(){
        
        /*if(grounded) {rigidBody.drag = groundDrag;}
        else rigidBody.drag = 0;*/

        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        rigidBody.AddForce(moveDirection * walkSpeed, ForceMode.Force);

        
    }

    void Running(){
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        rigidBody.AddForce(moveDirection * runningSpeed, ForceMode.Force);
        
    }

    void CheckVelocityLimit(){
        Vector3 velocityVector = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
        if(velocityVector.magnitude > maxSpeed){
            velocityVector = velocityVector.normalized * maxSpeed;
            rigidBody.velocity = new Vector3(velocityVector.x, rigidBody.velocity.y, velocityVector.z);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Chainsaw")){
            if(enemyAIController[0].frontAttacking){
                HP -= 15;
            }
            else if(enemyAIController[0].spinAttacking){
                HP -= 10;
            }
        }
    }   
}
