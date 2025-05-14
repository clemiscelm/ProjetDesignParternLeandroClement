using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayerState : PlayerState
{
    protected override void OnStateInit()
    {
    }

    protected override void OnStateEnter(PlayerState previousState)
    {
        StateMachine.Velocity = Vector2.zero;
        StateMachine.Animator.SetTrigger("Hit");
        _timeSinceEnteredState = 0;
    }

    protected override void OnStateExit(PlayerState nextState)
    {
    }

    protected override void OnStateUpdate()
    {
        if (_timeSinceEnteredState>_playerMovementParameters.StaggerTime)
        {
            if (_inputsManager.MoveX != 0 || _inputsManager.MoveY != 0)
            {
                StateMachine.ChangeState(StateMachine.RunningState);
                return;
            }
            StateMachine.ChangeState(StateMachine.IdleState);
            return;
        }
    }
}