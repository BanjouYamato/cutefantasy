public abstract class ActionState : BaseState 
{

    protected Direction currentDirect;
    protected PlayerControler controler;
    protected float count;
    public ActionState(PlayerControler controler, Direction direction)
    {
        this.controler = controler;
        this.currentDirect = direction;
    }

    public override void EnterState()
    {
        PlayAnimation();
    }
    public override void UpdateState()
    {
        ChangeDirectState();
    }

    void PlayAnimation()
    {
        controler.PlayerStateMachine.PlayAnimation($"{GetType().Name}_{currentDirect}", 0);
    }
    void ChangeDirectState()
    {
        Direction newDirect = controler.PlayerStats.dir;
        if (this.currentDirect != newDirect)
        {
            this.currentDirect = newDirect;
            controler.PlayerStateMachine.ChangeState(CreateNewState());
        }

    }
    protected abstract ActionState CreateNewState();
}
