using UnityEngine;
public class MeleeAttackNode : Node {
    private EnemyBT enemy;
    private float lastAttackTime = 0f;
    private float attackCooldown = 1.5f; // en secondes

    public MeleeAttackNode(EnemyBT enemy) {
        this.enemy = enemy;
    }

    public override NodeState Evaluate() {
        if (Time.time - lastAttackTime < attackCooldown) {
            return NodeState.Running;
        }


        float attackRange = 2f;
        float damageAmount = 20f;

        Collider[] hits = Physics.OverlapSphere(enemy.transform.position, attackRange);

        foreach (Collider hit in hits) {
            if (hit.CompareTag("Player")) {
                IDamageable damageable = hit.GetComponent<IDamageable>();
                if (damageable != null) {
                    damageable.TakeDamage(damageAmount);
                    Debug.Log($"Player hit with melee attack: -{damageAmount} HP");
                }
            }
        }

        lastAttackTime = Time.time; // on enregistre le temps de la dernière attaque

        return NodeState.Running;
    }
}
