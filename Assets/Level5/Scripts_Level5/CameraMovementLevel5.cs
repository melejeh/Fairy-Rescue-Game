using UnityEngine;

public class CameraMovementLevel5 : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 5f;

    private Transform player;

    // Assign the target the camera should follow
    public void SetTarget(Transform newTarget)
    {
        player = newTarget;
    }

    void LateUpdate()
    {
        // Ensure a target is assigned
        if (player == null) return;

        // Maintain camera Z position while following player X and Y
        Vector3 targetPosition = new Vector3(
            player.position.x,
            player.position.y,
            transform.position.z
        );

        // Smoothly interpolate toward the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}