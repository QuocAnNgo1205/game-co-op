using UnityEngine;

[CreateAssetMenu(fileName = "New Character Stats", menuName = "Character/Stats")]
public class CharacterStats : ScriptableObject
{
    public int maxHealth;//máu cơ bản
    public int basedamage;// sát thương cơ bản
    public int basearmor;// giáp cơ bản
    public float baseMoveSpeed;// tốc độ di chuyển cơ bản
    public float baseAttackCooldown;// tốc đánh cơ bản
    public float baseAttackRange;// tầm đánh cơ bản (chỉ dùng cho AI của quái và tầm bắn của cung)
}
