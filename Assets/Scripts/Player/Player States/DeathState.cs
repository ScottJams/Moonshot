using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : PlayerState
{
    
    private bool timerStarted;
    private const float respawnTimer = 0.5f;
    private Vector2 initialPos;

    private void Awake()
    {
        stateAnimationName = "Death";
    }

    public override void OnEnable()
    {
        base.OnEnable();
        timerStarted = false;
        playerController.TrailRenderer.emitting = false;
        initialPos = playerController.transform.position;
        AudioManager.SharedInstance.PlaySoundEffect(SoundEffect.death);
    }

    public override void LogicUpdate()
    {
        // Start timer first time through
        if (!timerStarted)
        {
            StartCoroutine(Respawn());
        }

    }


    public override void PhysicsUpdate()
    {
        // Halt movement
        playerController.RigidBody.velocity = Vector2.zero;
        playerController.transform.position = initialPos;
    }

    IEnumerator Respawn()
    {
        timerStarted = true;
        yield return new WaitForSeconds(respawnTimer);
        playerController.ChangeToState(playerController.respawnState);
    }
}
