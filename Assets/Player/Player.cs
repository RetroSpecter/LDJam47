using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Controller2D))]
public class Player : MonoBehaviour {
    Controller2D controller;
    public static Player Instance;

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
    float velocityXSmoothing;
    Vector3 velocity;

    public float accelerationTimeAirborn = 0.2f;
    public float acclerationTimeGrounded = 0.1f;


    private SpriteAnimator animator;

    public Interactable objectOfConcentration;
    private const KeyCode interact = KeyCode.Z;

    public Transform headPosition;
    private Item heldItem;

    public GameObject concentratingUI;

    private void Awake()
    {
        Instance = this;
    }

    void Start() {
        controller = GetComponent<Controller2D>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJump = Mathf.Abs(gravity) * timeToJumpApex;
        animator = GetComponentInChildren<SpriteAnimator>();
        concentratingUI.SetActive(false);
    }

    public void TogglePlayerMovement(bool on) {
        canMove = on;
        velocity.x = 0;
    }

    void Update() {
        checkIfGrounded();
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        if (canMove)
        {
            jumpAndHover();
            movement(input);

            // Check interaction
            if (Input.GetKeyDown(interact))
            {
                if (this.objectOfConcentration != null)
                {
                    this.objectOfConcentration.Interact();
                }
                else if (heldItem != null) {
                    // Had to add customer logic
                    Item item;
                    if (this.heldItem.interactableID.Contains("Duck"))
                        item = this.RemoveItem(false);
                    else
                        item = this.RemoveItem();
                    item.transform.position = this.transform.position;
                }
            }
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

        if (Input.GetKey(KeyCode.Tab)) {
            targetSpeed *= 3;
        }

        float targetVelocityX = input.x * targetSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? acclerationTimeGrounded : accelerationTimeAirborn);
        animator.SpriteWalk(velocity.x);

    }

    void jumpAndHover()
    {
        if (Input.GetButtonDown("Jump") && canJump)
        {
            AudioManager.instance.Play("Jump", GetComponent<AudioSource>());
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


    // Interaction Management

    // Set another object as the one you want to interact with
    public void Concentrate(Interactable obj) {
        if (this.objectOfConcentration == null) {
            this.objectOfConcentration = obj;
            concentratingUI.SetActive(true);
        }
    }

    // Stop thinking about another object. think about waifus instead.
    public void StopConcentrating(Interactable obj) {
        if (this.objectOfConcentration != null
            && this.objectOfConcentration.interactableID == obj.interactableID) {
            this.objectOfConcentration = null;
            concentratingUI.SetActive(false);
        }

    }


    // Item Management

    // Pick up an item. If you already have an item, replace it
    public void PickUpItem(Item item, bool playSound = true) {
        if (this.heldItem != null) {
            // Remove item from head and place it on the ground. Also concentrate on it.
            this.Concentrate(this.heldItem);
            this.RemoveItem().transform.position = item.transform.position;
        }
        if (playSound)
            AudioManager.instance.Play("PickUp");
        this.heldItem = item;
        this.heldItem.transform.position = this.headPosition.position;
        this.heldItem.transform.SetParent(this.headPosition);

        // Stop concentrating on the item you just picked up
        this.StopConcentrating(item);
    }

    public static bool IsHolding(string itemID) {
        return Instance.GetHeldItemID() == itemID;
    }

    public string GetHeldItemID() {
        if (this.heldItem != null)
            return this.heldItem.interactableID;
        return "";
    }

    // removes the item from your head and returns it
    public Item RemoveItem(bool playSound = true) {
        this.heldItem.transform.SetParent(null);
        if (playSound)
            AudioManager.instance.Play("PutDown");
        var item = this.heldItem;
        this.heldItem = null;
        item.dropItem();
        return item;
    }
}
