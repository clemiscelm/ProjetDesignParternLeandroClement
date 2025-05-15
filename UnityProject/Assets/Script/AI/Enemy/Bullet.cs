using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 2f;
    public float damage = 10f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            PlayerStateMachine player = collision.gameObject.GetComponent<PlayerStateMachine>();
            if (player != null)
            {
                if (damageable != null) 
                {
                    damageable.TakeDamage(20f);
                    player.ChangeState(player.HitState);
                }
                Destroy(gameObject);
            }
        }
    }
}
