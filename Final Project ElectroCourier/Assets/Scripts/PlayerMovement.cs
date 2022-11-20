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


    [Tooltip("The sprite renderer that represents the player.")]
    public SpriteRenderer spriteRenderer = null;


    bool canJump;
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
    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if(inputManager == null)
        {
            inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

        }

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        HandleSpriteDirection();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Move()
    {
        movementDirection = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movementDirection * MOVEMENT_SPEED, rb.velocity.y); 
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, JUMP_SPEED), ForceMode2D.Impulse);
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
}
