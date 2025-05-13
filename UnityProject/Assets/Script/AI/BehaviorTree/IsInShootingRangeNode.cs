using UnityEngine;

public class IsInShootingRangeNode : Node {
    private EnemyBT enemy;

    public IsInShootingRangeNode(EnemyBT enemy) {
        this.enemy = enemy;
    }

    public override NodeState Evaluate() {
        float distance = Vector3.Distance(enemy.transform.position, enemy.Player.position);
        return distance <= enemy.ShootingRange ? NodeState.Success : NodeState.Failure;
    }
}