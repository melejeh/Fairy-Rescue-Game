using UnityEngine;

public class CharacterSpawner_MainHub : MonoBehaviour
{
    [Header("Fairy Options")]
    [SerializeField] private GameObject fairyRedCharacter;
    [SerializeField] private GameObject fairyGreenCharacter;
    [SerializeField] private GameObject fairyOrangeCharacter;

    [Header("Camera Follow")]
    [SerializeField] private CameraFollow_MainHub cameraFollow;

    void Start()
    {
        // Turn all fairies OFF first
        if (fairyRedCharacter != null) fairyRedCharacter.SetActive(false);
        if (fairyGreenCharacter != null) fairyGreenCharacter.SetActive(false);
        if (fairyOrangeCharacter != null) fairyOrangeCharacter.SetActive(false);

        GameObject selectedObject = null;

        // Pick the selected fairy from GameManager
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

            default:
                fairyRedCharacter.SetActive(true);
                selectedObject = fairyRedCharacter;
                break;
        }

        if (selectedObject != null)
        {
            // Make sure it is tagged as Player
            selectedObject.tag = "Player";

            // VERY IMPORTANT: Assign camera target
            if (cameraFollow != null)
            {
                cameraFollow.target = selectedObject.transform;
            }
            else
            {
                Debug.LogWarning("CameraFollow_MainHub is not assigned!");
            }

            Debug.Log("Main Hub Spawned: " + selectedObject.name);
        }
        else
        {
            Debug.LogWarning("No fairy was selected!");
        }
    }
}