using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int health = 2;

    public System.Action OnDeath;

    public void TakeDamage()
    {
        health--;
    if (health <= 0)
    {
        ScoreManager.Instance?.AddScore(100); // zumbi vale 100
        Destroy(gameObject);
    }
    }
}