using System.Collections;
using System.Data.Common;
using UnityEngine;

public class FairyController_MainHub : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    private bool hasReachedPortal = false; // prevents moving during animation

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveInput;

    private KeyManager keyManager;
    private NPCDialogue dialogueManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        keyManager = Object.FindFirstObjectByType<KeyManager>();
        dialogueManager = Object.FindFirstObjectByType<NPCDialogue>();
        
        rb.gravityScale = 0f; // so fairy will nt have gravity, but physics will still work since it will be dynamic instead of kinematic
    }

    // Update is called once per frame
    void Update()
    {
        if (hasReachedPortal)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        UpdateAnimationState();

    }

    private void FixedUpdate()
    {
        if (!hasReachedPortal && moveInput != Vector2.zero)
        {
            rb.MovePosition(rb.position + moveInput.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }
    

    private IEnumerator EnterPortalSequence(Vector3 portalPos)
    {
        hasReachedPortal = true;

        float elapsed = 0;
        Vector3 startPos = transform.position;
        while (elapsed < 0.5f)
        {
            transform.position = Vector3.Lerp(startPos, portalPos, elapsed / 0.5f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        anim.SetTrigger("OnPortal");

        yield return new WaitForSeconds(2f);

        Debug.Log("Level Complete! Moving to next world...");
    }


    private void UpdateAnimationState()
    {
        // Flip sprite
        if (moveInput.x != 0)
            GetComponent<SpriteRenderer>().flipX = moveInput.x < 0;
        
        // Animation direction
        anim.SetFloat("MoveX",moveInput.x);
        anim.SetFloat("MoveY",moveInput.y);
    }
}
