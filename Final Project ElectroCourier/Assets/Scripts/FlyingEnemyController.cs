using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    public FlyingEnemy enemy;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
                // Changing the state of the enemy from patrolling to chasing
                enemy.canChase = true;
        }
    }
  
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Changing the state of the enemy from chasing to patrolling
            enemy.canChase = false;
            
        }
    }
}
