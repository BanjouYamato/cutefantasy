public abstract class EnemyBaseState : IState
{   
    protected EnemyControler controler;
    protected Direction currentDirection;
    protected float count;
    public EnemyBaseState(EnemyControler controler, Direction direction)
    {   
        this.controler = controler;
        this.currentDirection = direction;
    }
    public virtual void EnterState() { }

    public virtual void ExitState() { }

    public virtual void UpdateState()
    {
        ChangeDirectState();
    }
    void ChangeDirectState()
    {
        if (controler.behavior.IsAttacking)
            return;
        Direction newDirect = controler.pathFinding.DirectionCondition();
        if (this.currentDirection != newDirect)
        {   
            this.currentDirection = newDirect;
            controler.fsm.ChangeEnemyState(GetState());
        }

    }
    protected abstract EnemyBaseState GetState();
}
