using UnityEngine;
using System.Collections;
using Photon.Pun;

public class Attack : MonoBehaviour
{
    public CharacterStats characterStats;
    public move move;
    public Animator animator;
    private Rigidbody2D rb; // tham chiếu đến Rigidbody của đối tượng này
    public bool isAttacking = false;
    public bool canAttack = true;
    PhotonView view;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        view = GetComponentInParent<PhotonView>();

        if (animator == null)
            animator = GetComponentInParent<Animator>(); // tìm Animator từ cha
    }

    void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isAttacking && canAttack)
            {
                canAttack = false; // Ngăn chặn tấn công liên tục
                StartCoroutine(AttackRoutine());
            }
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        // Cập nhật hướng tấn công (animation nhìn đúng hướng)
        animator.SetFloat("Last_Horizontal", move.movementData.LastHorizontalInput);
        animator.SetFloat("Last_Vertical", move.movementData.LastVerticalInput);

        animator.SetBool("isAttacking", true);

        // Không khóa di chuyển — player vẫn move bình thường
        yield return new WaitForSeconds(characterStats.baseAttackCooldown);

        canAttack = true;
    }

    public void EndAttack()
    {
        // Hàm này có thể được gọi từ Animation Event để kết thúc tấn công
       animator.SetBool("isAttacking", false);
       isAttacking = false;
    }
}
