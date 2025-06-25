using UnityEngine;

public class Orc : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;
    public float changeDirectionTime = 5f; // thời gian đổi hướng
    private Vector2 direction;
    private float timer;

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PickNewDirection();
        timer = 0f;
    }

    [System.Obsolete]
    private void FixedUpdate()
    {
        // Di chuyển
        rb.velocity = direction * speed;

        // Animator
        bool isWalking = rb.velocity.magnitude > 0.01f;
        animator.SetBool("isWalking", isWalking);
        if (isWalking)
        {
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);
        }

        // Đếm thời gian
        timer += Time.fixedDeltaTime;
        if (timer >= changeDirectionTime)
        {
            PickNewDirection();
            timer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            animator.SetTrigger("hurt");

            // Tính hướng knockback
            Vector2 knockbackDirection = (transform.position - collider.transform.position).normalized;

            transform.position += (Vector3)knockbackDirection * 0.1f; // lùi nhẹ 0.1 đơn vị
        }
    }

    [System.Obsolete]
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Wall"))
        {
            timer = 0f;
            direction = -direction;

            // Reset vận tốc để không "xuyên" tường
            rb.velocity = Vector2.zero;
        }
    }

    private void PickNewDirection()
    {
        Vector2[] directions = new Vector2[]
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };
        direction = directions[Random.Range(0, directions.Length)];
    }
}
