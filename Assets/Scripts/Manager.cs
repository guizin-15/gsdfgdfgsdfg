using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Play()
    {
        SceneManager.LoadScene("MainScene"); // Carrega a cena chamada "SampleScene"
    }

    // Update is called once per frame
    public void Guide()
    {
        SceneManager.LoadScene("GuideScene"); // Carrega a cena chamada "SampleScene"
    }
}
