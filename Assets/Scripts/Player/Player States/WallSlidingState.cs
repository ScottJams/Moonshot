using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlidingState : PlayerState
{
    // The delay during which horizontal movement is not applied whilst on a wall
    private const float defaultDelay = 0.3f;
    private float wallStickingDelay;

    private void Awake()
    {
        stateAnimationName = "Wall Sliding Right";
    }

    public override void OnEnable()
    {
        // Get correct wall slide animation before playing it
        stateAnimationName = playerController.TouchingWallRight() ? "Wall Sliding Right" : "Wall Sliding Left";

        base.OnEnable();
        wallStickingDelay = defaultDelay;
    }

    public override void SpriteUpdate()
    {
        // Don't inherit base behaviour for this state.
        if (stateAnimationName == "Wall Sliding Right") {
            playerController.SpriteRenderer.flipX = true;
        } 
        else
        {
            playerController.SpriteRenderer.flipX = false;
        }
    
    }

    public override void LogicUpdate()
    {
        // If grounded, transition to idle state
        if (playerController.IsGrounded())
        {
            playerController.ChangeToState(playerController.idleState);
        }
        // If jump pressed, transition to walljump
        else if (jumpInput)
        {
            playerController.ChangeToState(playerController.wallJumpState);
        }
        // If not touching wall, transition to fall state
        else if (!playerController.TouchingWallLeft() && !playerController.TouchingWallRight())
        {
            playerController.ChangeToState(playerController.fallState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        // Dust
        GenerateDust();

        // Reduce wall sticking time
        wallStickingDelay -= Time.deltaTime;

        // New velocity
        Vector2 newVelocity = new Vector2(0, 0);

        // Horizontal movement
        if (horizontalInput != 0 && wallStickingDelay <= 0)
        {
            newVelocity.x = Mathf.MoveTowards(
                newVelocity.x,
                MaxMovementSpeed * horizontalInput,
                AirAcceleration * Time.deltaTime);
        }
        else
        {
            newVelocity.x = Mathf.MoveTowards(
                newVelocity.x,
                0,
                AirAcceleration * Time.deltaTime);

        }

        // Vertical movement - cap speed when sliding down wall but not upwards
        float currentVelocity = playerController.RigidBody.velocity.y;
        if (currentVelocity < playerController.wallSlideDownSpeed)
        {
            newVelocity.y = playerController.wallSlideDownSpeed;
        } else
        {
            newVelocity.y = currentVelocity;
        }
            

        // Move
        playerController.RigidBody.velocity = newVelocity;

    }

    private void GenerateDust()
    {
        Vector2 dustPosition = Vector2.zero;
        
        float dustPosX = 0;
        float dustOffset = 0.5f;

        if (playerController.TouchingWallLeft())
        {
            dustPosX = playerController.transform.position.x - dustOffset;
        }
        else
        {
            dustPosX = playerController.transform.position.x + dustOffset;
        }

        dustPosition.y = playerController.transform.position.y - dustOffset;
        dustPosition.x = dustPosX;

        // Position dust particles whilst running
        Instantiate(playerController.dustPrefab, dustPosition, Quaternion.identity);
    }

}
