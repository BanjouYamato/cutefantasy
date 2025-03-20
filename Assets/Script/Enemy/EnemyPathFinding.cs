using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathFinding : MonoBehaviour
{   
    public NavMeshAgent agent {  get; private set; }
    Vector3 targetDir;
    public bool leftFace {  get; private set; }
    EnemyControler controler;
    Direction curDir;

    private void Start()
    {   
        controler = GetComponent<EnemyControler>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = controler.EnemyInfo.normalSpeed;
        curDir = Direction.down;
    }

    public Vector3 GetMoveDir()
    {
        return agent.velocity.normalized;
    }
    public Direction DirectionCondition()
    {
        Vector3 direction = agent.velocity.normalized;
        if (direction == Vector3.zero)
        {
            return curDir;
        }
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            curDir = Direction.side;
            Flip(direction.x > 0 ? true : false);
            return curDir;
        }
        if (leftFace)
            Flip(false);
        curDir = direction.y > 0 ? Direction.up : Direction.down;
        return curDir;
    }
    public void Flip(bool val)
    {
        if (val && leftFace)
        {
            leftFace = false;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (!val && !leftFace)
        {
            leftFace = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    public void SetTargetDestination(Vector3 target)
    {
        agent.SetDestination(target);
    }
}
