using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


// might be combined with the door travel script (or would call it)
// can add audio controls
public class Trigger : MonoBehaviour
{

    [Header("Settings")]
    [Tooltip("Enable Single Use to deactivate trigger after one use.")]
    public bool singleUse = false;
    public bool hideInGame = true;

    [Header("Objects visibility")]
    public List<GameObject> objectsToShow;
    public List<GameObject> objectsToHide;


    [Header("Game")]
    [Tooltip("End game when trigger is activated.")]
    public bool endGame;
    public ItemCollection pickups;

    private GameObject[] packages;
    private int collectedPackages;

    void Start()
    {
        if (hideInGame)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        packages = GameObject.FindGameObjectsWithTag("Pickup");
        collectedPackages = pickups.packages;
        Debug.Log(packages.Length);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
        if(collision.gameObject.tag == "Player")
        {
            // Show Objects
            for (int i = 0; i < objectsToShow.Count; i++)
            {
                if (objectsToShow[i] != null)
                {
                    objectsToShow[i].SetActive(true);
                }
            }

            // Hide Objects
            for (int i = 0; i < objectsToHide.Count; i++)
            {
                if (objectsToHide[i] != null)
                {
                    objectsToHide[i].SetActive(false);
                }
            }

            // End Game
            if (endGame)
            {
                //if it's the end of the game and all of the packages been collected, show the winning page 
                //else, show the losing page 
                if (collectedPackages == packages.Length)
                {
                    Game_Manager.instance.LevelCleared();
                }
                else
                {
                    Game_Manager.instance.GameOver();
                }
            }

            // Single Use
            if (singleUse)
            {
                if (GetComponent<Collider2D>() != null)
                {
                    GetComponent<Collider2D>().enabled = false;
                }
            }
        }
    }
}
