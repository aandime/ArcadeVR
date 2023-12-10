using UnityEngine;
using TMPro;

public class MovingDartboardScoring : MonoBehaviour
{
    public static MovingDartboardScoring Instance;

    public TMP_Text scoreText;
    public TMP_Text timerText;
    public ParticleSystem timerEndParticleSystem;
    public float timerDuration = 20f;
    public float speed = 1f; // Movement speed
    public float range = 2f; // Range of movement from the starting position

    private float minX;
    private float maxX;
    private float direction = 1f;
    private float timeRemaining;
    private bool timerIsRunning = false;
    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Instance.InitializeMovement();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        timeRemaining = timerDuration;
    }

    private void Update()
    {
        UpdateMovement();
        UpdateTimer();
    }

    private void InitializeMovement()
    {
        float startX = transform.position.x;
        minX = startX - range;
        maxX = startX + range;
    }

    private void UpdateMovement()
    {
        float newX = transform.position.x + direction * speed * Time.deltaTime;

        if (newX > maxX || newX < minX)
        {
            direction *= -1f;
        }

        transform.position = new Vector3(
            Mathf.Clamp(newX, minX, maxX), 
            transform.position.y, 
            transform.position.z);
    }

    private void UpdateTimer()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timerText.text = "Time: " + Mathf.RoundToInt(timeRemaining);
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
        timeRemaining = timerDuration;
    }

    private void ResetGame()
    {
        score = 0;
        scoreText.text = "Score: " + score;
        timerText.text = "Time: 20";
        timeRemaining = timerDuration;

        if (timerEndParticleSystem != null)
        {
            timerEndParticleSystem.Play();
        }

        DestroyAllDarts();
    }

    private void DestroyAllDarts()
    {
        // Iterate through all child objects (darts) and destroy them
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Dart")) // Make sure to tag your darts with "Dart"
            {
                Destroy(child.gameObject);
            }
        }
    }
}
