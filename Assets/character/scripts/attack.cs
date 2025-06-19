using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    public CharacterStats characterStats;
    public MovementData movementData;
    public Animator animator;
    public bool isAttacking = false;

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
