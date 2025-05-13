using UnityEngine;

public class ChaseNode : Node {
    private EnemyBT enemy;

    public ChaseNode(EnemyBT enemy) {
        this.enemy = enemy;
    }

    public override NodeState Evaluate() {
        enemy.Agent.destination = enemy.Player.position;
        return NodeState.Running;
    }
}