using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTravel : MonoBehaviour
{
    // Door variables: Each door has a separate target scene and x,y offsets. Target door is shared by all instances of door travel, and is used to make sure the player spawns next to the correct door while traversing the rooms
    public static string targetDoor;
    public string targetScene;
    public static SaveLoadData sld;
    public float xOffset = 0;
    public float yOffset = 0;

    private void Start()
    {
        if (sld == null)
        {
            sld = FindObjectOfType<SaveLoadData>();
        }
    }

    // When the player enters a door, it activates the ChangeScene function and makes sure that the PlayerMovement is not stuck on thinking it's in the first level
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangeScene(collision.transform.position);
            if (PlayerComponentFinder.isFirstRoom == true)
            {
                PlayerComponentFinder.isFirstRoom = false;
            }
        }
    }

    // When the player enters a door to switch levels, the target door variable is set so the player knows which door it will come out of in the next scene. The game data is saved to be loaded in the next room and the next room is then loaded
    public void ChangeScene(Vector2 playerPos)
    {
        sld.TempSave();
        targetDoor = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(targetScene);
    }
}
