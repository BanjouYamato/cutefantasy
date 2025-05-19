using System.Collections;
using UnityEngine;

public class Cid : BasePartner
{
    [SerializeField] float _normalAttackCd;   
    
    float _count, _animTime;
    bool _isAttacking;
    bool _canAttack = true;
    Vector3 _attackDir;
    protected override void EnterAttack()
    {
        _canMove = false;
        _isAttacking = true;
        _anim.SetTrigger("Stab");
        _anim.SetFloat("Horizontal", _dir.x);
        _anim.SetFloat("Vertical", _dir.y);
        _animTime = _anim.GetCurrentAnimatorStateInfo(0).length;
        _count = 0;
        _attackDir = AbsDir(_lastMoveDir);
        HitBoxAttack(_attackDir, _attackRange, _normalAttackRadius);
    }

    protected override void UpdateAttack()
    {
        _count += Time.deltaTime;
        if (_count >= _animTime)
        {
            ChangeState(PartnerState.Idle);
        }
    }

    protected override void ExitAttack()
    {
        _canMove = true;
        _isAttacking = false;
        StartCoroutine(NormalAttackCD());

        if (_enemyManager.enemyDatas.Count == 0)
        {
            _target = _player.transform;
            return;
        }
        if (!_enemyManager.enemyDatas.Contains(_currentEnemy))
        {
            _currentEnemy = CheckNearestEnemy(_enemyManager.enemyDatas);
        }
            
    }

    protected override void CheckAttack()
    {
        if (!_enemyManager.enemyDatas.Contains(_currentEnemy))
        {
            _currentEnemy = CheckNearestEnemy(_enemyManager.enemyDatas);
        }
        if (_target != _currentEnemy.transform)
            _target = _currentEnemy.transform;
        var distance = Vector3.Distance(attackPoint.position, _target.position);
        Vector2 dirToPlayer = (_target.position - attackPoint.position).normalized;
        float dot = Vector2.Dot(AbsDir(_dir), dirToPlayer);

        if (!_isAttacking && _canAttack && distance <= _attackRange && dot > 0.7f)
        {   
            ChangeState(PartnerState.Attack);
        }
    }
    IEnumerator NormalAttackCD()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_normalAttackCd);
        _canAttack = true;
    }
    
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position + _attackDir * _attackRange, _normalAttackRadius);
        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, _dir * 1f);

    }
}
