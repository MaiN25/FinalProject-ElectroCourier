using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    SaveLoadData sld;

    private void Start()
    {
        sld = FindObjectOfType<SaveLoadData>();
    }

    public void Replay(string sceneName)
    {
        Time.timeScale = 1;
        Game_Manager.SaveHighScore();
        sld.NewGame(1);

    }
}
