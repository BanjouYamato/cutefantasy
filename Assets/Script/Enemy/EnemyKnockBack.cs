using System.Collections;
using UnityEngine;

public class EnemyKnockBack : MonoBehaviour
{
    EnemyControler controler;
    public bool KnockBack {  get; private set; }
    float pushForce = 15f;
    float knockBackDuration = 0.1f;

    private void Start()
    {
        controler = GetComponent<EnemyControler>();
    }
    public void GetKnockBack(Transform damageSource)
    {
        KnockBack = true;
        Vector2 pushDirect = (transform.position - damageSource.position).normalized;
        controler.rb.AddForce(pushDirect * pushForce * controler.rb.mass, ForceMode2D.Impulse);
        controler.fsm.ChangeEnemyState(new EnemyHitState(controler, controler.pathFinding.DirectionCondition()));
        StartCoroutine(KnockBackRoutine());
    }
    IEnumerator KnockBackRoutine()
    {
        yield return new WaitForSeconds(knockBackDuration);
        controler.rb.velocity = Vector2.zero;
        KnockBack = false;
        controler.data.DetecDeath();
    }
}
