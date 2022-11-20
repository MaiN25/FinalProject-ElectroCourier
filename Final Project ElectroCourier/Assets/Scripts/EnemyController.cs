using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public LayerMask player;
    public LayerMask wall;

    private Rigidbody2D rb;
    public Collider2D head;
    private Game_Manager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<Game_Manager>();
    }

    void Update()
    {
        
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }
    
    void FixedUpdate()
    {
        if (head.IsTouchingLayers(player))
        {
            //gameManager.LevelCleared();
            gameObject.SetActive(false);
        }
    }
    
    private void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        moveSpeed *= -1;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Flip();
        }
        if(other.gameObject.tag == "Player")
        {
            gameManager.currentHealth -= 0.2f;
            gameManager.ChangeHealthBar();
        }
            
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Flip();
        }
    }
}

