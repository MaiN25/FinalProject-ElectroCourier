using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class handles quitting out of the game
public class QuitGameButton : MonoBehaviour
{
    SaveLoadData sld;

    private void Start()
    {
        sld = FindObjectOfType<SaveLoadData>();
    }

    public void QuitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Game_Manager.ResetGamePlayerPrefs();
    }

    private void OnApplicationQuit()
    {
        sld.SaveGame();
    }
}
