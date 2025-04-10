using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public Vector2 direction = Vector2.right;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Zumbi
        if (other.CompareTag("Enemy"))
        {
            ZombieHealth zHealth = other.GetComponent<ZombieHealth>();
            if (zHealth != null)
                zHealth.TakeDamage();

            Destroy(gameObject);
            return;
        }

        // Spawner
        if (other.CompareTag("Spawner"))
        {
            ZombieSpawnerHealth spawnerHealth = other.GetComponent<ZombieSpawnerHealth>();
            if (spawnerHealth != null)
                spawnerHealth.TakeDamage();

            Destroy(gameObject);
            return;
        }

        // Ignorar Player e outras balas (por segurança)
        if (other.CompareTag("Player") || other.CompareTag("Bullet"))
        {
            return;
        }

        // Qualquer outra colisão (parede, props)
        Destroy(gameObject);
    }
}