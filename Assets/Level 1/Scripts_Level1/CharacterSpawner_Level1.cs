using UnityEngine;

public class CharacterSpawner_Level1 : MonoBehaviour
{
    [SerializeField] private GameObject fairyRedCharacter;
    [SerializeField] private GameObject fairyGreenCharacter;
    [SerializeField] private GameObject fairyOrangeCharacter;

    void Start()
    {
        // Disable all fairy characters initially
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
                fairyRedCharacter.tag = "Player";
            }

            return;
        }

        // Activate the selected fairy based on GameManager value
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
                // Default to red fairy if selection is invalid
                if (fairyRedCharacter != null)
                {
                    fairyRedCharacter.SetActive(true);
                    selectedObject = fairyRedCharacter;
                }
                break;
        }

        // Assign Player tag to the selected fairy
        if (selectedObject != null)
        {
            selectedObject.tag = "Player";
        }
        else
        {
            Debug.LogWarning("No fairy was selected or activated.");
        }
    }
}