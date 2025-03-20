using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : ActionState
{   
    public AttackState(PlayerControler controler, Direction direction) 
        : base(controler, direction)
    {
       
    }

    public override void EnterState()
    {
        controler.PlayerStateMachine.PlayAnimation($"{controler.PlayerStats.weapon.ItemName}_{currentDirect}_{controler.PlayerAction.comboStep}"
            , controler.PlayerStats.weapon.indexLayer);
        controler.PlayerStateMachine.anim.SetLayerWeight(controler.PlayerStats.weapon.indexLayer, 1);
        count = 0;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        count += Time.deltaTime;
        if (count  > controler.PlayerStateMachine.anim.GetCurrentAnimatorStateInfo(1).length)
        {
            controler.PlayerStateMachine.ChangeState(new IdleState(controler, currentDirect));
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        controler.PlayerAction.PerformingAction = false;
        controler.PlayerStateMachine.anim.SetLayerWeight(controler.PlayerStats.weapon.indexLayer, 0);
    }

    protected override ActionState CreateNewState()
    {
        return new AttackState(controler, currentDirect);
    }
    
}
