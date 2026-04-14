using UnityEngine;

public class FairyControllerLevel5 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;      // Horizontal movement speed
    [SerializeField] private float jumpForce = 10f;     // Jump strength
    [SerializeField] private float maxMoveSpeed = 10f;  // Maximum speed allowed after speed power-ups

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;         // Position used to test whether the fairy is grounded
    [SerializeField] private float groundCheckRadius = 0.25f; // Radius of the ground check
    [SerializeField] private LayerMask groundLayer;         // Layers that count as ground

    [Header("Ladder")]
    [SerializeField] private float climbSpeed = 8f;   // Speed while climbing ladders
    [SerializeField] private float normalGravity = 4f; // Gravity used when not climbing

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveInput;  // Horizontal input
    private float vertical;     // Vertical input for ladder climbing
    private bool isGrounded;    // True if touching ground
    private bool isLadder;      // True if inside a ladder trigger
    private bool isClimbing;    // True while actively climbing

    [Header("Wing Power-Up")]
    private bool hasWingPower = false; // Tracks whether wings power-up is active
    private int extraJumps;            // Number of extra jumps left
    private int maxExtraJumps = 1;     // Maximum allowed extra jumps

    void Start()
    {
        // Get required components from the fairy object
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Read movement input
        moveInput.x = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Check if the fairy is on the ground
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            // Reset extra jump when landing
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

        // Determine whether the fairy should be climbing
        if (isLadder && Mathf.Abs(vertical) > 0.01f)
            isClimbing = true;
        else if (!isLadder)
            isClimbing = false;

        // Allow jumping only when not climbing
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isClimbing)
        {
            if (isGrounded)
            {
                // Normal jump from the ground
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            else if (hasWingPower && extraJumps > 0)
            {
                // Double jump after collecting the wings power-up
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJumps--;
                Debug.Log("Level 5 double jump used. Remaining extra jumps: " + extraJumps);
            }
        }

        // Flip the fairy sprite to face movement direction
        if (moveInput.x < 0)
            spriteRenderer.flipX = true;
        else if (moveInput.x > 0)
            spriteRenderer.flipX = false;

        // Update animator values
        UpdateAnimationState();
    }

    void FixedUpdate()
    {
        if (isClimbing)
        {
            // Turn off gravity while climbing and move vertically on the ladder
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, vertical * climbSpeed);
        }
        else
        {
            // Restore gravity and apply normal horizontal movement
            rb.gravityScale = normalGravity;
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enter ladder zone
        if (collision.CompareTag("Ladder"))
            isLadder = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Exit ladder zone and stop climbing
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }

    private void UpdateAnimationState()
    {
        if (anim == null) return;

        // Update movement, jumping, and climbing animation states
        anim.SetFloat("MoveX", Mathf.Abs(moveInput.x));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isJumping", !isGrounded && !isClimbing);
        anim.SetBool("isClimbing", isClimbing);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the ground check circle in the Scene view
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    public void ActivateWingPower()
    {
        // Enable wings power-up and allow one extra jump in the air
        hasWingPower = true;
        extraJumps = maxExtraJumps;
        Debug.Log("Wing power activated in Level 5");
    }

    public void ActivateSpeedPower()
    {
        // Increase movement speed, but do not allow it to go over the max speed
        moveSpeed += 1f;
        moveSpeed = Mathf.Min(moveSpeed, maxMoveSpeed);
        Debug.Log("Speed power activated in Level 5. moveSpeed = " + moveSpeed);
    }
}