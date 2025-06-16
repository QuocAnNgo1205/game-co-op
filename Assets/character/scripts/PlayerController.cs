using UnityEngine;

public class move : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;

    private Vector2 input;
    private Vector2 movement;
    public Animator animator;

    private Vector2 lastMovement = Vector2.down;
    private bool isAttacking = false;


    public float attackCooldown = 0.5f;
    private float attackTimer = 0f;


    void Update()
    {
        attackTimer -= Time.deltaTime;
        // Nếu đang tấn công thì không nhận input di chuyển
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

        // Kiểm tra phím Space để tấn công
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking && attackTimer <= 0f)
        {
            isAttacking = true;
            animator.SetBool("isAttacking", true);
            attackTimer = attackCooldown;
        }

    }

    void FixedUpdate()
    {
        if (!isAttacking)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }

    // Gọi từ Animation Event khi animation tấn công kết thúc
    public void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }
}
