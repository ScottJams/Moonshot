using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : PlayerState
{
    protected bool jump;

    public override void OnEnable()
    {
        base.OnEnable();
        playerController.ResetDashes();
    }

    public override void LogicUpdate()
    {
        if (playerController.IsFalling())
        {
            playerController.ChangeToState(playerController.fallState);
        }
        else if (jumpInput)
        {
            playerController.ChangeToState(playerController.jumpState);
        }
        else if (dashInput)
        {
            playerController.ChangeToState(playerController.dashState);
        }
        else if (horizontalInput != 0)
        {
            playerController.ChangeToState(playerController.runState);
        }

    }

    public override void SpriteUpdate()
    {
        // Disable trail emissions
        base.SpriteUpdate();
    }

    public override void PhysicsUpdate()
    {
        // New velocity
        Vector2 newVelocity = new Vector2(0, 0);

        // Horizontal movement
        // Accelerate towards max speed
        if (horizontalInput != 0)
        {
            newVelocity.x = Mathf.MoveTowards(
                playerController.RigidBody.velocity.x,
                MaxMovementSpeed * horizontalInput,
                RunAcceleration * Time.deltaTime);
        }
        else
        // Decelerate towards 0
        {
            newVelocity.x = Mathf.MoveTowards(
                playerController.RigidBody.velocity.x,
                0,
                GroundDeceleration * Time.deltaTime);

        }

        // Vertical movement
        newVelocity.y = 0;
        // Move
        playerController.RigidBody.velocity = newVelocity;
    }

}
