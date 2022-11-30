using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public GameObject player;
    public BoxCollider2D playerLegs;
    public LayerMask wall;

    private Rigidbody2D rb;
    public Collider2D head;
    private float headY;
    private Game_Manager gameManager;
    private SoundControl sc;
    private bool collidingWithPlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        headY = transform.position.y + head.offset.y;
        collidingWithPlayer = false;
        gameManager = GameObject.Find("GameManager").GetComponent<Game_Manager>();
        sc = GameObject.FindObjectOfType<SoundControl>();
    }

    void Update()
    {
        
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }
    
    void FixedUpdate()
    {
        
    }
    
    private void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        moveSpeed *= -1;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Flip();
        }
        
        if(other.gameObject.tag == "Player" & collidingWithPlayer == false)
        {
            collidingWithPlayer = true;
            if(other.gameObject.transform.position.y >= headY)
            {
                gameObject.SetActive(false);
                ScoreDisplay.score += 500;
                sc.EnemyDeathSFX();
            }
            else
            {
                gameManager.currentHealth -= 0.2f;
                gameManager.ChangeHealthBar();
                sc.PlayerHurtSFX();
            }
        }  
         
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            collidingWithPlayer = false;
        }
    }
}

