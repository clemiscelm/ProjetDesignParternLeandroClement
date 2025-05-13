using UnityEngine;

public class MeleeAttackNode : Node {
    private EnemyBT enemy;

    public MeleeAttackNode(EnemyBT enemy) {
        this.enemy = enemy;
    }

    public override NodeState Evaluate() {
        Debug.Log("Enemy performs melee attack!");
        // code to perform melee attack flemme
        return NodeState.Running;
    }
}