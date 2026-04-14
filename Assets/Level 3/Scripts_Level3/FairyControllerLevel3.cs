using UnityEngine;

public class FairyControllerLevel3 : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Ground Settings")]
    public float gravityScale = 3f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;
    
    [Header("Bounds")]
    public Collider2D boundaryCollider;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isFlying;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // start floating
        isFlying = true;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        ClampPosition();
    }
    
    // MOVEMENT
    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal"); // left/right arrows
        float vertical = Input.GetAxis("Vertical");     // up/down arrows

        // flip sprite
        if (horizontal > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x); // ensure facing right
            transform.localScale = scale;
        }
        else if (horizontal < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x); // ensure facing left
            transform.localScale = scale;
        }
        
        if (isFlying)
        {
            // free movement in all directions
            rb.linearVelocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
        }
        else
        {
            // grounded (only move horiz, gravity handles vertical)
            rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);

            // up to start flying again
            if (Input.GetAxis("Vertical") > 0)
            {
                isFlying = true;
                rb.gravityScale = 0f;
            }
        }
    }
    
    void ClampPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, boundaryCollider.bounds.min.x, boundaryCollider.bounds.max.x);
        pos.y = Mathf.Clamp(pos.y, boundaryCollider.bounds.min.y, boundaryCollider.bounds.max.y);
        transform.position = pos;
    }
}