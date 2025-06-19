using UnityEngine;

public class WarPriest_controller : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 7f;
    public float MaxHealth = 100f;
    public float CurrentHealth { get; private set; }

    private Vector2 input;
    private Vector2 movement;
    public Animator animator;

    private Vector2 lastMovement = Vector2.down;
    private bool isAttacking = false;

    public float attackCooldown = 0.0f;
    private float attackTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        CurrentHealth = MaxHealth; // Initialize current health
        attackTimer = 0f; // Set initial attack timer
    }

    // This function will be called when the character dies
    void Die()
    {
        // Perform actions when the character dies, e.g., show a message, reset the game, etc.
        Debug.Log("War Priest has died!");
        // Additional logic to reset position or reload the scene can be added here
    }

    private void TakeDamage()
    {
        CurrentHealth -= 10f; // Reduce health when taking damage
        if (CurrentHealth <= 0f)
        {
            Die(); // Call Die if health <= 0
        }
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;
        // If attacking, do not accept movement input
        if (!isAttacking)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            movement = input.normalized;

            if (input != Vector2.zero)
            {
                lastMovement = input;

                animator.SetFloat("Horizontal", input.x);
                animator.SetFloat("Vertical", input.y);
            }

            animator.SetFloat("Last_Horizontal", lastMovement.x);
            animator.SetFloat("Last_Vertical", lastMovement.y);
            animator.SetFloat("speed", input.sqrMagnitude);
        }

        // Check for Space key to attack
        if (Input.GetKeyDown(KeyCode.F) && !isAttacking && attackTimer <= 0f)
        {
            isAttacking = true;
            animator.SetBool("isAttacking", true);
            attackTimer = attackCooldown;
        }
    }

    public void FixedUpdate()
    {
        if (!isAttacking)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }

    // Called from Animation Event when attack animation ends
    public void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }
}
