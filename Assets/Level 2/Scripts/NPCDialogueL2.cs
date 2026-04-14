using UnityEngine;

public class NPCDialogueL2 : MonoBehaviour
{
    [SerializeField] private GameObject dialogueUI;

    private void Start()
    {
        Debug.Log("NPC script started on " + gameObject.name);

        if (dialogueUI != null)
        {
            dialogueUI.SetActive(false);
            Debug.Log("Dialogue UI found: " + dialogueUI.name);
        }
        else
        {
            Debug.LogError("Dialogue UI is NOT assigned.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("NPC trigger entered by: " + collision.name + " tag = " + collision.tag);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("PLAYER detected by NPC trigger");

            if (dialogueUI != null)
            {
                dialogueUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("NPC trigger exited by: " + collision.name + " tag = " + collision.tag);

        if (collision.CompareTag("Player"))
        {
            if (dialogueUI != null)
            {
                dialogueUI.SetActive(false);
            }
        }
    }
}