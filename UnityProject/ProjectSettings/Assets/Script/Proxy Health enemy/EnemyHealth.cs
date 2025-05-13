using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable {
    public float MaxHealth = 100f;
    private float currentHealth;

    void Start() {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float amount) {
        currentHealth -= amount;
        Debug.Log($"[Enemy] took {amount} damage. Remaining: {currentHealth}");

        if (currentHealth <= 0f) {
            Die();
        }
    }

    private void Die() {
        Debug.Log("[Enemy] has died.");
        Destroy(gameObject);
    }
}