using UnityEngine;

public class SineMovementLevel4 : MonoBehaviour
{
    [SerializeField] private float amplitude = 3f; // Distance left/right
    [SerializeField] private float frequency = 2f; // Speed of movement

    private Vector2 startPosition;

    void Start()
    {
        // Store the original position of the platform
        startPosition = transform.position;
    }

    void Update()
    {
        float xOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPosition.x + xOffset, startPosition.y);
    }
}
