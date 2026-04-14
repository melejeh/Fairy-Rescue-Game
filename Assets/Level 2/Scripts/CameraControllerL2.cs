using UnityEngine;

public class CameraControllerL2 : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 5f; // How smooth the camera follows

    [Header("Bounds")]
    [SerializeField] private float minX; // Left limit
    [SerializeField] private float maxX; // Right limit
    [SerializeField] private float minY; // Bottom limit
    [SerializeField] private float maxY; // Top limit

    private Transform player;

    void Start()
    {
        // Try to find player automatically at start
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    // This lets the spawner manually assign the player (more reliable)
    public void SetTarget(Transform target)
    {
        player = target;
    }

    void LateUpdate()
    {
        // If player doesn't exist yet, do nothing
        if (player == null) return;

        // Clamp player position within camera bounds
        float targetX = Mathf.Clamp(player.position.x, minX, maxX);
        float targetY = Mathf.Clamp(player.position.y, minY, maxY);

        // Create the final camera position
        Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);

        // Smoothly move camera toward target
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}