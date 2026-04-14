using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // To stop game when player dies
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0f;
        }
    }
}