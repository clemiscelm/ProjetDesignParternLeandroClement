using UnityEngine;

public class IsPlayerVisibleNode : Node {
    private EnemyBT enemy;

    public IsPlayerVisibleNode(EnemyBT enemy) {
        this.enemy = enemy;
    }

    public override NodeState Evaluate() {
        float distance = Vector3.Distance(enemy.transform.position, enemy.Player.position);
        return distance <= enemy.SightRange ? NodeState.Success : NodeState.Failure;
    }
}