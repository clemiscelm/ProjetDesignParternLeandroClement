using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlayerState : PlayerState
{

    protected override void OnStateInit()
    {
    }

    protected override void OnStateEnter(PlayerState previousState)
    {
        StateMachine.Velocity = Vector2.zero;
        StateMachine.Animator.SetTrigger("Die");
    }

    protected override void OnStateExit(PlayerState nextState)
    {
    }

    protected override void OnStateUpdate()
    {
    }
}