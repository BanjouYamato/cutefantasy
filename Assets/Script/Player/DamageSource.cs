using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] 
    WeaponItemSO weapon;
    [SerializeField]
    AudioClip hitSFX;
    public WeaponItemSO Weapon { get { return weapon; } }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyData enemyData = collision.GetComponent<EnemyData>();
        if (enemyData != null )
        {
            SoundManager.Instance.PlayOS(hitSFX);
            enemyData.TakeDamage(weapon.Damage);
        }
    }
}
