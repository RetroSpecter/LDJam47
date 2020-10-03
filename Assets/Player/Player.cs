using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Controller2D))]
public class Player : MonoBehaviour {
    Controller2D controller;

    [HideInInspector] public bool canJump;
    public float maxJumpHeight = 4;
    public float minJumpHeight = 2;
    float gravity = -20;
    public bool gravityOn = true;
    public float timeToJumpApex = 0.4f;
    float maxJump = 8;
    float minJump = 4;

    public bool canMove = true;
    public float moveSpeed = 6;
    public float holdingSpeed = 3;
    public float shootingSpeed = 6;
    float velocityXSmoothing;
    Vector3 velocity;

    public float accelerationTimeAirborn = 0.2f;
    public float acclerationTimeGrounded = 0.1f;

    void Start() {
        controller = GetComponent<Controller2D>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJump = Mathf.Abs(gravity) * timeToJumpApex;
    }

    void Update() {
        checkIfGrounded();
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        if (canMove)
        {
            jumpAndHover();
            movement(input);
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void checkIfGrounded() {
        if (controller.collisions.below) {
            velocity.y = Mathf.Max(0, velocity.y);
        }

        canJump = controller.collisions.below;

    }

    public void burstForce(Vector3 velocity, bool additive) {
        if (additive)
        {
            this.velocity += velocity;
        } else {
            this.velocity = velocity;
        }
    }

    void movement(Vector2 input) {

        float targetSpeed =  moveSpeed;
        targetSpeed = Input.GetButton("Fire2") ? shootingSpeed : targetSpeed;

        float targetVelocityX = input.x * targetSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? acclerationTimeGrounded : accelerationTimeAirborn);

    }

    void jumpAndHover()
    {
        if (Input.GetButtonDown("Jump") && canJump)
        {
            velocity.y = maxJump;
            canJump = false;
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > minJump)
            {
                velocity.y = minJump;
            }
        }
    }

    public bool grounded() {
        return canJump;
    }
}
