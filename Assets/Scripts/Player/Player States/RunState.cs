using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : GroundedState
{
    void Awake()
    {
        stateAnimationName = "Run";
    }

    public override void LogicUpdate()
    {
        if (playerController.RigidBody.velocity.x < 0.5 && 
            playerController.RigidBody.velocity.x > -0.5 && 
            horizontalInput == 0)
        {
            playerController.ChangeToState(playerController.idleState);
        }
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        // Position dust particles whilst running
        Vector2 dustPosition = new Vector2(playerController.transform.position.x, playerController.transform.position.y - 0.5f);
        Instantiate(playerController.dustPrefab, dustPosition, Quaternion.identity);
       // GameObject dustParticle = playerController.PooledParticle();
        //dustParticle.SetActive(true);
       // dustParticle.transform.position = dustPosition; 

        base.PhysicsUpdate();
    }

}
