using UnityEngine;

public class CharacterSpawner_Level6 : MonoBehaviour
{
    [Header("Fairy Options")]
    [SerializeField] private GameObject fairyRedCharacter;
    [SerializeField] private GameObject fairyGreenCharacter;
    [SerializeField] private GameObject fairyOrangeCharacter;
    
    [Header("Level 6 Camera")]
    [SerializeField] private CameraFollow_level6 level6Camera;

    void Start()
    {
        // Turn all off first
        if (fairyRedCharacter != null) fairyRedCharacter.SetActive(false);
        if (fairyGreenCharacter != null) fairyGreenCharacter.SetActive(false);
        if (fairyOrangeCharacter != null) fairyOrangeCharacter.SetActive(false);

        GameObject selectedObject = null;

        // If GameManager is missing, default to red
        if (GameManager.instance == null)
        {
            Debug.LogWarning("GameManager is null, defaulting to red fairy");

            if (fairyRedCharacter != null)
            {
                fairyRedCharacter.SetActive(true);
                selectedObject = fairyRedCharacter;
            }
        }
        else
        {
            switch (GameManager.instance.selectedFairy)
            {
                case "FairyR":
                    selectedObject = fairyRedCharacter;
                    break;

                case "FairyG":
                    selectedObject = fairyGreenCharacter;
                    break;

                case "FairyO":
                    selectedObject = fairyOrangeCharacter;
                    break;

                default:
                    selectedObject = fairyRedCharacter;
                    break;
            }

            if (selectedObject != null)
            {
                selectedObject.SetActive(true);
            }
        }

        // VERY IMPORTANT → tag the active one as Player
        if (selectedObject != null)
        {
            selectedObject.tag = "Player";
            
            if (level6Camera != null)
            {
                level6Camera.SetPlayer(selectedObject.transform);
            }
            
            BossController_level6 boss = FindObjectOfType<BossController_level6>();

            if (boss != null)
            {
                boss.SetPlayer(selectedObject.transform);
            }

            Debug.Log("Level 6 spawned: " + selectedObject.name);
        }
        else
        {
            Debug.LogWarning("No fairy assigned in CharacterSpawner_Level6");
        }
    }
}