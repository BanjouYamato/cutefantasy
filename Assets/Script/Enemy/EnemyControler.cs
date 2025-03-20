using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    [SerializeField]
    EnemyScriptable enemyInfo;

    public EnemyScriptable EnemyInfo
    {
        get { return enemyInfo; }
    }
    public Rigidbody2D rb { get; private set; }
    public EnemyData data { get; private set; }
    public EnemyPathFinding pathFinding { get; private set; }
    public EnemyBehavior behavior { get; private set; }
    public EnemyStateMachine fsm { get; private set; }
    public EnemyKnockBack knockBack { get; private set; }
    public CapsuleCollider2D capsuleCollider { get; private set; }

    private void Awake()
    {   
        rb = GetComponent<Rigidbody2D>();
        data = GetComponent<EnemyData>();
        behavior = GetComponent<EnemyBehavior>();
        pathFinding = GetComponent<EnemyPathFinding>();
        fsm = GetComponent<EnemyStateMachine>();
        knockBack = GetComponent<EnemyKnockBack>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
}
