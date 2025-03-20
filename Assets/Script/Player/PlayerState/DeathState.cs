using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : ActionState
{
    public DeathState(PlayerControler controler, Direction direction) : base(controler, direction)
    {
    }
    public override void EnterState()
    {
        controler.PlayerStateMachine.PlayAnimation("DeathState", 0);
    }

    protected override ActionState CreateNewState()
    {
        return new DeathState(controler, currentDirect);
    }
}
