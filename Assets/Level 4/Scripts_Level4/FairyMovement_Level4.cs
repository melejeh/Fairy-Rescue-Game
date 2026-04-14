using UnityEngine;

public class FairyMovement_Level4 : MonoBehaviour
{ [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.25f;
    [SerializeField] private LayerMask groundLayer;

    
    // Wing power-up
    private bool hasWingPower = false;
    private bool usedExtraJump = false;
    
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveInput;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");

        if (groundCheck != null)
        {
            Collider2D hit = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            isGrounded = hit != null;

            if (hit != null)
                Debug.Log("Grounded on: " + hit.name);
            else
                Debug.Log("Grounded: false");
        }
        else
        {
            isGrounded = false;
            Debug.LogWarning("No groundCheck assigned on " + gameObject.name);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (moveInput.x < 0)
            spriteRenderer.flipX = true;
        else if (moveInput.x > 0)
            spriteRenderer.flipX = false;

        UpdateAnimationState();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void UpdateAnimationState()
    {
        if (anim == null) return;

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
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    public void ActivateWingPower()
    {
        //hasWingPower = true;
       // usedExtraJump = false;
      //  Debug.Log("Wing power activated: one extra jump unlocked.");
    }
}