using UnityEngine;

public class PlayerHealthProxy : MonoBehaviour, IDamageable 
{
    public bool IsInvincible = false;
    public float Armor = 5f;

    private IDamageable realHealth;

    void Awake() 
    {
        realHealth = GetComponent<PlayerHealth>();
    }

    public void TakeDamage(float amount) 
    {
        if (IsInvincible) 
        {
            return;
        }

        float reduced = Mathf.Max(0, amount - Armor);
        realHealth.TakeDamage(reduced);
    }
}