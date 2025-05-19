using UnityEngine;

public class EnemyDamageSource : MonoBehaviour
{
    [SerializeField] EnemyScriptable enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(enemy.attackDamage, transform.root);
        }
    }
}
