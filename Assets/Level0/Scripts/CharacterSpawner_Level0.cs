using UnityEngine;

public class CharacterSpawner_Level0 : MonoBehaviour
{
    [SerializeField] private GameObject fairyRedCharacter;
    [SerializeField] private GameObject fairyGreenCharacter;
    [SerializeField] private GameObject fairyOrangeCharacter;

    [SerializeField] private CameraFollow_MainHub cameraFollow;

    void Start()
    {
        if (fairyRedCharacter != null) fairyRedCharacter.SetActive(false);
        if (fairyGreenCharacter != null) fairyGreenCharacter.SetActive(false);
        if (fairyOrangeCharacter != null) fairyOrangeCharacter.SetActive(false);

        GameObject selectedObject = null;

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
            selectedObject.tag = "Player";

            if (cameraFollow != null)
            {
                cameraFollow.target = selectedObject.transform;
            }

            Debug.Log("Spawned fairy: " + selectedObject.name);
        }
        else
        {
            Debug.LogWarning("No fairy was selected/spawned.");
        }
    }
}