using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "BaseStats/PlayerStats")]   
public class EnemyStats : ScriptableObject
{

    public float aggroRange, attackRange, dropRate;
}
