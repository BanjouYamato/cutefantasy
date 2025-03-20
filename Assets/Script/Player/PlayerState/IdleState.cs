using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : ActionState
{
    public IdleState(PlayerControler controler, Direction direction) : base(controler, direction) { }

    protected override ActionState CreateNewState()
    {
        return new IdleState(controler, currentDirect);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (controler.PlayerMovement.movement != Vector2.zero && !controler.Player.CanNotAction())
        {
            controler.PlayerStateMachine.ChangeState(new MoveState(controler, currentDirect));
        }
    }
}
