using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnState : PlayerState
{
    private bool timerStarted;
    private const float transitionTimer = 0.5f;

    private void Awake()
    {
        stateAnimationName = "Respawn";
    }

    public override void OnEnable()
    {
        base.OnEnable();
        timerStarted = false;
        playerController.TrailRenderer.emitting = false;
        //AudioManager.SharedInstance.PlaySoundEffect(SoundEffect.respawn);
    }

    public override void LogicUpdate()
    {
        // Start timer first time through
        if (!timerStarted)
        {
            StartCoroutine(ChangeStateAfterDelay());
        }

    }
    public override void SpriteUpdate()
    {
        // Don't flip sprite during respawn
    }

    public override void PhysicsUpdate()
    {
        // Halt movement
        playerController.RigidBody.velocity = Vector2.zero;
        playerController.transform.position = playerController.cameraController.currentScreen.respawnPoint.transform.position;
    }

    IEnumerator ChangeStateAfterDelay()
    {
        timerStarted = true;
        yield return new WaitForSeconds(transitionTimer);
        playerController.ChangeToState(playerController.idleState);
    }
}
