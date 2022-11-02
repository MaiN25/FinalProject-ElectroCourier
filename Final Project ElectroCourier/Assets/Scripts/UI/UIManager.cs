using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [Header("Page Management")]
    [Tooltip("The pages (Panels) managed by the UI Manager")]
    public List<UIPage> pages;
    [Tooltip("The index of the active page in the UI")]
    public int currentPage = 0;
    [Tooltip("The page (by index) switched to when the UI Manager starts up")]
    public int defaultPage = 0;

    [Header("Pause Settings")]
    [Tooltip("The index of the pause page in the pages list")]
    public int pausePageIndex = 1;
    [Tooltip("Whether or not to allow pausing")]
    public bool allowPause = true;
    [Header("Polish Effects")]

    [Tooltip("The effect to create when clicking on or pressing a UI element")]
    public GameObject clickEffect;


    // Whether the application is paused
    private bool isPaused = false;

    // A list of all UI element classes
    private List<UIelement> UIelements;

    // The event system handling UI navigation
    [HideInInspector]
    public EventSystem eventSystem;
    // The Input Manager to listen for pausing
    [SerializeField]
    private InputManager inputManager;


    // Creates a click effect if one is set
    public void CreateClickEffect()
    {
        if (clickEffect)
        {
            Instantiate(clickEffect, transform.position, Quaternion.identity, null);
        }
    }




    // function called when the attached gameobject becomes enabled
    // When this component wakes up (including switching scenes) it sets itself as the GameManager's UI manager

    private void OnEnable()
    {
        SetupGameManagerUIManager();
    }


    // Sets this component as the UI manager for the GameManager

    private void SetupGameManagerUIManager()
    {
        if (Game_Manager.instance != null && Game_Manager.instance.uiManager == null)
        {
            Game_Manager.instance.uiManager = this;
        }
    }


    // Finds and stores all UIElements in the UIElements list
    private void SetUpUIElements()
    {
        UIelements = FindObjectsOfType<UIelement>().ToList();
    }


    // Gets the event system from the scene if one exists
    // If one does not exist a warning will be displayed
    private void SetUpEventSystem()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem == null)
        {
            Debug.LogWarning("There is no event system in the scene but you are trying to use the UIManager. /n" +
                "All UI in Unity requires an Event System to run. /n" +
                "You can add one by right clicking in hierarchy then selecting UI->EventSystem.");
        }
    }


    // Attempts to set up an input manager with this UI so manager so it can get pause input
    private void SetUpInputManager()
    {
        if (inputManager == null)
        {
            inputManager = InputManager.instance;
        }
        if (inputManager == null)
        {
            Debug.LogWarning("The UIManager is missing a reference to an Input Manager, without a Input Manager the UI can not pause");
        }
    }


    // If the game is paused, unpauses the game.
    // If the game is not paused, pauses the game.

    public void TogglePause()
    {
        if (allowPause)
        {
            if (isPaused)
            {
                GoToPage(defaultPage);
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                GoToPage(pausePageIndex);
                Time.timeScale = 0;
                isPaused = true;
            }
        }
    }


    // Goes through all UI elements and calls their UpdateUI function

    public void UpdateUI()
    {
        foreach (UIelement uiElement in UIelements)
        {
            uiElement.UpdateUI();
        }
    }


    private void Start()
    {
        SetUpInputManager();
        SetUpEventSystem();
        SetUpUIElements();
        InitilizeFirstPage();
        UpdateUI();
    }

    // Sets up the first UI page
    private void InitilizeFirstPage()
    {
        GoToPage(defaultPage);
    }


    private void Update()
    {
        CheckPauseInput();
    }


    // If the input manager is set up, reads the pause input

    private void CheckPauseInput()
    {
        if (inputManager != null)
        {
            if (inputManager.pauseButton == 1)
            {
                TogglePause();
                //Consume the input
                inputManager.pauseButton = 0;
            }
        }
    }


    // Goes to a page by that page's index

    public void GoToPage(int pageIndex)
    {
        if (pageIndex < pages.Count && pages[pageIndex] != null)
        {
            SetActiveAllPages(false);
            pages[pageIndex].gameObject.SetActive(true);
            pages[pageIndex].SetSelectedUIToDefault();
        }
    }


    // Goes to a page by that page's name

    public void GoToPageByName(string pageName)
    {
        UIPage page = pages.Find(item => item.name == pageName);
        int pageIndex = pages.IndexOf(page);
        GoToPage(pageIndex);
    }


    // Turns all stored pages on or off depending on parameters
    public void SetActiveAllPages(bool activated)
    {
        if (pages != null)
        {
            foreach (UIPage page in pages)
            {
                if (page != null)
                    page.gameObject.SetActive(activated);
            }
        }
    }
}
