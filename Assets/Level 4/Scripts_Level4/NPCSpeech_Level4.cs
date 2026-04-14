using System.Collections;
using UnityEngine;
using TMPro;

public class NPCSpeech_Level4 : MonoBehaviour
{
    [SerializeField] private GameObject speechBubbleObject;
    [SerializeField] private TMP_Text bubbleText;

    [TextArea] [SerializeField] private string firstLine;
    [TextArea] [SerializeField] private string secondLine;
    [SerializeField] private float timeBetweenLines = 2f;
    

    private Coroutine dialogueRoutine;

    void Start()
    {
        speechBubbleObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (dialogueRoutine != null)
            StopCoroutine(dialogueRoutine);

        dialogueRoutine = StartCoroutine(ShowDialogueSequence());
    }

    private IEnumerator ShowDialogueSequence()
    {
        speechBubbleObject.SetActive(true);
        bubbleText.text = firstLine;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        
        bubbleText.text = secondLine;
        
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        
        speechBubbleObject.SetActive(false);
    }
}