using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable {
    public float MaxHealth = 100f;
    private float currentHealth;

    void Start() 
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float amount) 
    {
        currentHealth -= amount;
        if (currentHealth <= 0f) 
        {
            Die();
        }
    }

    private void Die() 
    {
        Debug.Log("[Player] has died.");
        gameObject.SetActive(false);
    }
}