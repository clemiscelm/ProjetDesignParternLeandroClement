using UnityEngine;

public class ShootNode : Node {
    private EnemyBT enemy;
    private float lastShotTime = 0f;
    private float shootCooldown = 1f;

    public ShootNode(EnemyBT enemy) {
        this.enemy = enemy;
    }

    public override NodeState Evaluate() {
        if (Time.time - lastShotTime > shootCooldown) {
            lastShotTime = Time.time;
            enemy.shoot();
        }

        enemy.Agent.destination = enemy.Player.position;
        return NodeState.Running;
    }

    
}