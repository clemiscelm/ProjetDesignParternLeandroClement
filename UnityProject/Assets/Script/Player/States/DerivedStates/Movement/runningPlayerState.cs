using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningPlayerState : PlayerState
{
    private Vector2 _timeSinceEnteredState;

    protected override void OnStateInit()
    {
    }

    protected override void OnStateEnter(PlayerState previousState)
    {
        _timeSinceEnteredState = new Vector2(
            StateMachine.Velocity.x / _playerMovementParameters.maxSpeed * _playerMovementParameters.accelerationTime,
            StateMachine.Velocity.y / _playerMovementParameters.maxSpeed * _playerMovementParameters.accelerationTime
        );
    }

    protected override void OnStateExit(PlayerState nextState)
    {
    }

    protected override void OnStateUpdate()
    {
        if (StateMachine.Velocity.magnitude <= 0 && _inputsManager.MoveX == 0 && _inputsManager.MoveY == 0)
        {
            StateMachine.ChangeState(StateMachine.IdleState);
            return;
        }
        if (_inputsManager.InputInteract)
        {
            StateMachine.ChangeState(StateMachine.AttackState);
            return;
        }

        float targetValueX = 0;
        if (_inputsManager.MoveX != 0)
        {
            targetValueX = _playerMovementParameters.accelerationTime * _inputsManager.MoveX;
        }


        float targetValueY = 0;
        if (_inputsManager.MoveY != 0)
        {
            targetValueY = _playerMovementParameters.accelerationTime * _inputsManager.MoveY;
        }


        ProcessAxis(ref _timeSinceEnteredState.x, targetValueX, out StateMachine.Velocity.x);


        ProcessAxis(ref _timeSinceEnteredState.y, targetValueY, out StateMachine.Velocity.y);

        if (_playerMovementParameters.normalizeVelocity && StateMachine.Velocity.magnitude > _playerMovementParameters.maxSpeed)
        {
            StateMachine.Velocity = StateMachine.Velocity.normalized * _playerMovementParameters.maxSpeed;
        }
    }


    private void ProcessAxis(ref float timeValue, float targetValue, out float velocityComponent)
    {

        bool isAccelerating = ((timeValue >= 0 && targetValue > timeValue) ||
                               (timeValue <= 0 && targetValue < timeValue));

        float step;
        if (isAccelerating)
        {
            step = Time.deltaTime / _playerMovementParameters.accelerationTime;
        }
        else
        {
            step = Time.deltaTime / _playerMovementParameters.decelerationTime;
        }


        if (timeValue < targetValue)
        {
            timeValue = Mathf.Min(timeValue + step, targetValue);
        }
        else if (timeValue > targetValue)
        {
            timeValue = Mathf.Max(timeValue - step, targetValue);
        }


        float speedRatio = timeValue / _playerMovementParameters.accelerationTime;
        velocityComponent = speedRatio * _playerMovementParameters.maxSpeed;
    }
}