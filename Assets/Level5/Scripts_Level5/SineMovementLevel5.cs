using UnityEngine;


public class SineMovementLevel5 : MonoBehaviour
{
    [SerializeField] private float amplitude = 7f; // The height of the movement
    [SerializeField] private float frequency = 6f; // The speed of the movement
    
    
    private Vector2 startPosition;
    
    
    void Start()
    {
        // Store the original position of the platform
        startPosition = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPosition.x, startPosition.y + yOffset);
    }
}
