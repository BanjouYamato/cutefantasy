using UnityEngine;

public class ThrowFishRod : ActionState
{
    public ThrowFishRod(PlayerControler controler, Direction direction) 
        : base(controler, direction) { }

    public override void EnterState()
    {
        controler.PlayerStateMachine.PlayAnimation($"ThrowRod", 2);
        controler.PlayerStateMachine.anim.SetLayerWeight(2, 1);
        count = 0;
    }

    public override void ExitState()
    {
        base.ExitState();
        controler.PlayerAction.PerformingAction = false;
        controler.PlayerStateMachine.anim.SetLayerWeight(2, 0);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        count += Time.deltaTime;
        if (count > controler.PlayerStateMachine.anim.GetCurrentAnimatorStateInfo(2).length 
            && controler.PlayerFishing.IsInWater)
        {
            controler.PlayerStateMachine.ChangeState(new FishingState(controler, currentDirect));
        }
        else if (count > controler.PlayerStateMachine.anim.GetCurrentAnimatorStateInfo(2).length 
            && !controler.PlayerFishing.IsInWater)
        {
            controler.PlayerStateMachine.ChangeState(new IdleState(controler, currentDirect));
        }
    }

    protected override ActionState CreateNewState()
    {
        return new ThrowFishRod(controler, currentDirect);
    }
}
