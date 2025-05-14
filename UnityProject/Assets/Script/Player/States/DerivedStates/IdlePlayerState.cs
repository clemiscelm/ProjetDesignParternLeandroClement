using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePlayerState : PlayerState
{
    protected override void OnStateInit()
    {
    }

    protected override void OnStateEnter(PlayerState previousState)
    {
    }

    protected override void OnStateExit(PlayerState nextState)
    {
    }

    protected override void OnStateUpdate()
    {
        if (_inputsManager.MoveX != 0 || _inputsManager.MoveY != 0)
        {
            StateMachine.ChangeState(StateMachine.RunningState);
            return;
        }
        if(_inputsManager.InputInteract)
        {
            StateMachine.ChangeState(StateMachine.AttackState);
            return;
        }
    }
}
