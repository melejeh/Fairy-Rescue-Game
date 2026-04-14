using UnityEngine;

public class CharacterSpawner_Level2 : MonoBehaviour
{
    [Header("Fairy Options")]
    // Different fairy choices (drag these in from the scene)
    [SerializeField] private GameObject fairyRedCharacter;
    [SerializeField] private GameObject fairyGreenCharacter;
    [SerializeField] private GameObject fairyOrangeCharacter;

    [Header("Camera Follow")]
    // Reference to the Level 2 camera script
    [SerializeField] private CameraControllerL2 cameraFollow;

    void Start()
    {
        // Turn OFF all fairies at the start
        if (fairyRedCharacter != null) fairyRedCharacter.SetActive(false);
        if (fairyGreenCharacter != null) fairyGreenCharacter.SetActive(false);
        if (fairyOrangeCharacter != null) fairyOrangeCharacter.SetActive(false);

        GameObject selectedObject = null;

        // Safety check in case GameManager doesn't exist
        if (GameManager.instance == null)
        {
            Debug.LogWarning("GameManager instance not found.");
            return;
        }

        // Choose which fairy to activate based on player selection
        switch (GameManager.instance.selectedFairy)
        {
            case "FairyR":
                fairyRedCharacter.SetActive(true);
                selectedObject = fairyRedCharacter;
                break;

            case "FairyG":
                fairyGreenCharacter.SetActive(true);
                selectedObject = fairyGreenCharacter;
                break;

            case "FairyO":
                fairyOrangeCharacter.SetActive(true);
                selectedObject = fairyOrangeCharacter;
                break;

            // Default fallback (just in case)
            default:
                fairyRedCharacter.SetActive(true);
                selectedObject = fairyRedCharacter;
                break;
        }

        if (selectedObject != null)
        {
            // VERY IMPORTANT: tag as Player so enemies + camera can find it
            selectedObject.tag = "Player";

            // Tell the camera to follow this fairy
            if (cameraFollow != null)
            {
                cameraFollow.SetTarget(selectedObject.transform);
            }

            Debug.Log("Level 2 spawned: " + selectedObject.name);
        }
        else
        {
            Debug.LogWarning("No fairy selected for Level 2.");
        }
    }
}