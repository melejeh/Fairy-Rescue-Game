using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    // Show pause menu and stop time
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    // Return to main hub and resume time
    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainHubV2");
    }

    // Restart game and resume time
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Startscreen");
    }

    // Hide pause menu and resume time
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
