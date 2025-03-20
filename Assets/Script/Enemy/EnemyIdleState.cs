using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyControler controler, Direction direction) 
        : base(controler, direction)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        controler.fsm.PlayAnimation($"{controler.EnemyInfo.animName}_Idle_{currentDirection}");
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (controler.pathFinding.GetMoveDir() != Vector3.zero)
        {
            controler.fsm.ChangeEnemyState(new EnemyMoveState(controler, currentDirection));
        }
    }
    protected override EnemyBaseState GetState()
    {
        return new EnemyIdleState(controler, currentDirection);
    }
}
