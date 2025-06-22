using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    public CharacterStats characterStats;
    public MovementData movementData;
    public Animator animator;
    private Rigidbody2D rb; // tham chiếu đến Rigidbody của đối tượng này
    public bool isAttacking = false;

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


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        // Cập nhật hướng tấn công (animation nhìn đúng hướng)
        animator.SetFloat("LastHorizontal", movementData.LastHorizontalInput);
        animator.SetFloat("LastVertical", movementData.LastVerticalInput);

        animator.SetBool("isAttacking", true);

        // Không khóa di chuyển — player vẫn move bình thường
        yield return new WaitForSeconds(characterStats.baseAttackCooldown);

        animator.SetBool("isAttacking", false);
        isAttacking = false;
    }
}
