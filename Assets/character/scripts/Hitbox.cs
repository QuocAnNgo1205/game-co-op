using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private move move;
    private Attack attack; // Tham chiếu đến script Attack nếu cần thiết
    private Transform hitboxTransform; // Transform của hitbox, nếu cần thiết
    private float xpos = 0f;
    private float ypos = -2.25f;
    public float xRange = 1.1f; // Chiều rộng của hitbox, nếu cần thiết
    public float yRange = 1.5f; // Chiều cao của hitbox, nếu cần thiết
    private BoxCollider2D box;

    void Start()
    {
        move = GetComponentInParent<move>();
        attack = GetComponentInParent<Attack>();
        if (move == null)
        {
            Debug.LogError("Move component not found in parent object.");
        }
        hitboxTransform = GetComponent<Transform>();
        box = GetComponent<BoxCollider2D>();
        box.enabled = false; // Tắt hitbox ban đầu
    }

    // Update is called once per frame
    void Update()
    {
        xpos = move.movementData.LastHorizontalInput * xRange;
        ypos = move.movementData.LastVerticalInput * yRange - 0.2f;
        if (hitboxTransform != null)
        {
            // Cập nhật vị trí hitbox dựa trên hướng di chuyển
            // Cập nhật rotation hitbox dựa trên hướng di chuyển
            hitboxTransform.SetLocalPositionAndRotation(new Vector3(xpos, ypos, 0f), Quaternion.Euler(0f, 0f, Mathf.Atan2(move.movementData.LastVerticalInput, move.movementData.LastHorizontalInput) * Mathf.Rad2Deg));
        }
        else
        {
            Debug.LogError("Hitbox Transform not found.");
        }
        if (attack.isAttacking)
        {
            box.enabled = true; // Kích hoạt hitbox khi đang tấn công
        } 
        else
        {
            box.enabled = false; // Tắt hitbox khi không tấn công
        }
    }

}
