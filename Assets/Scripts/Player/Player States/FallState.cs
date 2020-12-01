using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : AerialState
{
    void Awake()
    {
        stateAnimationName = "Fall";
    }

    public override void LogicUpdate()
    {
        if (playerController.IsGrounded())
        {
            playerController.ChangeToState(playerController.idleState);
        }
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
