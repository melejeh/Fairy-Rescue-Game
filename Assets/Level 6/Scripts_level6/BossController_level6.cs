using UnityEngine;

public class BossController_level6 : MonoBehaviour
{
    [Header("Movement")] [SerializeField] private float moveSpeed = 1f;

    //[SerializeField] private float jumpForce = 10f;
    [SerializeField] private float leftBound = 43f;
    [SerializeField] private float rightBound = 67f;

    [Header("Combat")] [SerializeField] private float attackRange = 3f;

    [SerializeField] private float attackCooldown = 5f;

    //[SerializeField] private int numberOfAttackAnimations = 2;
    private float lastAttackTime;

    private Transform player;
    private Rigidbody2D rb;

    //private Animator anim;
    private SpriteRenderer spriteRenderer;
    
    public void SetPlayer(Transform newPlayer)
    {
        player = newPlayer;
        Debug.Log("Boss now targeting: " + newPlayer.name);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null) return;
        
        TryAttack();
    }

    void FixedUpdate()
    {
        if (player == null) return;
        Move();

    }

    private void Move()
    {
        float direction = Mathf.Sign(player.position.x - transform.position.x);

        // Clamp movement inside bounds
        if ((direction < 0 && transform.position.x <= leftBound) ||
            (direction > 0 && transform.position.x >= rightBound))
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            //anim.SetFloat("Speed", 0);
            return;
        }

        // Move toward player
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        //Debug.Log("Boss moving");

        //anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

        //Flip sprite
        if (direction < 0)
            spriteRenderer.flipX = true;
        else if (direction > 0)
            spriteRenderer.flipX = false;
    }

    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        //Debug.Log("Distance to player: " + distanceToPlayer);

        // Proper cooldown enforcement
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        lastAttackTime = Time.time;

        Debug.Log("Boss Attack"); //i dont see this

        // Random attack animation from 1 to N
        //int attackIndex = Random.Range(1, numberOfAttackAnimations + 1);
        //anim.SetTrigger("Attack" + attackIndex);

        PlayerDamage damageScript = player.GetComponent<PlayerDamage>();

        if (damageScript != null)
        {
            damageScript.TakeDamage(1, false);
        }
    }
}