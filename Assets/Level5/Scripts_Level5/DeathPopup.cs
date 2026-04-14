using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPopup : MonoBehaviour
{
    // Resets game state and returns to start screen
    public void RestartGame()
    {
        // Ensure game time is running
        Time.timeScale = 1f;

        // Reset player progress
        if (GameManager.instance != null)
        {
            GameManager.instance.playerLives = GameManager.instance.startingLives;
            GameManager.instance.coinsCollected = 0;
            GameManager.instance.keysCollected.Clear();
            GameManager.instance.keysDeposited.Clear();
        }

        // Load start screen scene
        SceneManager.LoadScene("StartScreen");
    }
}
