using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// reconnect the New game and continue game buttons after going back to the main menu 
public class ButtonReconnection : MonoBehaviour
{
    SaveLoadData sld;
    public string whichButton;
    public int saveSlot;

    void Awake()
    {
        sld = GameObject.FindObjectOfType<SaveLoadData>();
        this.gameObject.GetComponent<Button>().onClick.AddListener(Button_onClick);
    }

    void Button_onClick()
    {
        if (whichButton.Equals("StartGame"))
        {
            sld.NewGame(saveSlot);
        }
        else if (whichButton.Equals("ContinueGame"))
        {
            sld.StartLoad(saveSlot);
        }
    }
}