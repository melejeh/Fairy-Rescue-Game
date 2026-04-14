using UnityEngine;

public class RandomCoconutDrop_Level4 : MonoBehaviour
{
    [SerializeField] private float minDropTime = 1f;
    [SerializeField] private float maxDropTime = 3f;
    [SerializeField] private float horizontalForce = 350f;
    [SerializeField] private float torqueAmount = 5f;

    [Header("Activation")]
    [SerializeField] private float activationDistance = 10f;

    private Rigidbody2D rb;
    private float dropTimer = 0f;
    private float timeToDrop = 0f;

    private bool hasDropped = false;
    private bool isActivated = false;

    private Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Ensure Rigidbody2D is present
        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D on coconut: " + gameObject.name);
            return;
        }

        // Disable physics until activation
        rb.simulated = false;

        // Randomize drop delay
        timeToDrop = Random.Range(minDropTime, maxDropTime);

        // Cache main camera transform
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogError("Camera.main is null. Ensure the camera has the MainCamera tag.");
        }
    }

    void Update()
    {
        if (hasDropped || rb == null) return;
        if (cameraTransform == null) return;

        // Activate when within horizontal distance of camera
        if (!isActivated)
        {
            float distanceX = Mathf.Abs(transform.position.x - cameraTransform.position.x);

            if (distanceX <= activationDistance)
            {
                isActivated = true;
                dropTimer = 0f;
            }
            else
            {
                return;
            }
        }

        // Count time after activation
        dropTimer += Time.deltaTime;

        if (dropTimer >= timeToDrop)
        {
            DropCoconut();
        }
    }

    void DropCoconut()
    {
        hasDropped = true;

        // Enable physics and apply random motion
        rb.simulated = true;

        float randomX = Random.Range(-horizontalForce, horizontalForce);
        rb.AddForce(new Vector2(randomX, 0f));
        rb.AddTorque(Random.Range(-torqueAmount, torqueAmount));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Apply damage if player has PlayerDamage component
        PlayerDamage damage = other.GetComponent<PlayerDamage>();
        if (damage != null)
        {
            damage.TakeDamage(1, true);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Damage player on collision
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerDamage damage = collision.gameObject.GetComponent<PlayerDamage>();
            if (damage != null)
            {
                damage.TakeDamage(1, true);
            }

            Destroy(gameObject);
            return;
        }

        // Destroy on ground impact
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}