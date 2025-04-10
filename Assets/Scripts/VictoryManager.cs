using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    public int totalSpawners = 3;

    private int spawnersDestroyed = 0;

    public void SpawnerDestroyed()
    {
        spawnersDestroyed++;
        if (spawnersDestroyed >= totalSpawners)
        {
            Debug.Log("Vitória!");
            Invoke("LoadVictoryScene", 2f);
        }
    }

    void LoadVictoryScene()
    {
        SceneManager.LoadScene("Victory"); // ou tela de vitória
    }
}