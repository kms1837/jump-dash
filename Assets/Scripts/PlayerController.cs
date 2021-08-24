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

    private IState currentState; // FSM

    private bool firstPerson = false;

    private Vector3 generatePos = new Vector3(0, 10, 0);

    private Vector3 dashForwad;

    public IState CurrentState {
        get => currentState;
    }

    void Start() {
        playerRigidbody = this.GetComponent<Rigidbody>();
        currentState = new IdleState(this);
    }

    void Update() {
        currentState.update();

        if (Input.GetKeyUp(KeyCode.Z)) {
            cameraSwap();
        }

        if (transform.position.y < -10) {
            this.transform.position = generatePos;
            playerRigidbody.velocity = Vector3.zero;
        }
    }

    private void FixedUpdate() {
        currentState.fixedUpdate();
    }

    public void setState(IState newState) {
        currentState.exit();

        currentState = newState;

        currentState.init();
    }

    public void cameraRotation(float x) {
        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0.0f, x, 0.0f);
    }

    public void move(float depth) {
        float totalSpeed = moveSpeed;
        float moveDepth = depth * totalSpeed;

        playerRigidbody.velocity = transform.forward * moveDepth + new Vector3(0, playerRigidbody.velocity.y, 0);
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
            this.setState(new IdleState(this));
            generatePos = other.transform.position + new Vector3(0, 10, 0);
        }
    }
}
