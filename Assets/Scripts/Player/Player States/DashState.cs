using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    // Whether or not the dash is in progress
    private bool dashing;
    // Current time spent dashing
    private float currentDashTime;
    // The vector in which to dash
    private Vector2 dashVector;

    void Awake()
    {
        stateAnimationName = "Dash";
    }

    public override void OnEnable()
    {
        base.OnEnable();
        playerController.RemoveDash();
        dashing = true;
        currentDashTime = playerController.dashDuration;
        dashVector = new Vector2(0,0);
        AudioManager.SharedInstance.PlaySoundEffect(SoundEffect.dash);
        playerController.TrailRenderer.emitting = true;
    }

    public void OnDisable()
    {
        ResetDash();
        playerController.TrailRenderer.emitting = false;
    }

    public override void SpriteUpdate()
    {
        base.SpriteUpdate();
    }

    public override void LogicUpdate()
    {
        // First time through, use inputs to determine dash vector.
        if (dashVector.x == 0 && dashVector.y == 0)
        {
            dashVector = new Vector2(horizontalInput, verticalInput);
            // If no input then dash straight ahead
            if (dashVector.x == 0 && dashVector.y == 0)
            {
                dashVector = Vector2.right;
            }

        }


        // If dash is over and grounded, switch to idle
        if (playerController.IsGrounded() && !dashing)
        {
            playerController.ChangeToState(playerController.idleState);
        } 
        // If dash is over and aerial, switch to falling
        else if (!playerController.IsGrounded() && !dashing)
        {
            playerController.ChangeToState(playerController.fallState);
        } 
        // If player touches wall during dash
        else if (playerController.TouchingWallLeft() || playerController.TouchingWallRight())
        {
            playerController.ChangeToState(playerController.wallSlidingState);
        } 
        // Cancel dash into jump
        else if (jumpInput && playerController.IsGrounded())
        {
            playerController.ChangeToState(playerController.jumpState);
        }

    }

    public override void PhysicsUpdate()
    {
        // If dash has completed, reset the dash properties
        if (currentDashTime <= 0) {
            ResetDash();
        }
        else
        // Continue dash
        {
            currentDashTime -= Time.deltaTime;
            playerController.RigidBody.velocity = (dashVector * playerController.dashSpeed);
        } 
 
    }

    // Resets the dash timer before exiting state
    private void ResetDash()
    {
        dashing = false;
        dashVector.x = 0;
        dashVector.y = 0;
        currentDashTime = playerController.dashDuration;
    }

}
