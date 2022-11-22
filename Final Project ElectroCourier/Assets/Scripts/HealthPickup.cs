using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private Game_Manager gameManager;
    public float addHealth;
    public GameObject healthPickUp;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<Game_Manager>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.currentHealth += addHealth;
            gameManager.ChangeHealthBar();
            healthPickUp.SetActive(false);
        }
    }
}
