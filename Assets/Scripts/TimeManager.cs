using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    private float currentTime = 0f;
    private bool isRunning = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // opcional
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime += Time.deltaTime;
        }
    }

    public void StopTimer()
    {
        isRunning = false;

        float record = PlayerPrefs.GetFloat("BestTime", 0f);
        if (currentTime > record)
        {
            PlayerPrefs.SetFloat("BestTime", currentTime);
            PlayerPrefs.Save();
        }
    }

    public void ResetTimer()
    {
        currentTime = 0f;
        isRunning = true;
    }

    public float GetTime()
    {
        return currentTime;
    }
}