using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;

// Class which manages the game

public class Game_Manager : MonoBehaviour
{
    // The global instance for other scripts to reference
    public static Game_Manager instance = null;

    [Header("References:")]
    [Tooltip("The UIManager component which manages the current scene's UI")]
    public UIManager uiManager = null;
    [Tooltip("The player gameobject")]
    public GameObject player = null;

    [Header("Scores")]
    [Tooltip("The player's score")]
    [SerializeField] public float gameManagerScore = 0;

    // Static getter/setter for player score (for convenience)
    public static float score
    {
        get
        {
            return instance.gameManagerScore;
        }
        set
        {
            instance.gameManagerScore = value;
        }
    }

    [Tooltip("The highest score acheived on this device")]
    public float highScore = 0;


    [Tooltip("Page index in the UIManager to go to on winning the game")]
    public int gameVictoryPageIndex = 0;
    [Tooltip("The victory effect to create when the player won")]
    public GameObject victoryEffect;


    [Header("Health")]
    public float currentHealth = 1f;
    public GameObject healthBar;

    private void Awake()
    {
        // When this component is first added or activated, setup the global reference
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    private void Start()
    {
        // Less urgent startup behaviors, like loading highscores
        if (PlayerPrefs.HasKey("highscore"))
        {
            highScore = PlayerPrefs.GetFloat("highscore");
        }
        if (PlayerPrefs.HasKey("score"))
        {
            score = PlayerPrefs.GetFloat("score");
        }

    }

    //function that gets called when the application (or playmode) ends
    private void OnApplicationQuit()
    {
        SaveHighScore();
        ScoreDisplay.Reset();
    }


    // Sends out a message to UI elements to update
    public static void UpdateUIElements()
    {
        if (instance != null && instance.uiManager != null)
        {
            instance.uiManager.UpdateUI();
        }
    }



    // Ends the level, meant to be called when the level is complete (End of level reached)
    public void LevelCleared()
    {
        PlayerPrefs.SetFloat("score", score);
        Instantiate(victoryEffect, player.transform);
        if (uiManager != null)
        {
            Time.timeScale = 0;
            uiManager.allowPause = false;
            uiManager.GoToPage(gameVictoryPageIndex);
        }
    }

    [Header("Game Over Settings:")]
    [Tooltip("The index in the UI manager of the game over page")]
    public int gameOverPageIndex = 0;
    [Tooltip("The game over effect to create when the game is lost")]
    public GameObject gameOverEffect;

    // Whether or not the game is over
    [HideInInspector]
    public bool gameIsOver = false;

    
    // Displays game over screen
    public void GameOver()
    {
        gameIsOver = true;
        if (gameOverEffect != null)
        {
            Instantiate(gameOverEffect, transform.position, transform.rotation, null);
        }
        if (uiManager != null)
        {
            Time.timeScale = 0;
            uiManager.allowPause = false;
            uiManager.GoToPage(gameOverPageIndex);
        }
    }
  
  

    public void ChangeHealthBar()
    {
        //indicate the player's health
        healthBar.GetComponent<UnityEngine.UI.Image>().fillAmount = currentHealth;
        PlayerPrefs.SetFloat("Health", currentHealth);
        if (currentHealth <= 0)
        {
            GameOver();
        }
        UpdateUIElements();
    }
 

    // Resets the game player prefs of the health and high score

    public static void ResetGamePlayerPrefs()
    {

        PlayerPrefs.SetFloat("Health", 1f);
        PlayerPrefs.SetInt("highscore", 0);
        ScoreDisplay.highscore = 0;

    }


    // Saves the player's highscore
    public static void SaveHighScore()
    {
        if (score > instance.highScore)
        {
            PlayerPrefs.SetFloat("highscore", score);
            instance.highScore = score;
        }
        UpdateUIElements();
    }


    // Resets the high score in player preferences

    public static void ResetHighScore()
    {
        PlayerPrefs.SetInt("highscore", 0);
        if (instance != null)
        {
            instance.highScore = 0;
        }
        UpdateUIElements();
    }

}
