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
                //Later will be hidden at the start of the game, and will be activated here
                enemy.canChase = true;
        }
    }
  
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
                //Late will return to be hidden
                enemy.canChase = false;
            
        }
    }
}
