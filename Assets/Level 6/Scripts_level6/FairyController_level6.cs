using System.Collections;
using UnityEngine;

public class FairyController_level6 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;      // Horizontal movement speed
    [SerializeField] private float jumpForce = 10f;     // Jump force
    [SerializeField] private float maxMoveSpeed = 10f;  // Maximum speed after speed boosts

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveInput;          // Stores horizontal movement input
    private SpriteRenderer spriteRenderer;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;         // Position used to test if the fairy is on the ground
    [SerializeField] private float groundCheckRadius = 0.2f; // Radius of the ground detection circle
    [SerializeField] private LayerMask groundLayer;         // Layers considered valid ground
    private bool isGrounded;

    [Header("Wing Power-Up")]
    private bool hasWingPower = false; // True once wing power-up is collected
    private int extraJumps;            // Number of extra jumps remaining
    private int maxExtraJumps = 1;     // Maximum extra jumps allowed

    [Header("Health")]
    [SerializeField] private int maxLives = 5;    // Maximum lives for the player
    private int currentLives;                     // Current remaining lives
    private bool canTakeDamage = true;            // Prevents repeated damage too quickly
    [SerializeField] private float damageCooldown = 1f; // Delay between damage hits

    void Start()
    {
        // Get components attached to the fairy object
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Start the player with max lives
        currentLives = maxLives;
    }

    void Update()
    {
        // Read horizontal movement input
        moveInput.x = Input.GetAxisRaw("Horizontal");

        // Check whether the fairy is standing on the ground
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            // Reset extra jump count after landing
            if (isGrounded && hasWingPower)
            {
                extraJumps = maxExtraJumps;
            }
        }
        else
        {
            isGrounded = false;
            Debug.LogWarning("No groundCheck assigned on " + gameObject.name);
        }

        // Handle normal jump and double jump
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                // Standard jump
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            else if (hasWingPower && extraJumps > 0)
            {
                // Extra jump from wing power-up
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJumps--;
                Debug.Log("Level 6 double jump used. Remaining extra jumps: " + extraJumps);
            }
        }

        // Flip the fairy to face left or right
        if (moveInput.x < 0)
            spriteRenderer.flipX = true;
        else if (moveInput.x > 0)
            spriteRenderer.flipX = false;

        // Update animations
        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        // Apply horizontal movement every physics frame
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy coins when collected by the fairy
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void UpdateAnimationState()
    {
        if (anim == null) return;

        // Update animation direction based on horizontal movement
        if (moveInput.x > 0)
        {
            anim.SetFloat("MoveX", 1);
            anim.SetFloat("MoveY", 0);
        }
        else if (moveInput.x < 0)
        {
            anim.SetFloat("MoveX", -1);
            anim.SetFloat("MoveY", 0);
        }
        else
        {
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", 0);
        }
    }

    public void ActivateWingPower()
    {
        // Enable double jump power-up
        hasWingPower = true;
        extraJumps = maxExtraJumps;
        Debug.Log("Wing power activated in Level 6");
    }

    public void ActivateSpeedPower()
    {
        // Increase movement speed, but clamp it so it does not exceed the limit
        moveSpeed += 1f;
        moveSpeed = Mathf.Min(moveSpeed, maxMoveSpeed);
        Debug.Log("Speed power activated in Level 6. moveSpeed = " + moveSpeed);
    }

    public void ActivateHeartPower()
    {
        // Placeholder for heart power-up behavior
        Debug.Log("Activating heart power");
    }

    private void Die()
    {
        // Stop movement and disable the script when the fairy dies
        Debug.Log("Fairy Died");

        rb.linearVelocity = Vector2.zero;
        this.enabled = false;

        anim.SetTrigger("Dying");

        // Load game over scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

    private void OnDrawGizmosSelected()
    {
        // Show ground check radius in the Scene view
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}