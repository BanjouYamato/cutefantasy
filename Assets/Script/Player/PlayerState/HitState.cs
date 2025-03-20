using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : ActionState
{
    public HitState(PlayerControler controler, Direction direction) : base(controler, direction)
    {
    }

    public override void EnterState()
    {
        controler.PlayerStateMachine.PlayAnimation($"HitState_{currentDirect}", 0);
        count = 0;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        count += Time.deltaTime;
        if (count > 0.2f)
        {
            controler.PlayerStateMachine.ChangeState(new IdleState(controler, currentDirect));
        }
    }

    protected override ActionState CreateNewState()
    {
        return new HitState(controler, currentDirect);
    }
}
