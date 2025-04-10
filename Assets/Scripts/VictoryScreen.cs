using UnityEngine;
using TMPro;

public class VictoryScreen : MonoBehaviour
{
    public TMP_Text finalScoreText;
    public TMP_Text finalTimeText;

    void Start()
    {
        int score = PlayerPrefs.GetInt("Score", 0);
        finalScoreText.text = "Final Score: " + score;

        float time = PlayerPrefs.GetFloat("BestTime", 0f);
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        string formattedTime = $"{minutes:00}:{seconds:00}";

        finalTimeText.text = "Final Time: " + formattedTime;
    }
}