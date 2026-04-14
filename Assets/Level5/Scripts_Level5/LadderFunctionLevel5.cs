using UnityEngine;

public class LadderFunctionLevel5 : MonoBehaviour
{
    private bool isLadder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Set ladder state when entering ladder trigger
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Reset ladder state when exiting ladder trigger
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
        }
    }
}

