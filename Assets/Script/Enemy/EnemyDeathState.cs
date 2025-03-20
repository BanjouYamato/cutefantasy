using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    public EnemyDeathState(EnemyControler controler, Direction direction) 
        : base(controler, direction)
    {
    }

    public override void EnterState()
    {
        controler.fsm.PlayAnimation($"{controler.EnemyInfo.animName}_death");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (!controler.data.Death)
        {
            controler.fsm.ChangeEnemyState(new EnemyIdleState(controler, currentDirection));
        }
    }

    protected override EnemyBaseState GetState()
    {
        return new EnemyDeathState(controler, currentDirection);
    }
}
