using UnityEngine;
using UnityEngine.EventSystems;

public class move : MonoBehaviour
{
    public CharacterStats characterStats; // tham chiếu đến dữ liệu nhân vật
    public MovementData movementData; // tham chiếu đến dữ liệu di chuyển
    private Rigidbody2D rb; // tham chiếu đến Rigidbody của đối tượng này
    public Animator animator; // tham chiếu đến Animator để điều khiển hoạt ảnh

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponentInParent<Animator>(); // tìm Animator từ cha
    }

    void Awake()
    {
        if (characterStats == null) characterStats = GetComponent<Character>().characterStats;
        if (animator == null) animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // nhận đầu vào từ bàn phím hoặc tay cầm
        float moveY = Input.GetAxis("Vertical"); // nhận đầu vào từ bàn phím hoặc tay cầm
        
        Vector2 movement = new Vector2(moveX, moveY).normalized; // chuẩn hóa đầu vào di chuyển
        Vector2 vector2 = movement * characterStats.baseMoveSpeed;
        Vector2 moveVelocity = vector2;
        rb.linearVelocity = moveVelocity; // đặt vận tốc của Rigidbody dựa trên tốc độ của nhân vật

        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);

            movementData.LastHorizontalInput = movement.x;
            movementData.LastVerticalInput = movement.y;

            animator.SetFloat("Last_Horizontal", movement.x);
            animator.SetFloat("Last_Vertical", movement.y);
            animator.SetFloat("speed", movement.sqrMagnitude);
        }
        else
        {
            animator.SetFloat("speed", 0f);
        }

    }
}
