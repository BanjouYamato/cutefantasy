using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    EnemyControler controler;
    private void Awake()
    {
        controler = GetComponent<EnemyControler>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            StartCoroutine(FixPos());
            player.TakeDamage(controler.EnemyInfo.bodyDamage, transform);
        }
    }
    IEnumerator FixPos()
    {
        controler.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.1f);
        controler.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
