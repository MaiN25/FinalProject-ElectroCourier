using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    public FlyingEnemy[] flyingEnemies;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(FlyingEnemy enemy in flyingEnemies)
            {
                //Later will be hidden at the start of the game, and will be activated here
                enemy.canChase = true;
            }
        }
    }
  
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (FlyingEnemy enemy in flyingEnemies)
            {
                //Late will return to be hidden
                enemy.canChase = false;
            }
        }
    }
}
