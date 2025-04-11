using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Jogo encerrado.");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadVictoryScene()
    {
        SceneManager.LoadScene("Victory"); // ou tela de vitória
    }
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("Game Over"); // ou tela de game over
    }
}
