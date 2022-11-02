using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// This class stores relevant information about a page of UI
public class UIPage : MonoBehaviour
{
    [Tooltip("The default UI to have selected when opening this page")]
    public GameObject defaultSelected;


    // Sets the currently selected UI to the one defaulted by this UIPage

    public void SetSelectedUIToDefault()
    {
        if (Game_Manager.instance != null && Game_Manager.instance.uiManager != null && defaultSelected != null)
        {
            Game_Manager.instance.uiManager.eventSystem.SetSelectedGameObject(null);
            Game_Manager.instance.uiManager.eventSystem.SetSelectedGameObject(defaultSelected);
        }

    }
}
