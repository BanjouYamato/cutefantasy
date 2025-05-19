using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : EnemyBehavior
{
    float moveRadius = 2f;
    bool isMovingToTarget;
    protected override void UpdateStateStatus(float distance)
    {
        if (currentState == state.Start)
        {
            if (distance > 0)
            {
                ChangeState(state.Patrol);
            }
            else
            {
                ChangeState(state.Run);
            }
        }
        else if (currentState == state.Patrol
            && distance <= controler.EnemyInfo.detectRange)
        {
            ChangeState(state.Run);
        }
        else if (currentState == state.Run
            && distance > controler.EnemyInfo.detectRange)
        {
            ChangeState(state.Patrol);
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
            case state.Run:
                RunChaos();
                break;
        }
    }
    protected override void Patrol()
    {   
        StopAllCoroutines();
        if (isMovingToTarget)
        {
            isMovingToTarget = false;
        }
        controler.pathFinding.agent.speed = controler.EnemyInfo.normalSpeed;
        base.Patrol();
    }
    void RunChaos()
    {
        StopAllCoroutines();
        if (isWaiting)
        {
            isWaiting = false;
            controler.pathFinding.agent.isStopped = false;
        }
        controler.pathFinding.agent.speed *= 1.5f;
        StartCoroutine(RunRoutine());
    }
    IEnumerator RunRoutine()
    {
        while (currentState == state.Run)
        {   
            MoveToAroundPlayer();
            yield return null;
        }
    }
    void MoveToAroundPlayer()
    {
        if (!isMovingToTarget && !controler.pathFinding.agent.pathPending
                && controler.pathFinding.agent.remainingDistance <= stopDistance)
            StartCoroutine(MoveRandomAroundPlayer());
    }
    IEnumerator MoveRandomAroundPlayer()
    {
        isMovingToTarget = true;
        controler.pathFinding.agent.ResetPath();
        controler.pathFinding.agent.isStopped = true;
        yield return new WaitForEndOfFrame();
        controler.pathFinding.agent.isStopped = false;
        controler.pathFinding.agent.SetDestination(GetRandomPosAroundPlayer());
        isMovingToTarget = false;
    }
    Vector2 GetRandomPosAroundPlayer()
    {
        Vector3 randomOffset = new Vector3(
            Random.Range(-moveRadius, moveRadius),
            0,
            Random.Range(-moveRadius, moveRadius)
        );
        Vector3 randomPosition = player.position + randomOffset;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPosition, out hit, moveRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position;
    }
}
