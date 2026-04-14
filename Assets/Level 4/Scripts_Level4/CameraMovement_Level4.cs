using UnityEngine;

public class CameraMovement_Level4 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float startDelay = 3f;

    [Header("Bounds")]
    [SerializeField] private float maxX;

    private float timer = 0f;
    private bool canMove = false;
    private Vector3 startPosition;

    void Start()
    {
        // Store initial camera position for reset
        startPosition = transform.position;
    }

    void Update()
    {
        // Track elapsed time
        timer += Time.deltaTime;

        // Enable movement after delay
        if (!canMove && timer >= startDelay)
        {
            canMove = true;
        }

        // Move camera right until reaching maxX
        if (canMove && transform.position.x < maxX)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }

    public void ResetCamera()
    {
        // Reset position and movement state
        transform.position = startPosition;
        timer = 0f;
        canMove = false;
    }
}