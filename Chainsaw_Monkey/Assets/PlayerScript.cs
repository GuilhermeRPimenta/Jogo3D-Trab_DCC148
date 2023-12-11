using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Variaveis para rotação
    [SerializeField] private float eyeSpeed = 2f;
    private Quaternion baseOrientation;
    private float mouseH = 0;
    private float mouseV = 0;
    [SerializeField] private GameObject playerCamera;
    private Quaternion cameraBaseOrientation;

    //Variaveis para movimentação
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody rigidBody;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float groundDrag = 5f;
    private float playerHeight = 1.7f;
    [SerializeField] private LayerMask Ground;
    private bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        baseOrientation = transform.localRotation; 
        cameraBaseOrientation = playerCamera.transform.localRotation;

        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
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
        UpdatePosition();
    }

    void GetInputs(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        mouseH += Input.GetAxis("Mouse X");
        mouseV += Input.GetAxis("Mouse Y");
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

    void UpdatePosition(){
        
        if(grounded) {rigidBody.drag = groundDrag;}
        else rigidBody.drag = 0;

        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        rigidBody.AddForce(moveDirection * moveSpeed, ForceMode.Force);

        
    }

    void CheckVelocityLimit(){
        Vector3 velocityVector = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
        if(velocityVector.magnitude > maxSpeed){
            velocityVector = velocityVector.normalized * maxSpeed;
            rigidBody.velocity = new Vector3(velocityVector.x, rigidBody.velocity.y, velocityVector.z);
        }
    }
}
