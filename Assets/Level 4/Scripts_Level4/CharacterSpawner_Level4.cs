using UnityEngine;

public class CharacterSpawner_Level4 : MonoBehaviour
{
    [SerializeField] private GameObject fairyRedCharacter;
    [SerializeField] private GameObject fairyGreenCharacter;
    [SerializeField] private GameObject fairyOrangeCharacter;

    void Start()
    {
        // Turn all fairies off first
        if (fairyRedCharacter != null) fairyRedCharacter.SetActive(false);
        if (fairyGreenCharacter != null) fairyGreenCharacter.SetActive(false);
        if (fairyOrangeCharacter != null) fairyOrangeCharacter.SetActive(false);

        GameObject selectedObject = null;

        // Pick whichever fairy was selected earlier
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

        // The hub portal only works when the entering object has the "Player" tag.
        if (selectedObject != null)
        {
            selectedObject.tag = "Player";
            Debug.Log("Spawned fairy: " + selectedObject.name + " | tag set to Player");
        }
        else
        {
            Debug.LogWarning("No fairy was selected/spawned.");
        }
    }
}