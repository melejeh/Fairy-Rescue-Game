using System.Collections;
using UnityEngine;
using TMPro;

public class NPCSpeech_Level5 : MonoBehaviour
{
    [SerializeField] private GameObject speechBubbleObject;
    [SerializeField] private TMP_Text bubbleText;

    [TextArea] [SerializeField] private string firstLine;
    [TextArea] [SerializeField] private string secondLine;

    private string[] lines;
    private int currentLine = 0;
    private bool isDialogueActive = false;

    void Start()
    {
        speechBubbleObject.SetActive(false);

        // Put your lines into an array like Level 1
        lines = new string[] { firstLine, secondLine };
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        currentLine = 0;
        speechBubbleObject.SetActive(true);
        bubbleText.text = lines[currentLine];
        isDialogueActive = true;
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;

            if (currentLine < lines.Length)
            {
                bubbleText.text = lines[currentLine];
            }
            else
            {
                speechBubbleObject.SetActive(false);
                isDialogueActive = false;
            }
        }
    }
}