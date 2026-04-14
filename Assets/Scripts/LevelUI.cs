using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    public static LevelUI instance;

    [Header("UI References")]
    public TMP_Text levelText;
    public TMP_Text objectiveText;
    public TMP_Text portalsUnlockedText;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        GameManager.OnProgressChanged += UpdateLevelUI;
    }

    private void OnDisable()
    {
        GameManager.OnProgressChanged -= UpdateLevelUI;
    }

    private void Start()
    {
        UpdateLevelUI();
    }

    public void UpdateLevelUI()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName.StartsWith("Level"))
        {
            string levelNumber = sceneName.Replace("Level", "");
            if (levelText != null)
                levelText.text = "Level: " + levelNumber;
        }
        else if (sceneName == "MainHubV2")
        {
            if (levelText != null)
                levelText.text = "Main Hub";
        }
        else
        {
            if (levelText != null)
                levelText.text = sceneName;
        }

        bool inHub = sceneName == "MainHubV2";

        if (objectiveText != null)
        {
            if (inHub)
            {
                objectiveText.gameObject.SetActive(true);
                objectiveText.text = GameManager.instance.GetHubObjectiveText();
            }
            else
            {
                objectiveText.gameObject.SetActive(false);
            }
        }

        if (portalsUnlockedText != null)
        {
            if (inHub)
            {
                portalsUnlockedText.gameObject.SetActive(true);
                portalsUnlockedText.text = "Portals Unlocked: " + GameManager.instance.GetUnlockedPortalCount() + "/3";
            }
            else
            {
                portalsUnlockedText.gameObject.SetActive(false);
            }
        }

        Debug.Log("Current Scene: " + sceneName);
    }
}