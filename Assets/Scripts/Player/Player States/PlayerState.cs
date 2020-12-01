using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // Player controller + properties
    [SerializeField] protected PlayerController playerController;
    protected float MaxMovementSpeed => playerController.maxHorizontalSpeed;
    protected float RunAcceleration => playerController.runAcceleration;
    protected float AirAcceleration => playerController.airAcceleration;
    protected float GroundDeceleration => playerController.groundDeceleration;
    protected float JumpHeight => playerController.jumpHeight;

    // The animation to play as named on the Animator
    protected string stateAnimationName;
    // Input
    protected float horizontalInput;
    protected float verticalInput;
    protected bool dashInput;
    protected bool jumpInput;

    public virtual void OnEnable()
    {
        if (playerController.PlayerAnimator != null)
        {
            playerController.PlayerAnimator.Play(stateAnimationName);
        }
    }

    // Handle inputs 
    public virtual void HandleInput()
    {
        if (!LevelManager.gameIsPaused)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            dashInput = Input.GetButtonDown("Dash");
            jumpInput = Input.GetButtonDown("Jump");
        }
    }

    // Core logic + state changes
    public virtual void LogicUpdate()
    {
        // Any logic updates common to all states.
    }

    // Sprite rendering - default implementation 
    public virtual void SpriteUpdate()
    {
        playerController.SpriteRenderer.flipX = (horizontalInput < 0) ? true : false;
    }

    // Physics calculations
    public virtual void PhysicsUpdate()
    {
        // Any physics updates common to all states
    }

}
