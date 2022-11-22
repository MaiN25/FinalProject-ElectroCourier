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
    
    bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if(Input.GetKeyDown(KeyCode.Space))
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
}
