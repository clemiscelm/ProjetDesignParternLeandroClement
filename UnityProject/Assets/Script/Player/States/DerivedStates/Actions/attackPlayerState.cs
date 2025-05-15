using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerState : PlayerState
{
    private Vector2 _timeSinceEnteredState;
    private bool hasAlreadyHit = false;

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
        StateMachine.Velocity -= StateMachine.Velocity * (Time.deltaTime / _playerMovementParameters.decelerationTime);

        if (StateMachine.attackCooldown <= 0)
        {
            StateMachine.attackCooldown = 1/_playerMovementParameters.AttackSpeed;
            StateMachine.Animator.SetTrigger("Attack");
            hasAlreadyHit = false;
            PerformAttackHitDetection();
        }
        if (!_inputsManager.InputInteract)
        {
            if (_inputsManager.MoveX != 0 || _inputsManager.MoveY != 0)
            {
                StateMachine.ChangeState(StateMachine.RunningState);
                return;
            }
            StateMachine.ChangeState(StateMachine.IdleState);
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
        velocityComponent = (speedRatio/_playerMovementParameters.VelocityPenaltyOnAttack) * _playerMovementParameters.maxSpeed;
    }

    private void PerformAttackHitDetection()
    {
        if (hasAlreadyHit) return;

        float attackRange = 1.5f;
        float attackDamage = 50f;
        Vector3 attackPosition = StateMachine.transform.position + StateMachine.transform.forward * 1.5f;

        Collider[] hits = Physics.OverlapSphere(attackPosition, attackRange);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Debug.Log("Hit an enemy");
                IDamageable damageable = hit.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(attackDamage);
                    Debug.Log("Enemy hit by player attack: -" + attackDamage);
                    hasAlreadyHit = true;
                }
            }
        }
    }
    


}