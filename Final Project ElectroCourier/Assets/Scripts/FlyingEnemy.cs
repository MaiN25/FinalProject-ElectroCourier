using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed;
    private GameObject player;

    public LayerMask playerLayer;
    public Collider2D head;
    private float headY;
    public bool canChase = false;
    public bool causedDamage = false;
    public Transform[] targets;
    public int currentTarget;
    private Game_Manager gameManager;
    private SoundControl sc;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        headY = transform.position.y + head.offset.y;
        gameManager = GameObject.Find("GameManager").GetComponent<Game_Manager>();
        sc = GameObject.FindObjectOfType<SoundControl>();
        currentTarget = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            Debug.LogError("Can't find the player in the scene");
        }
        if (gameManager == null)
        {
            Debug.LogError("Can't find the Game Manager in the scene");
        }
        //if chasing is permitted, chase the player
        //else go back to the starting position
        if (canChase && causedDamage == false)
        {
            //if chasing is permitted, chase the player
            Chase();
        }
        else
        {
            GoToTarget();
        }

        
        Flip();
    }
  
    private void GoToTarget()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, targets[currentTarget].position, speed * Time.deltaTime);
        if(isOnTarget(targets[currentTarget]))
        {
            if(causedDamage == true)
            {
                causedDamage = false;
                canChase = true;
            }
            SwitchCurrentTarget();
        } 
           
    }

    void FixedUpdate()
    {
        
    }
    void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player"& causedDamage == false)
        {
            causedDamage = true;
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
    void Flip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    
    bool isOnTarget(Transform target)
    {
        if(Vector2.Distance(transform.position, target.position) <= .1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void SwitchCurrentTarget()
    {
        if(currentTarget + 1 >= targets.Length)
        {
            currentTarget = 0;
        }
        else
        {
            currentTarget++;
        }
    }

}
