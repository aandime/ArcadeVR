using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score = 0; // Variable to keep track of the score
    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component for score display
    private float scoreCooldown = 2f; // 2 seconds cooldown
    private float lastScoreTime = 0;

    public ParticleSystem scoreEffect; // Reference to the Particle System for scoring effect

    public float gameDuration = 60f; // Duration of the game in seconds
    private float timer;
    public TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component for timer display

    private bool gameStarted = false; // Flag to track if the game has started

    void Start()
    {
        scoreText.text = "Score: " + score;
        if (timerText != null) 
        {
            timerText.text = "Game not started";
        }
    }

    void Update() 
    {
        if (gameStarted)
        {
            timer -= Time.deltaTime;

            if (timerText != null) 
            {
                timerText.text = "Time: " + Mathf.CeilToInt(timer).ToString();
            }

            if (timer <= 0) 
            {
                ResetGame();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball") && Time.time > lastScoreTime + scoreCooldown) 
        {
            if (!gameStarted)
            {
                gameStarted = true;
                timer = gameDuration;
            }
            IncreaseScore();
            lastScoreTime = Time.time;
        }
    }

    void IncreaseScore()
    {
        score++;
        scoreText.text = "Score: " + score;
        scoreEffect.Play(); // Play the scoring effect
    }

    void ResetGame()
    {
        score = 0;
        scoreText.text = "Score: " + score;

        timer = gameDuration;
        gameStarted = false;

        if (timerText != null) 
        {
            timerText.text = "Game not started";
        }
    }
}
