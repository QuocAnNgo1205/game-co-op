using UnityEngine;

public class move : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;

    private Vector2 input;
    private Vector2 movement;
    public Animator animator;

    private Vector2 lastMovement = Vector2.down;

    void Update()
    {
        // Lấy input từ bàn phím
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        // Chuẩn hóa vector để tránh tăng tốc khi đi chéo
        movement = input.normalized;

        // Nếu đang di chuyển, cập nhật hướng và animation
        if (input != Vector2.zero)
        {
            lastMovement = input;

            animator.SetFloat("Horizontal", input.x);
            animator.SetFloat("Vertical", input.y);
        }

        // Luôn cập nhật hướng idle
        animator.SetFloat("Last_Horizontal", lastMovement.x);
        animator.SetFloat("Last_Vertical", lastMovement.y);

        // Gán tốc độ để điều khiển chuyển trạng thái
        animator.SetFloat("speed", input.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
