using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [SerializeField] private AudioClip coinSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only respond to player
        if (!other.CompareTag("Player")) return;

        // Update coin count
        if (GameManager.instance != null)
        {
            GameManager.instance.CollectCoin();
        }

        // Play collection sound if assigned
        if (coinSound != null)
        {
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
        }

        // Remove coin from scene
        Destroy(gameObject);
    }
}