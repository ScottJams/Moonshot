using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialState : PlayerState
{

    public override void OnEnable()
    {
        base.OnEnable();

        // If vertical velocity is above acceptable level, lower it
        if (playerController.RigidBody.velocity.y > playerController.maxVerticalSpeed)
        {
            playerController.RigidBody.velocity = new Vector2
                (playerController.RigidBody.velocity.x,
                playerController.maxVerticalSpeed);
        }

        //If horizontal velocity is above acceptable level, lower it
        //if (playerController.RigidBody.velocity.x > playerController.maxMovementSpeed)
        //{
        //    playerController.RigidBody.velocity = new Vector2
        //        (0,
        //        playerController.RigidBody.velocity.y);
        //}

    }

    public override void LogicUpdate()
    {
        if (dashInput && playerController.remainingDashes > 0)
        {
            playerController.ChangeToState(playerController.dashState);
        } 
        
        else if (playerController.TouchingWallLeft() || playerController.TouchingWallRight())
        {
            playerController.ChangeToState(playerController.wallSlidingState);
        }


        base.LogicUpdate();
    }

    public override void SpriteUpdate()
    {
        // Enable trail emissions
        base.SpriteUpdate();
    }

    public override void PhysicsUpdate()
    {
        // New velocity
        Vector2 newVelocity = new Vector2(0, 0);

        // Horizontal movement
        if (horizontalInput != 0)
        {
            newVelocity.x = Mathf.MoveTowards(
                playerController.RigidBody.velocity.x,
                MaxMovementSpeed * horizontalInput,
                AirAcceleration * Time.deltaTime);
        }
        else
        {
            newVelocity.x = Mathf.MoveTowards(
               playerController.RigidBody.velocity.x,
                0,
                AirAcceleration * Time.deltaTime);

        }

        // Vertical movement
        newVelocity.y = playerController.RigidBody.velocity.y;
        // Move
        playerController.RigidBody.velocity = newVelocity;
    }

}
