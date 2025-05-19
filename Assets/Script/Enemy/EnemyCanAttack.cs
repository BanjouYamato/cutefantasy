using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyCanAttack : EnemyBehavior
{

    protected bool attackCD;
    

    protected override void UpdateStateStatus(float distance)
    {   
        if (currentState == state.Start)
        {
            if (distance > controler.EnemyInfo.detectRange)
            {
                ChangeState(state.Patrol);
            }
            else
            {
                ChangeState(state.Chase);
            }
        }
        if (distance <= controler.EnemyInfo.detectRange
            && currentState == state.Patrol)
        {
            ChangeState(state.Chase);
        }
        else if (currentState == state.Chase)
        {
            controler.pathFinding.agent.SetDestination(player.position);
            if (distance <= controler.EnemyInfo.attackRange)
            {
                ChangeState(state.Attack);
            }
            else if (distance > controler.EnemyInfo.detectRange)
            {
                ChangeState(state.Patrol);
            }
        }
        else if (currentState == state.Attack)
        {
            if (attackCD && !IsAttacking)
            {
                ChangeState(state.PostAttack);
            }
        }
        else if (currentState == state.PostAttack
            && !attackCD)
        {
            if (distance > controler.EnemyInfo.detectRange)
            {
                ChangeState(state.Patrol);
            }
            else
            {
                ChangeState(state.Chase);
            }
        }
    }
    protected override void ChangeState(state newState)
    {
        base.ChangeState(newState);
        switch (currentState)
        {
            case state.Patrol:
                Patrol();
                break;
            case state.Chase:
                Chase();
                break;
            case state.Attack:
                Attack();
                break;
            case state.PostAttack:
                PostAttack();
                break;
        }
    }
    protected override void Chase()
    {   
        if (isWaiting)
            isWaiting = false;
        StopAllCoroutines();
        if (controler.pathFinding.agent.isStopped)
            controler.pathFinding.agent.isStopped = false;
        controler.pathFinding.SetTargetDestination(SetChaseTarget());
    }
    protected abstract Vector3 SetChaseTarget();
    protected virtual void Attack()
    {
        controler.pathFinding.agent.isStopped = true;
        attackCD = true;
        IsAttacking = true;
        attackDir = SetDirTargetAttack();
        controler.fsm.ChangeEnemyState(new EnemyAttackState(controler,
            attackDir));
        StartCoroutine(AttackCDRoutine());
    }
    Direction SetDirTargetAttack()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {   
            controler.pathFinding.Flip(direction.x > 0 ? true : false);
            return Direction.side;
        }
        else
        {
            if (direction.y > 0)
            {   
                return Direction.up;
            }
            else
            {
                return Direction.down;
            }
        }
    }
    protected virtual void PostAttack()
    {
        controler.pathFinding.agent.isStopped = false;
        MoveAroundPlayer();
    }
    protected void MoveAroundPlayer()
    {
        float orbitRadius = 3f;
        float randomAngle = Random.Range(0f, 360f);
        Vector3 offset = new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle)) * orbitRadius;
        Vector3 targetPosition = player.position + offset;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, orbitRadius, NavMesh.AllAreas))
        {
            controler.pathFinding.agent.SetDestination(hit.position);
        }
    }
    IEnumerator AttackCDRoutine()
    {
        float attackCD = controler.EnemyInfo.cooldownAttack;
        yield return new WaitForSeconds(attackCD);
        this.attackCD = false;
    }
}
