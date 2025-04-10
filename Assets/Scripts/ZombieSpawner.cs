using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public float spawnRadius = 4f;
    public float spawnInterval = 2f;
    public int maxZombies = 5;

    private int currentZombies = 0;

    void Start()
    {
        InvokeRepeating(nameof(TrySpawnZombie), 1f, spawnInterval);
    }

    void TrySpawnZombie()
    {
        if (currentZombies >= maxZombies) return;

        Vector2 spawnPos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
        GameObject zombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
        currentZombies++;

        // Escuta a morte do zumbi
        zombie.GetComponent<ZombieHealth>().OnDeath += () => currentZombies--;
    }

    void OnDrawGizmosSelected()
    {
        // Visualiza o raio do spawner na cena
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}