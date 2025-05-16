using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour, IDamageable {
    public float MaxHealth = 100f;
    private float currentHealth;

    [SerializeField] private Slider healthBar;

    void Start() 
    {
        currentHealth = MaxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = MaxHealth;
            healthBar.value = MaxHealth;
        }
    }

    public void TakeDamage(float amount) 
    {
        currentHealth -= amount;
        healthBar.value = currentHealth;
        if (currentHealth <= 0f) 
        {
            healthBar.gameObject.SetActive(false);
            Die();
        }
    }

    private void Die() 
    {
        PlayerStateMachine sm = gameObject.GetComponent<PlayerStateMachine>();
        sm.ChangeState(sm.DeathState);
    }
}