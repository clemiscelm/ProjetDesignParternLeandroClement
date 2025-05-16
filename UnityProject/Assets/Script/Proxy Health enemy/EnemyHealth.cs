using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamageable {
    public float MaxHealth = 100f;
    private float currentHealth;

    [SerializeField] private Slider healthBar;
    void Start() {
        currentHealth = MaxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = MaxHealth;
            healthBar.value = MaxHealth;
        }
    }

    public void TakeDamage(float amount) {
        currentHealth -= amount;
        healthBar.value = currentHealth;
        Debug.Log($"[Enemy] took {amount} damage. Remaining: {currentHealth}");

        if (currentHealth <= 0f) {
            healthBar.gameObject.SetActive(false);
            Die();
        }
    }

    private void Die() {
        GetComponentInChildren<Animator>().SetTrigger("Die");
        GetComponent<EnemyBT>().isAlive = false;
        //Destroy(gameObject);
    }
}