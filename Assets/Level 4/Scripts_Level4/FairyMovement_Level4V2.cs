using UnityEngine;

public class FairyMovement_Level4V2 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;   // Horizontal movement speed
    [SerializeField] private float jumpForce = 10f;  // Strength of each jump

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;        // Point used to check if player is standing on ground
    [SerializeField] private float groundCheckRadius = 0.25f; // Size of the ground check circle
    [SerializeField] private LayerMask groundLayer;        // Which layers count as ground

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveInput;  // Stores player movement input
    private bool isGrounded;    // True when player is touching the ground

    [Header("Wing Power-Up")]
    private bool hasWingPower = false; // Tracks whether the wings power-up has been collected
    private int extraJumps;            // Number of extra jumps currently available
    private int maxExtraJumps = 1;     // Maximum number of extra jumps allowed

    void Start()
    {
        // Get important components attached to the fairy
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Read horizontal movement input
        moveInput.x = Input.GetAxisRaw("Horizontal");

        // Check if the fairy is touching the ground
        if (groundCheck != null)
        {
            Collider2D hit = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            isGrounded = hit != null;
        }
        else
        {
            // Fail safely if no ground check object is assigned
            isGrounded = false;
            Debug.LogWarning("No groundCheck assigned on " + gameObject.name);
        }

        // Reset extra jumps whenever the player lands
        if (isGrounded && hasWingPower)
        {
            extraJumps = maxExtraJumps;
        }

        // Handle normal jump and double jump
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                // Normal jump from the ground
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            else if (hasWingPower && extraJumps > 0)
            {
                // Extra jump in the air after collecting wings power-up
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJumps--;
                Debug.Log("Level 4 double jump used. Remaining extra jumps: " + extraJumps);
            }
        }

        // Flip the fairy sprite so it faces the direction of movement
        if (moveInput.x < 0)
            spriteRenderer.flipX = true;
        else if (moveInput.x > 0)
            spriteRenderer.flipX = false;

        // Update animation parameters
        UpdateAnimationState();
    }

    void FixedUpdate()
    {
        // Apply horizontal movement every physics frame
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void UpdateAnimationState()
    {
        if (anim == null) return;

        // Update only the parameters that exist in the Animator
        foreach (AnimatorControllerParameter param in anim.parameters)
        {
            if (param.name == "MoveX")
                anim.SetFloat("MoveX", Mathf.Abs(moveInput.x));

            if (param.name == "isGrounded")
                anim.SetBool("isGrounded", isGrounded);

            if (param.name == "isJumping")
                anim.SetBool("isJumping", !isGrounded);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the ground check area in the Scene view for debugging
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    public void ActivateWingPower()
    {
        // Enable double jump power-up and reset available extra jumps
        hasWingPower = true;
        extraJumps = maxExtraJumps;
        Debug.Log("Wing power activated in Level 4");
    }
}