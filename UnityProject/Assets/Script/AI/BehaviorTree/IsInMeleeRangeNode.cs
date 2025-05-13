using UnityEngine;

public class IsInMeleeRangeNode : Node {
    private EnemyBT enemy;

    public IsInMeleeRangeNode(EnemyBT enemy) {
        this.enemy = enemy;
    }

    public override NodeState Evaluate() {
        float distance = Vector3.Distance(enemy.transform.position, enemy.Player.position);
        return distance <= enemy.MeleeRange ? NodeState.Success : NodeState.Failure;
    }
}