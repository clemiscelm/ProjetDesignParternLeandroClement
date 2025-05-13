using UnityEngine;

public class PatrolNode : Node {
    private EnemyBT enemy;
    private int currentWaypoint = 0;

    public PatrolNode(EnemyBT enemy) {
        this.enemy = enemy;
    }

    public override NodeState Evaluate() {
        if (enemy.PatrolPoints.Length == 0) return NodeState.Failure;

        var target = enemy.PatrolPoints[currentWaypoint];
        enemy.Agent.destination = target.position;

        if (!enemy.Agent.pathPending && enemy.Agent.remainingDistance < 0.5f) {
            currentWaypoint = (currentWaypoint + 1) % enemy.PatrolPoints.Length;
        }

        return NodeState.Running;
    }
}