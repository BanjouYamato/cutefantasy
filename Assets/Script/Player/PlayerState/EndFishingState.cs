public class EndFishingState : ThrowFishRod
{
    public EndFishingState(PlayerControler controler, Direction direction) 
        : base(controler, direction) { }

    public override void EnterState()
    {
        controler.PlayerStateMachine.PlayAnimation($"WonFish", 2);
        controler.PlayerStateMachine.anim.SetLayerWeight(2, 1);
    }
    public override void ExitState()
    {
        base.ExitState();
        controler.PlayerFishing.OutOfFishing();
    }
    protected override ActionState CreateNewState()
    {
        return new EndFishingState(controler, currentDirect);
    }
}
