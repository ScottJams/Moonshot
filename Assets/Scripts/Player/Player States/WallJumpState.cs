using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpState : PlayerState
{

    // Whether or not the wall jump is in progress
    private bool wallJumping;
    // Current time left to complete wall jumping
    private float currentWallJumpTime;
    // The vector In which to wall jump
    private Vector2 wallJumpVector;
    // Time after starting the wall jump to check for another wall collision
    private const float wallJumpTransitionDelay = 0.1f;
    void Awake()
    {
        stateAnimationName = "Wall Jump";
    }

    public override void OnEnable()
    {
        base.OnEnable();
        wallJumping = true;
        currentWallJumpTime = playerController.wallJumpDuration;
        wallJumpVector = new Vector2(0,0);
        AudioManager.SharedInstance.PlaySoundEffect(SoundEffect.jump);
    }

    public void OnDisable()
    {
        ResetWallJump();
    }

    public override void LogicUpdate()
    {
        // First time through, use collision direction to determine wall jump direction
        if (wallJumpVector.x == 0 && wallJumpVector.y == 0)
        {
            // Vertical
            wallJumpVector.y = playerController.wallJumpHeight;
            
            // If touching wall on right side, jump left
            if (playerController.TouchingWallRight())
            {
                wallJumpVector.x = -1;
            } 
            // If touching wall on left side, jump right
            else if (playerController.TouchingWallLeft())
            {
                wallJumpVector.x = 1;
            }

        }


        // If no longer wall jumping, switch to falling
        if (!wallJumping)
        {
            playerController.ChangeToState(playerController.fallState);
        }
        // If hit a wall during wall jump, switch to wall sliding state
        if (currentWallJumpTime < (playerController.wallJumpDuration - wallJumpTransitionDelay) && 
            (playerController.TouchingWallLeft() || playerController.TouchingWallRight()))
        {
            playerController.ChangeToState(playerController.wallSlidingState);
        }
        
        // If hit the ground, switch to idle
        else if (playerController.IsGrounded())
        {
            playerController.ChangeToState(playerController.idleState);
        } 
        else if (dashInput && playerController.remainingDashes > 0)
        {
            playerController.ChangeToState(playerController.dashState);
        }
    }

    public override void PhysicsUpdate()
    {
        // If wall jump has completed, reset the wall jump properties
        if (currentWallJumpTime <= 0)
        {
            ResetWallJump();
        }
        else
        // Continue wall jump
        {
            currentWallJumpTime -= Time.deltaTime;
            playerController.RigidBody.velocity = (wallJumpVector * playerController.wallJumpSpeed);
        }

    }

    // Resets the walljump timer before exiting state
    private void ResetWallJump()
    {
        wallJumping= false;
        wallJumpVector.x = 0;
        wallJumpVector.y = 0;
        currentWallJumpTime = playerController.wallJumpDuration;
    }


}
