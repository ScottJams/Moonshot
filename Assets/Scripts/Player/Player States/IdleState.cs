using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundedState
{

    void Awake()
    {
        stateAnimationName = "Idle";
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
