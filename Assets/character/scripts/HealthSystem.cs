using Photon.Pun;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public CharacterStats characterStats;
    public PhotonView photonView;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    public int currentHealth { get; private set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        currentHealth = characterStats.maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!photonView.IsMine) return; // Chỉ xử lý nếu là chủ sở hữu

        int finalDamage = Mathf.Max(0, damage - characterStats.basearmor);
        currentHealth -= finalDamage;
        Debug.Log($"{gameObject.name} nhận {finalDamage} sát thương!");

        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} đã chết!");

        // set animation die
        animator.SetBool("Die", true);

        // Tắt các thành phần không cần thiết
        if (GetComponent<move>() != null)
            GetComponent<move>().enabled = false;

        if (GetComponent<Attack>() != null)
            GetComponent<Attack>().enabled = false;
        StartCoroutine(DestroyAfterDelay(2f));
    }
    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PhotonNetwork.Destroy(gameObject);
    }
    public void Heal(int amount)
    {
        if (!photonView.IsMine) return;
        currentHealth = Mathf.Min(currentHealth + amount, characterStats.maxHealth);
        Debug.Log($"{gameObject.name} hồi {amount} máu!");
    }

    IEnumerator FlashRed()
    {
        if (spriteRenderer == null) yield break;

        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(10);
        }
    }

}
