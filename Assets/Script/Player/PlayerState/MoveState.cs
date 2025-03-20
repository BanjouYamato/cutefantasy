using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : ActionState
{
    public MoveState(PlayerControler controler, Direction direction) : base(controler, direction) { }

    protected override ActionState CreateNewState()
    {
        return new MoveState(controler, currentDirect);
    }
    public override void EnterState()
    {
        base.EnterState();
        count = 0;
        SoundManager.Instance.PlayOS(controler.PlayerMovement.moveSFX);
    }
    public override void UpdateState()
    {   
        base.UpdateState();
        count += Time.deltaTime;
        if (count >= 0.5f)
        {
            SoundManager.Instance.PlayOS(controler.PlayerMovement.moveSFX);
            count = 0;
        }
        if (controler.PlayerMovement.movement == Vector2.zero)
        {   
            controler.PlayerStateMachine.ChangeState(new IdleState(controler, currentDirect));
        }
    }
}
