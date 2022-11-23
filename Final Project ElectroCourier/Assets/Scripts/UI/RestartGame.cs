using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{

    public void Replay(string sceneName)
    {
        Time.timeScale = 1;
        Game_Manager.SaveHighScore();
        ScoreDisplay.Reset();
        SceneManager.LoadScene(sceneName);

    }
}
