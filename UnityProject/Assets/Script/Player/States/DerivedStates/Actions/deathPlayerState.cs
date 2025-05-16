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
        StateMachine.StartCoroutine(WaitForDieAnimation());
    }

    protected override void OnStateExit(PlayerState nextState)
    {
    }

    protected override void OnStateUpdate()
    {
    }

    private IEnumerator WaitForDieAnimation()
    {
        AnimatorStateInfo animationState = StateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        while (!animationState.IsName("Die") || animationState.normalizedTime < 1.0f)
        {
            animationState = StateMachine.Animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }
        StateMachine.gameObject.SetActive(false);
    }
}
