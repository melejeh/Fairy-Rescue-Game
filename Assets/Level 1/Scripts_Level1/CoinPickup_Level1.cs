using UnityEngine;
using TMPro;

public class CoinPickup_Level1 : MonoBehaviour
{
    public TMP_Text scoreText;
    public AudioClip coinSound;
    private int coin = 0;

    void Start()
    {
        scoreText.text = "Coins: 0";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coin++;
            scoreText.text = "Coins: " + coin;
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
            Destroy(other.gameObject); // Destroy coin when picked up by player
        }
    }
}
