using UnityEngine;
using UnityEngine.Events;

public class ZombieSpawnerHealth : MonoBehaviour
{
    public int health = 5;
    public UnityEvent OnDestroyed;

    public void TakeDamage()
    {
        health--;
    if (health <= 0)
    {
        ScoreManager.Instance?.AddScore(200); // spawner vale 200
        OnDestroyed.Invoke();
        Destroy(gameObject);
    }
    }
}