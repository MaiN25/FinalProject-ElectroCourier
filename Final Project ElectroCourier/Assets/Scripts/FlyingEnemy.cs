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

    public bool canChase = false;
    public Transform startingPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            Debug.LogError("Can't find the player in the scene");
        }
        //if chasing is permitted, chase the player
        //else go back to the starting position
        if (canChase)
        {
            //if chasing is permitted, chase the player
            Chase();
        }
        else
        {
            ReturnToStartingPosition();
        }
        Flip();
    }

    private void ReturnToStartingPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPosition.position, speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (head.IsTouchingLayers(playerLayer))
        {
            //gameManager.LevelCleared();
            gameObject.SetActive(false);
        }
    }
    void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
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
}
