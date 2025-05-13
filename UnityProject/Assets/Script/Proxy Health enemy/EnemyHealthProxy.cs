using UnityEngine;

public class EnemyHealthProxy : MonoBehaviour, IDamageable {
    public float Armor = 10f;

    private IDamageable realHealth;

    void Awake() 
    {
        realHealth = GetComponent<EnemyHealth>();
    }

    public void TakeDamage(float amount)
    {
        float reducedAmount = Mathf.Max(0, amount - Armor);
        Debug.Log($"[Proxy] Reduced damage from {amount} to {reducedAmount} due to armor ({Armor}).");

        realHealth.TakeDamage(reducedAmount);
    }
}