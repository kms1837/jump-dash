using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private Camera firstPersonCamera;

    [SerializeField]
    private Transform test;

    //ablity
    private float moveSpeed = 10.0f;
    private float dashPower = 20.0f;

    private Rigidbody playerRigidbody;

    private CharacterState currentState; // FSM

    private bool firstPerson = false;

    private Vector3 generatePos = new Vector3(0, 10, 0);

    private Vector3 dashForwad;

    private float cameraAngleY = 0;

    public IState CurrentState {
        get => currentState;
    }

    void Start() {
        playerRigidbody = this.GetComponent<Rigidbody>();
        currentState = new IdleState();
        currentState.init(this);
        cameraAngleY = Camera.main.transform.localRotation.eulerAngles.y;
    }

    void Update() {
        currentState.update();

        if (Input.GetKeyUp(KeyCode.Z)) {
            cameraSwap();
        }

        if (transform.position.y < -10) {
            this.transform.position = generatePos;
            playerRigidbody.velocity = Vector3.zero;
            this.setState(new IdleState());
        }
    }

    private void FixedUpdate() {
        currentState.fixedUpdate();
    }

    public void setState(CharacterState newState) {
        currentState.exit();

        currentState = newState;
        currentState.init(this);
        currentState.enter();
    }

    public void cameraRotation() {
        float y = firstPerson ? Input.GetAxis("Mouse Y") : 0;
        Transform cameraTransform = firstPersonCamera.transform;

        if(firstPerson) {
            float x = Input.GetAxis("Mouse X");
            playerRigidbody.rotation *= Quaternion.Euler(0.0f, x, 0.0f); 
        }
        else {
            Vector3 mousePosition = Input.mousePosition;
            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            float dx = screenCenter.x - mousePosition.x;
            float dy = screenCenter.y - mousePosition.y;
            float rotationY = Mathf.Atan2(-1 * dy, dx) * Mathf.Rad2Deg;

            playerRigidbody.rotation = Quaternion.Euler(0.0f, rotationY - cameraAngleY, 0.0f);
        }
        cameraTransform.localRotation = cameraTransform.localRotation * Quaternion.Euler(y * -1, 0.0f, 0.0f);
    }

    public void move() {
        float depth = Input.GetAxis("Vertical");
        float horizontal = firstPerson ? Input.GetAxis("Horizontal") : 0;
        float totalSpeed = moveSpeed;
        float moveDepth = depth * totalSpeed;

        playerRigidbody.velocity = transform.forward * moveDepth + new Vector3(horizontal * 10, playerRigidbody.velocity.y, 0);
    }

    public void jump() {
        playerRigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }

    public void dashStart() {
        dashForwad = transform.forward;
    }

    public void dash() {
        playerRigidbody.velocity = dashForwad * dashPower + new Vector3(0, playerRigidbody.velocity.y, 0);
    }

    private void cameraSwap() {
        firstPerson = !firstPerson;
        mainCamera.enabled = !firstPerson;
        firstPersonCamera.enabled = firstPerson;
        Cursor.lockState = firstPerson ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void hold(float colliderTop) {
        Vector3 currentPosition = this.transform.position;
        this.transform.position = new Vector3(currentPosition.x, colliderTop, currentPosition.z);
        playerRigidbody.useGravity = false;
        playerRigidbody.velocity = Vector3.zero;
    }

    public void unHold() {
        playerRigidbody.useGravity = true;
    }

    public void OnCollisionEnter(Collision other) {
        currentState.collisionEnter(other);

        Vector3 hitPoint = other.GetContact(0).point;
        Bounds hitObjectSize = other.collider.bounds;
        float colliderTop = hitObjectSize.max.y;

        float characterBottom = (this.transform.position.y - this.transform.localScale.y / 2);

        test.position = hitPoint;
        
        if (characterBottom > colliderTop) {
            this.setState(new IdleState());
            generatePos = other.transform.position + new Vector3(0, 10, 0);
        }
    }
}
