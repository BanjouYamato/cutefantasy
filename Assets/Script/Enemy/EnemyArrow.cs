using UnityEngine;

public class EnemyArrow : Arrow
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyControler>() == null)
        {
            Destroy(gameObject);
        }
    }
}
