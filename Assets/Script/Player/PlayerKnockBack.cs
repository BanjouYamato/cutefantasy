using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockBack : MonoBehaviour
{   
    public bool KnockBack { get; private set; }
    float pushForce = 10f;
    PlayerControler controler;
    private void Start()
    {
        controler = GetComponent<PlayerControler>();
    }
    public void GetKnockBack(Transform damageSource)
    {
        KnockBack = true;
        Vector2 pushDirect = (transform.position - damageSource.position).normalized;
        controler.Rigidbody2D.AddForce(pushDirect * pushForce * controler.Rigidbody2D.mass, 
            ForceMode2D.Impulse);
        controler.PlayerStateMachine.ChangeState(new HitState(controler, controler.PlayerStats.dir));
        StartCoroutine(KnockBackRoutine());
    }
    IEnumerator KnockBackRoutine()
    {
        float knockBackDuration = 0.1f;
        yield return new WaitForSeconds(knockBackDuration);
        controler.Rigidbody2D.velocity = Vector2.zero;
        KnockBack = false;
        controler.Player.DetecDeath();
    }
}
