using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float movementDirection;

    [Header("Character Attributes:")]
    public float MOVEMENT_SPEED;
    public float JUMP_SPEED;
    [Space]

    [Header("References:")]
    public Rigidbody2D rb;
    public InputManager inputManager = null;
    public LayerMask groundLayer;
    public Collider2D legs;
    public Animator animator;


    [Tooltip("The sprite renderer that represents the player.")]
    public SpriteRenderer spriteRenderer = null;


    bool canJump;
    // Whether the player is in the middle of a jump right now
    public bool jumping = false;

    //if the player is touching the ground
    public bool grounded = false;
    #region Player State Variables

    // Enum used for categorizing the player's state
    public enum PlayerState
    {
        Idle,
        Run,
        Jump, 
        Fall,
        Dead //Might delete later
    }

    // The player's current state (walking, idle, jumping, or falling)
    public PlayerState state = PlayerState.Idle;
    #endregion


    #region Input from Input Manager
    // The horizontal movement input collected from the input manager
    public float horizontalMovementInput
    {
        get
        {
            if (inputManager != null)
                return inputManager.horizontalMovement;
            else
                return 0;
        }
    }
    #endregion
    public enum PlayerDirection
    {
        Right,
        Left
    }

    // Which way the player is facing right now
    public PlayerDirection facing
    {
        get
        {
            if (horizontalMovementInput > 0)
            {
                return PlayerDirection.Right;
            }
            else if (horizontalMovementInput < 0)
            {
                return PlayerDirection.Left;
            }
            else
            {
                if (spriteRenderer != null && spriteRenderer.flipX == true)
                    return PlayerDirection.Left;
                return PlayerDirection.Right;
            }
        }
    }
    private void Awake()
    {
        if (inputManager == null)
        {
            inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        HandleSpriteDirection();
        DetermineState();
        if (Input.GetKeyDown(KeyCode.Space) & grounded)
        {
            Jump();
        }
    }
    void FixedUpdate()
    {
        if (legs.IsTouchingLayers(groundLayer))
        {
            grounded = true;
            jumping = false;
        }
        else
        {
            grounded = false;
            jumping = true;
        }
    }

    void Move()
    {
        movementDirection = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movementDirection * MOVEMENT_SPEED, rb.velocity.y);
        SetState(PlayerState.Run);
        if (grounded && !this.transform.hasChanged) animator.SetBool("isIdle", true);// Turn on idle animation

    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, JUMP_SPEED * (rb.gravityScale / 2) ), ForceMode2D.Impulse);
        jumping = true;
        grounded = false;
    }
    private void HandleSpriteDirection()
    {
        if (spriteRenderer != null)
        {
            if (facing == PlayerDirection.Left)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }
    #region State Functions

    public PlayerState GetState()
    {
        return state;
    }


    public void SetState(PlayerState newState)
    {
        state = newState;
    }

    private void DetermineState()
    {
        if (grounded)
        {
            if (Input.GetButton("Horizontal"))
            {
                SetState(PlayerState.Run);
            }
            else
            {
                SetState(PlayerState.Idle);
            }
        }
        else
        {
            if (jumping)
            {
                SetState(PlayerState.Jump);
            }
            else
            {
                SetState(PlayerState.Fall);
            }
        }
    }
    #endregion
}
