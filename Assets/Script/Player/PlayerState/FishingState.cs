public class FishingState : ThrowFishRod
{
    public FishingState(PlayerControler controler, Direction direction) 
        : base(controler, direction) { }

    public override void EnterState()
    {
        controler.PlayerStateMachine.PlayAnimation($"IsFishing", 2);
        controler.PlayerStateMachine.anim.SetLayerWeight(2, 1);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    protected override ActionState CreateNewState()
    {
        return new FishingState(controler, currentDirect);
    }
}
