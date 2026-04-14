using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Only respond to player
        if (!other.CompareTag("Player")) return;

        // Deposit first collected key if available
        if (GameManager.instance != null && GameManager.instance.keysCollected.Count > 0)
        {
            string keyToDeposit = GameManager.instance.keysCollected[0];
            GameManager.instance.DepositKey(keyToDeposit);
        }
        else
        {
            Debug.Log("No keys to deposit.");
        }
    }
}