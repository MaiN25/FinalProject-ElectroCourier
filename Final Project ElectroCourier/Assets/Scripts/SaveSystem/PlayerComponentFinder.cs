using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerComponentFinder : MonoBehaviour
{
    public static PlayerComponentFinder instance;
    SaveLoadData sld;
    public InputManager im;
    public Game_Manager gm;
    public PlayerMovement pm;
    public static bool isFirstRoom = true;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        sld = GameObject.FindObjectOfType<SaveLoadData>();
        im = GameObject.FindObjectOfType<InputManager>();
        gm = GameObject.FindObjectOfType<Game_Manager>();
        pm = this.gameObject.GetComponent<PlayerMovement>();
        
        // Make sure SaveLoadData knows Player gameobject, PlayerMovement script, and ItemCollection script
        sld.UpdateUsedObjects(this);
        // If the player is not in the first room, make sure they spawn by the door they were supposed to be travelling through
        if (!isFirstRoom)
        {
            Debug.Log("Setting Player Position to Entrance");
            DoorTravel[] doorList = GameObject.FindObjectsOfType<DoorTravel>();
            for (int i = 0; i < doorList.Length; i++)
            {
                if (doorList[i].targetScene.Equals(DoorTravel.targetDoor))
                {
                    Transform door = doorList[i].transform;
                    DoorTravel desDT = door.GetComponent<DoorTravel>();
                    this.gameObject.transform.position = new Vector2(door.position.x + desDT.xOffset, door.position.y + desDT.yOffset);
                    sld.SetPlayerFromSave(false);
                    break;
                }
            }
        }
        // If the player is in the first room, let the SaveLoadData script handle player position
        else
        {
            sld.SetPlayerFromSave(true);
        }
    }
}
