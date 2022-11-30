using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private Game_Manager gameManager;
    private SoundControl sc;
    public float addHealth;
    public GameObject healthPickUp;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<Game_Manager>();
        sc = GameObject.FindObjectOfType<SoundControl>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameManager.currentHealth < 1)
        {
            gameManager.currentHealth += addHealth;
            gameManager.ChangeHealthBar();
            healthPickUp.SetActive(false);
            sc.HealSFX();
        }
        else
        {
            Debug.Log("Your health is full!");
        }
        
    }
}
