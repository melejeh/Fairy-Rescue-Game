using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public static LivesUI instance;
    public TMP_Text livesText;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateLives(float currentLives)
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives.ToString("0.#");
        }
    }

    private void Start()
    {
        UpdateLives(GameManager.instance.playerLives);
    }
}