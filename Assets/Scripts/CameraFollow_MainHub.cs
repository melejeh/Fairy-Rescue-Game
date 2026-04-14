using UnityEngine;

public class CameraFollow_MainHub : MonoBehaviour
{
    public Transform target;      // the player/fairy
    public float smoothSpeed = 0.125f;
    public Collider2D bounds;     // assign your CameraBounds (any 2D collider works)
    private float zOffset = -10f;

    private float camHalfHeight;
    private float camHalfWidth;

    void Start()
    {
        camHalfHeight = Camera.main.orthographicSize;
        camHalfWidth = camHalfHeight * Camera.main.aspect;
    }

    void LateUpdate()
    {
        if (target == null || bounds == null) return;

        Vector3 desiredPos = new Vector3(target.position.x, target.position.y, zOffset);

        // Get the bounds from the collider
        float minX = bounds.bounds.min.x + camHalfWidth;
        float maxX = bounds.bounds.max.x - camHalfWidth;
        float minY = bounds.bounds.min.y + camHalfHeight;
        float maxY = bounds.bounds.max.y - camHalfHeight;

        float clampedX = Mathf.Clamp(desiredPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPos.y, minY, maxY);

        Vector3 clampedPos = new Vector3(clampedX, clampedY, zOffset);
        transform.position = Vector3.Lerp(transform.position, clampedPos, smoothSpeed);
    }
}