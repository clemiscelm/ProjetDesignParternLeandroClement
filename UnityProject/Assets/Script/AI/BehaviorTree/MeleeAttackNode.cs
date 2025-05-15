using UnityEngine;

public class MeleeAttackNode : Node {
    private EnemyBT enemy;

    public MeleeAttackNode(EnemyBT enemy) {
        this.enemy = enemy;
    }

    public override NodeState Evaluate() {

        PlayerStateMachine playerStateMachine = enemy.Player.GetComponent<PlayerStateMachine>();
        playerStateMachine.ChangeState(playerStateMachine.HitState);
        enemy.Animator.SetTrigger("Attack");
        return NodeState.Running;
    }
}