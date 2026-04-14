using UnityEngine;

public class CharacterSpawner_Level5 : MonoBehaviour
{
    [Header("Fairy Options")]
    [SerializeField] private GameObject fairyRedCharacter;
    [SerializeField] private GameObject fairyGreenCharacter;
    [SerializeField] private GameObject fairyOrangeCharacter;

    [Header("Level 5 Camera")]
    [SerializeField] private CameraMovementLevel5 level5Camera;

    void Start()
    {
        // Disable all characters initially
        if (fairyRedCharacter != null) fairyRedCharacter.SetActive(false);
        if (fairyGreenCharacter != null) fairyGreenCharacter.SetActive(false);
        if (fairyOrangeCharacter != null) fairyOrangeCharacter.SetActive(false);

        GameObject selectedObject = null;

        // Fallback if GameManager is unavailable
        if (GameManager.instance == null)
        {
            Debug.LogWarning("GameManager.instance is null. Defaulting to red fairy.");

            if (fairyRedCharacter != null)
            {
                fairyRedCharacter.SetActive(true);
                selectedObject = fairyRedCharacter;
            }
        }
        else
        {
            // Activate selected character based on GameManager value
            switch (GameManager.instance.selectedFairy)
            {
                case "FairyR":
                    if (fairyRedCharacter != null)
                    {
                        fairyRedCharacter.SetActive(true);
                        selectedObject = fairyRedCharacter;
                    }
                    break;

                case "FairyG":
                    if (fairyGreenCharacter != null)
                    {
                        fairyGreenCharacter.SetActive(true);
                        selectedObject = fairyGreenCharacter;
                    }
                    break;

                case "FairyO":
                    if (fairyOrangeCharacter != null)
                    {
                        fairyOrangeCharacter.SetActive(true);
                        selectedObject = fairyOrangeCharacter;
                    }
                    break;

                default:
                    // Default to red if selection is invalid
                    if (fairyRedCharacter != null)
                    {
                        fairyRedCharacter.SetActive(true);
                        selectedObject = fairyRedCharacter;
                    }
                    break;
            }
        }

        // Assign Player tag and camera target
        if (selectedObject != null)
        {
            selectedObject.tag = "Player";

            if (level5Camera != null)
            {
                level5Camera.SetTarget(selectedObject.transform);
            }
        }
        else
        {
            Debug.LogWarning("No fairy was selected or activated.");
        }
    }
}