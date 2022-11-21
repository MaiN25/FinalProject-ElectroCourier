using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;


    Vector2 movement;


    // Update is called once per frame
    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x > 0 || movement.x < 0)
        {
            movement.y = 0;
        }
        if (movement.y > 0 || movement.y < 0)
        {
            movement.x = 0;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed);
    }

  
}
