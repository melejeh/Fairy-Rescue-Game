using UnityEngine;

public class FairyController_Level0 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 8f;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void UpdateAnimationState()
    {
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

    public void ActivateWandPower()
    {
        Debug.Log("Activating wand power");
        jumpForce += 2f;
    }

    public void ActivateWingPower()
    {
        Debug.Log("Activating wing power");
        moveSpeed += 1f;
    }
}