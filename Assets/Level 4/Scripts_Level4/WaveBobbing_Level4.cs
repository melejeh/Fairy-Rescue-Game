using UnityEngine;

public class WaveBobbing_Level4 : MonoBehaviour
{
    [SerializeField] private float verticalAmplitude = 0.15f;
    [SerializeField] private float horizontalAmplitude = 0.1f;

    [SerializeField] private float speed = 5f; // Controls oscillation speed

    private Vector3 startPos;

    void Start()
    {
        // Store initial position
        startPos = transform.position;
    }

    void Update()
    {
        // Scale time for movement speed
        float time = Time.time * speed;

        // Calculate offsets using sine and cosine for smooth motion
        float yOffset = Mathf.Sin(time) * verticalAmplitude;
        float xOffset = Mathf.Cos(time * 0.7f) * horizontalAmplitude;

        // Apply offset to starting position
        transform.position = startPos + new Vector3(xOffset, yOffset, 0f);
    }
}