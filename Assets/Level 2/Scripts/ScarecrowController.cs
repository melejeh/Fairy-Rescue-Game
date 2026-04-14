using UnityEngine;

public class ScarecrowController : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public GameObject projectilePrefab;
    public Transform throwPoint;

    [Header("Attack Settings")]
    public float throwRange = 4f;
    public float throwForce = 8f;
    public float fireRate = 2f;

    [Header("Spread Shot")]
    public int mushroomCount = 3;
    public float spreadAngle = 15f;

    private float nextFireTime;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        nextFireTime = Time.time + fireRate;
    }

    void Update()
    {
        // If player is missing, keep trying to find the spawned player by tag
        if (player == null || !player.activeInHierarchy)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) return;
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (player.transform.position.x < transform.position.x)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;

        if (distance <= throwRange && Time.time >= nextFireTime)
        {
            ThrowProjectiles();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ThrowProjectiles()
    {
        if (projectilePrefab == null || throwPoint == null || player == null) return;

        Vector2 baseDirection = (player.transform.position - throwPoint.position).normalized;

        if (mushroomCount <= 1)
        {
            SpawnMushroom(baseDirection);
            return;
        }

        float startAngle = -spreadAngle * 0.5f;
        float angleStep = spreadAngle / (mushroomCount - 1);

        for (int i = 0; i < mushroomCount; i++)
        {
            float currentAngle = startAngle + (angleStep * i);
            Quaternion rotation = Quaternion.Euler(0, 0, currentAngle);
            Vector2 finalDirection = rotation * baseDirection;

            SpawnMushroom(finalDirection);
        }
    }

    void SpawnMushroom(Vector2 direction)
    {
        GameObject mushroom = Instantiate(projectilePrefab, throwPoint.position, Quaternion.identity);

        Rigidbody2D rb = mushroom.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * throwForce;
        }
    }
}