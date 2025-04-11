using UnityEngine;

public class ZombieSpawnerHealth : MonoBehaviour
{
    public int health = 5;
    private VictoryManager victoryManager;

    void Start()
    {
        victoryManager = FindFirstObjectByType<VictoryManager>();
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            ScoreManager.Instance?.AddScore(200);

            if (victoryManager != null)
                victoryManager.SpawnerDestroyed();

            Destroy(gameObject);
        }
    }
}