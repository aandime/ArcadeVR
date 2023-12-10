using UnityEngine;
using TMPro;

public class DartboardScoring : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score = 0;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit detected on: " + other.tag); // Logs the tag of the collider hit
        
        if (other.CompareTag("10pt"))
        {
            AddScore(10); // Center hit
        }
        else if (other.CompareTag("5pt"))
        {
            AddScore(5); // Medium zone hit
        }
        else if (other.CompareTag("1pt"))
        {
            AddScore(1); // Outer zone hit
        }
    }

    private void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }
}
