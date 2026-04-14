using UnityEngine;
using TMPro;

public class NPCText : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text dialogueText;

    [Header("Dialogue Lines")]
    [TextArea(2, 4)]
    [SerializeField] private string[] dialogueLines;

    private int currentLine = 0;
    private bool playerInRange = false;
    private bool dialogueActive = false;

    void Start()
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
        }
    }

    void Update()
    {
        if (!playerInRange) return;

        if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;

            if (currentLine < dialogueLines.Length)
            {
                dialogueText.text = dialogueLines[currentLine];
            }
            else
            {
                EndDialogue();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;
        StartDialogue();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
    }

    public void TriggerNextKeySequence()
    {
        StartDialogue();
    }

    private void StartDialogue()
    {
        if (dialogueLines == null || dialogueLines.Length == 0) return;

        dialogueActive = true;
        currentLine = 0;

        dialogueBox.SetActive(true);
        dialogueText.text = dialogueLines[currentLine];
    }
    

    private void EndDialogue()
    {
        dialogueActive = false;
        dialogueBox.SetActive(false);
    }
}