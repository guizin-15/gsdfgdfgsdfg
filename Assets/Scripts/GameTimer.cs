using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText;
    private float timer = 0f;
    private bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        timer += Time.deltaTime;
        timerText.text = "Time: " + timer.ToString("F2") + "s";
    }

    public void StopTimer()
    {
        isRunning = false;

        float record = PlayerPrefs.GetFloat("BestTime", 0f);
        if (timer > record)
        {
            PlayerPrefs.SetFloat("BestTime", timer);
            PlayerPrefs.Save();
        }
    }

    public float GetCurrentTime()
    {
        return timer;
    }
}