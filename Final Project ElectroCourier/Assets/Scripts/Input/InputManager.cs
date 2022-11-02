using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


// Class which handles reading Input for other scripts to reference

public class InputManager : MonoBehaviour
{
    // A global instance for scripts to reference
    public static InputManager instance;


    // Function called when the script is loaded

    private void Awake()
    {
        ResetValuesToDefault();
        // Set up the instance of this
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    // Sets all the input variables to their default values so that nothing weird happens in the game if you accidentally set them in the editor

    void ResetValuesToDefault()
    {
        horizontalMovement = default;
        verticalMovement = default;

        horizontalLookAxis = default;
        verticalLookAxis = default;

        jumpStarted = default;
        jumpHeld = default;

        pauseButton = default;
    }

    [Header("Movement Input")]
    [Tooltip("The horizontal movmeent input of the player.")]
    public float horizontalMovement;
    [Tooltip("The vertical movmeent input of the player.")]
    public float verticalMovement;


    //Reads and stores the movement input
    // <param name="callbackContext">The context of the movement input</param>
    public void GetMovementInput(InputAction.CallbackContext callbackContext)
    {
        Vector2 movementVector = callbackContext.ReadValue<Vector2>();
        horizontalMovement = movementVector.x;
        verticalMovement = movementVector.y;
    }

    [Header("Jump Input")]
    [Tooltip("Whether a jump was started this frame.")]
    public bool jumpStarted = false;
    [Tooltip("Whether the jump button is being held.")]
    public bool jumpHeld = false;


    /// Reads and stores the jump input
    /// <param name="callbackContext">The context of the jump input</param>
    public void GetJumpInput(InputAction.CallbackContext callbackContext)
    {
        jumpStarted = !callbackContext.canceled;
        jumpHeld = !callbackContext.canceled;
        if (InputManager.instance.isActiveAndEnabled)
        {
            StartCoroutine("ResetJumpStart");
        }
    }


    // Coroutine that resets the jump started variable after one frame

    private IEnumerator ResetJumpStart()
    {
        yield return new WaitForEndOfFrame();
        jumpStarted = false;
    }

    [Header("Pause Input")]
    [Tooltip("The state of the pause button")]
    public float pauseButton = 0;

    // Collects pause button input
    public void GetPauseInput(InputAction.CallbackContext callbackContext)
    {
        pauseButton = callbackContext.ReadValue<float>();
    }

    [Header("Mouse Input")]
    [Tooltip("The horizontal mouse input of the player.")]
    public float horizontalLookAxis;
    [Tooltip("The vertical mouse input of the player.")]
    public float verticalLookAxis;

    
    /// Collects movementInput

    public void GetMouseLookInput(InputAction.CallbackContext callbackContext)
    {
        Vector2 mouseLookVector = callbackContext.ReadValue<Vector2>();
        horizontalLookAxis = mouseLookVector.x;
        verticalLookAxis = mouseLookVector.y;
    }
   
}