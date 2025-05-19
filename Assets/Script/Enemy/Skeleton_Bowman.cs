using UnityEngine;

public class Skeleton_Bowman : EnemyCanAttack
{
    protected override Vector3 SetChaseTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                return TargetToPlayer(Vector3.right);
            }
            else
            {
                return TargetToPlayer(Vector3.left);
            }
        }
        else
        {
            if (direction.y > 0)
            {
                return TargetToPlayer(Vector3.up);
            }
            else
            {
                return TargetToPlayer(Vector3.down);
            }
        }
    }
    
    Vector3 TargetToPlayer(Vector3 dir)
    {
        return player.position + dir * controler.EnemyInfo.attackRange;
    }
    protected override void PostAttack()
    {
        controler.pathFinding.agent.isStopped = false;
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > controler.EnemyInfo.attackRange)
        {
            controler.pathFinding.SetTargetDestination(SetChaseTarget());
        }
        else if (distance <= controler.EnemyInfo.attackRange)
        {
            MoveAwayFromPlayer();
        }
    }
    void MoveAwayFromPlayer()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        Vector3 newPos = player.position - dirToPlayer * controler.EnemyInfo.detectRange;
        controler.pathFinding.SetTargetDestination(new Vector3(newPos.x, newPos.y, 0));
    }
}
