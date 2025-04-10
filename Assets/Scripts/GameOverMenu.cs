using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public string gameSceneName = "MainScene";      // nome da cena do jogo
    public string mainMenuSceneName = "MainMenu";   // nome da cena do menu

    public void TryAgain()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Jogo encerrado.");
    }
}