public abstract class BaseState : IState
{
    public virtual void EnterState() { }

    public virtual void ExitState() { }

    public virtual void UpdateState() { }

}
