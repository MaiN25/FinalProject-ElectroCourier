using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class handles interactions with the animator component of the player
// It reads the player's state from the controller and animates accordingly

public class PlayerAnimator : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The player Movement script to read state information from")]
    public PlayerMovement player;
    [Tooltip("The animator component that controls the player's animations")]
    public Animator animator;


    void Start()
    {
        ReadPlayerStateAndAnimate();
    }

    void Update()
    {
        ReadPlayerStateAndAnimate();
    }

    // Reads the player's state and then sets and unsets booleans in the animator accordingly
    void ReadPlayerStateAndAnimate()
    {
        if (animator == null)
        {
            return;
        }
        if (player.state == PlayerMovement.PlayerState.Idle)
        {
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isIdle", false);
        }
        if (player.state == PlayerMovement.PlayerState.Run)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        
        if (player.state == PlayerMovement.PlayerState.Jump)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

        if (player.state == PlayerMovement.PlayerState.Fall)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }
        // For end game purposes
        if (player.state == PlayerMovement.PlayerState.Dead)
        {
            animator.SetBool("isDead", true);
        }
        else
        {
            animator.SetBool("isDead", false);
        }
        
    }
}
