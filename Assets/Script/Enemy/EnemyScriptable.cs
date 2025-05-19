using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "New Enemy")]
public class EnemyScriptable : ScriptableObject
{
    public int hp;
    public int bodyDamage;
    public int attackDamage;
    public string animName;
    public float duration;
    public float normalSpeed;
    public float detectRange;
    public float attackRange;
    public float cooldownAttack;
}
