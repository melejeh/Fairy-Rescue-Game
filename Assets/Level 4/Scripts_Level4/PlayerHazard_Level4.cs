using System.Collections;
using UnityEngine;

public class PlayerHazard_Level4 : MonoBehaviour
{
    [Header("Respawn")]
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float damageCooldown = 1f;

    [Header("Camera Loss Check")]
    [SerializeField] private CameraMovement_Level4 cameraMovement;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float offscreenBuffer = 0.05f;

    private bool canTakeDamage = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Fallback to main camera if not assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Attempt to get CameraMovement from main camera if not assigned
        if (cameraMovement == null && Camera.main != null)
        {
            cameraMovement = Camera.main.GetComponent<CameraMovement_Level4>();
        }
    }

    void Update()
    {
        if (!canTakeDamage) return;
        if (mainCamera == null) return;

        // Convert world position to viewport space (0–1 range)
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

        // Check if player is outside camera bounds (with buffer)
        bool outOfView =
            viewportPos.x < 0f - offscreenBuffer ||
            viewportPos.x > 1f + offscreenBuffer ||
            viewportPos.y < 0f - offscreenBuffer ||
            viewportPos.y > 1f + offscreenBuffer;

        // Also detects if player is behind the camera
        if (outOfView || viewportPos.z < 0f)
        {
            TakeDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTakeDamage) return;

        // Trigger damage on hazardous objects
        if (other.CompareTag("Water") || other.CompareTag("Coconut"))
        {
            TakeDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canTakeDamage) return;

        // Handle collision-based hazards
        if (collision.gameObject.CompareTag("Coconut"))
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        if (!canTakeDamage) return;

        canTakeDamage = false;

        // Reduce player life
        if (GameManager.instance != null)
        {
            GameManager.instance.LoseLife();
        }

        // Respawn player at designated point
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }

        // Reset physics state
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // Reset camera position
        if (cameraMovement != null)
        {
            cameraMovement.ResetCamera();
        }

        // Start cooldown before player can take damage again
        StartCoroutine(DamageCooldown());
    }

    IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
}