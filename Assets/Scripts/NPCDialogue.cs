using System.Collections;
using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text bubbleText;

    [Header("Dialogue")]
    [TextArea] public string[] firstTalkLines;
    [TextArea] public string[] repeatTalkLines;

    [SerializeField] private float lineDuration = 3f;

    private Coroutine dialogueRoutine;
    private bool playerInside = false;
    private bool isTalking = false;

    private void Start()
    {
        if (bubbleText != null)
        {
            bubbleText.text = "";
            bubbleText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!playerInside) return;

        if (Input.GetKeyDown(KeyCode.Space) && !isTalking)
        {
            PlayAppropriateDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = false;
        isTalking = false;

        if (dialogueRoutine != null)
            StopCoroutine(dialogueRoutine);

        if (bubbleText != null)
        {
            bubbleText.text = "";
            bubbleText.gameObject.SetActive(false);
        }
    }

    private void PlayAppropriateDialogue()
    {
        if (dialogueRoutine != null)
            StopCoroutine(dialogueRoutine);

        if (!GameManager.instance.hasTalkedToNPC)
        {
            dialogueRoutine = StartCoroutine(PlayLines(firstTalkLines, true));
        }
        else
        {
            dialogueRoutine = StartCoroutine(PlayLines(repeatTalkLines, false));
        }
    }

    private IEnumerator PlayLines(string[] lines, bool markTutorialCompleteAfter)
    {
        if (bubbleText == null || lines == null || lines.Length == 0)
            yield break;

        isTalking = true;
        bubbleText.gameObject.SetActive(true);

        for (int i = 0; i < lines.Length; i++)
        {
            if (!playerInside)
            {
                isTalking = false;
                yield break;
            }

            bubbleText.text = lines[i];
            yield return new WaitForSeconds(lineDuration);
        }

        bubbleText.text = "";
        bubbleText.gameObject.SetActive(false);
        isTalking = false;

        if (markTutorialCompleteAfter)
        {
            GameManager.instance.MarkNPCTutorialComplete();
        }
    }
}