using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadButton : MonoBehaviour
{
    SaveLoadData sld;
    SoundControl sc;

    //Loads a level according to the name provided
    public void LoadLevelByName(string levelToLoadName)
    {
        if (levelToLoadName.Equals("MainMenu"))
        {
            sld = FindObjectOfType<SaveLoadData>();
            sc = sld.sc;
            sld.DeleteTemps();
            sc.SilenceBackSFX();
        }
        SceneManager.LoadScene(levelToLoadName);
    }
}
