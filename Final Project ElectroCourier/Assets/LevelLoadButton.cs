using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadButton : MonoBehaviour
{
    //Loads a level according to the name provided
    public void LoadLevelByName(string levelToLoadName)
    {
        SceneManager.LoadScene(levelToLoadName);
    }
}
