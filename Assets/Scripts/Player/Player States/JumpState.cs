using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AerialState
{

    // Whether to apply initial jump velocity this frame
    private bool shouldJump;

    void Awake()
    {
        stateAnimationName = "Jump";
    }

    public override void OnEnable()
    {
        base.OnEnable();
        shouldJump = true;
        AudioManager.SharedInstance.PlaySoundEffect(SoundEffect.jump);
    }

    public override void LogicUpdate()
    {
        // If player hits the ground but isnt starting their jump, they have landed
        if (playerController.IsGrounded() && !shouldJump)
        {
            playerController.ChangeToState(playerController.idleState);
        }
        // If player is moving down and isnt grounded, they are falling
        else if (playerController.IsFalling())
        {
            playerController.ChangeToState(playerController.fallState);
        }

        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
      
        // Vertical movement - apply jump velocity the first time
        if (shouldJump)
        {
            Vector2 jumpVelocity = new Vector2(0, 0);
            jumpVelocity.y = Mathf.Sqrt(2 * JumpHeight * Mathf.Abs(Physics2D.gravity.y));
            shouldJump = false;
            playerController.RigidBody.velocity += jumpVelocity;
        }

        base.PhysicsUpdate();
    }

}
