using UnityEngine;
using TMPro;

public class DartboardScoring : MonoBehaviour
{
    public static DartboardScoring Instance;

    public TMP_Text scoreText;
    public TMP_Text timerText;
    public ParticleSystem timerEndParticleSystem;

    public float timerDuration = 20f;
    private float timeRemaining;
    private bool timerIsRunning = false;

    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            timeRemaining = timerDuration;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                ResetGame();
            }
        }
    }

    public void AddScore(int points)
    {
        if (!timerIsRunning)
        {
            StartTimer();
        }

        score += points;
        scoreText.text = "Score: " + score;
    }

    private void StartTimer()
    {
        timerIsRunning = true;
        timeRemaining = timerDuration; // Reset timer to 60 seconds
    }

    private void ResetGame()
    {
        score = 0;
        scoreText.text = "Score: " + score;
        timerText.text = "Time: 20";
        timeRemaining = timerDuration; // Reset the timer

        if (timerEndParticleSystem != null)
        {
            timerEndParticleSystem.Play();
        }
    }

    private void UpdateTimerDisplay()
    {
        timerText.text = "Time: " + Mathf.RoundToInt(timeRemaining);
    }
}
