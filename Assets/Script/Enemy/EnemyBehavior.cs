using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBehavior : MonoBehaviour
{
    protected enum state { Start ,Patrol, Run, Chase, Attack, PostAttack }
    protected state currentState;
    protected Transform player;
    protected bool isWaiting;
    float patrolRadius = 5f;
    float stopDuration = 2f;
    protected float stopDistance = 0.5f;
    protected EnemyControler controler;

    public bool IsAttacking { get; set; }
    public Direction attackDir { get; protected set; }

    private void Start()
    {   
        controler = GetComponent<EnemyControler>();
        player = PlayerControler.instance.transform;
    }
    protected virtual void Update()
    {   
        UpdateStateStatus(Vector3.Distance(transform.position, player.position));
        
    }
    protected abstract void UpdateStateStatus(float distance);
    protected virtual void ChangeState(state newState)
    {
        if (currentState == newState)
            return;
        currentState = newState;
    }
    protected virtual void Patrol()
    {
        StartCoroutine(PatrolRoutine());
    }
    IEnumerator PatrolRoutine()
    {
        while (currentState == state.Patrol)
        {
            WaitDestination();
            yield return null;
        }
    }
    protected void WaitDestination()
    {
        if (!isWaiting && !controler.pathFinding.agent.pathPending
                && controler.pathFinding.agent.remainingDistance <= stopDistance
                )
            StartCoroutine(WaitAtPoint());
    }
    IEnumerator WaitAtPoint()
    {
        isWaiting = true;
        controler.pathFinding.agent.ResetPath();
        controler.pathFinding.agent.isStopped = true; 
        yield return new WaitForSeconds(stopDuration);
        controler.pathFinding.agent.isStopped = false;
        MoveToRandomPoint();
        isWaiting = false;
    }

    protected virtual void Chase() { }

    protected void MoveToRandomPoint()
    {
        Vector3 randomPoint = GetRandomNavMeshPoint(transform.position, patrolRadius);
        if (randomPoint != Vector3.zero)
        {
            controler.pathFinding.agent.SetDestination(randomPoint);
        }
        else
        {
            Debug.LogWarning("Cannot find pos!");
        }
    }
    protected Vector3 GetRandomNavMeshPoint(Vector3 origin, float radius)
    {   
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += origin;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return Vector3.zero;
    }
    public void StopEveryRoutine()
    {
        StopAllCoroutines();
    }
}
