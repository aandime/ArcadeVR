using UnityEngine;

public class GoalColliderScript : MonoBehaviour
{
    public int playerNumber; // Assign 1 for Player1Goal and 2 for Player2Goal

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Puck")
        {
            // Start the game if not already started
            if (!AirHockeyScoreManager.Instance.GameStarted)
            {
                AirHockeyScoreManager.Instance.StartGame();
            }
            AirHockeyScoreManager.Instance.PlayerScored(playerNumber);
        }
    }
}
