using UnityEngine;

public class CameraResettable : MonoBehaviour
{
    private Vector3 startPosition;

    void Start()
    {
        // Store initial camera position
        startPosition = transform.position;
    }

    // Reset camera to its original position
    public void ResetCamera()
    {
        transform.position = startPosition;
    }
}