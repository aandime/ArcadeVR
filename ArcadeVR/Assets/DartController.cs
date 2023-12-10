using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; // Include this for TextMeshPro

public class DartController : MonoBehaviour
{
    public float shootForce = 500f; // Adjust the force as needed
    public TMP_Text scoreText; // Reference to the TextMeshPro element for score display
    public TMP_Text timerText; // Reference to the TextMeshPro element for timer display
    public Transform dartboardCenter; // Assign the center of the dartboard in the inspector

    private Rigidbody rb;
    private bool hasHit = false;
    private int score = 0; // Variable to keep track of the score

    private float timeRemaining; // Timer set for 60 seconds
    private bool timerIsRunning = false;

    // Define your scoring radii thresholds
    public float centerZoneRadius = 0.05f;
    public float mediumZoneRadius = 0.17f;
    public float outerZoneRadius = 0.287f;
    public ParticleSystem timerEndParticleSystem;

    public float timerDuration = 60f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnRelease);
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
        timeRemaining = timerDuration;
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

    private void OnRelease(SelectExitEventArgs args)
    {
        if (hasHit) return;

        rb.isKinematic = false;
        rb.AddForce(transform.forward * shootForce);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        hasHit = false;
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;

        if (collision.gameObject.CompareTag("Dartboard"))
        {
            hasHit = true;
            StickDart(collision);
            CalculateScore(collision);
        }
    }

    private void CalculateScore(Collision collision)
    {
        float distance = Vector3.Distance(collision.contacts[0].point, dartboardCenter.position);

        if (distance <= centerZoneRadius)
        {
            UpdateScore(10);
        }
        else if (distance <= mediumZoneRadius)
        {
            UpdateScore(5);
        }
        else if (distance <= outerZoneRadius)
        {
            UpdateScore(1);
        }

        if (!timerIsRunning)
        {
            StartTimer();
        }
    }

    private void UpdateScore(int points)
    {
        score += points;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
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
        timerText.text = "Time: 60";
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

    private void StickDart(Collision collision)
    {
        rb.isKinematic = true;
        transform.SetParent(collision.transform);
    }

    private void OnDestroy()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.RemoveListener(OnRelease);
            grabInteractable.selectEntered.RemoveListener(OnGrab);
        }
    }
}
