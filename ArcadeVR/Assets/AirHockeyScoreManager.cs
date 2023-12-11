
// AirHockeyScoreManager
using UnityEngine;
using TMPro;

public class AirHockeyScoreManager : MonoBehaviour
{
    public static AirHockeyScoreManager Instance;

    // Timer Texts for each player
    public TMP_Text player1TimerText;
    public TMP_Text player2TimerText;

    // Score Texts for each player
    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;

    public float gameTime = 60f;
    private float remainingTime;
    private int player1Score = 0;
    private int player2Score = 0;
    private bool gameStarted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool GameStarted
        {
            get { return gameStarted; }
        }
    private void Start()
    {
        remainingTime = gameTime;
        UpdateScoreDisplay();
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (gameStarted && remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerDisplay();

            if (remainingTime <= 0)
            {
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            remainingTime = gameTime; // Reset the timer when the game starts
            UpdateTimerDisplay();
        }
    }

    public void PlayerScored(int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1Score++;
        }
        else if (playerNumber == 2)
        {
            player2Score++;
        }
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        player1ScoreText.text = "Player 1: " + player1Score.ToString();
        player2ScoreText.text = "Player 2: " + player2Score.ToString();
    }

    private void UpdateTimerDisplay()
    {
        string timerString = Mathf.CeilToInt(remainingTime).ToString() + "s";
        player1TimerText.text = timerString;
        player2TimerText.text = timerString;
    }

    private void EndGame()
    {
        remainingTime = gameTime;
        player1Score = 0;
        player2Score = 0;
        gameStarted = false;

        UpdateScoreDisplay();
        UpdateTimerDisplay();
    }
}
