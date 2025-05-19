using UnityEngine;

public class EnemyMoveState : EnemyBaseState
{
    public EnemyMoveState(EnemyControler controler, Direction direction) 
        : base(controler, direction)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        controler.fsm.PlayAnimation($"{controler.EnemyInfo.animName}_run_{currentDirection}");
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (controler.pathFinding.GetMoveDir() == Vector3.zero)
        {
            controler.fsm.ChangeEnemyState(new EnemyIdleState(controler, currentDirection));
        }
    }
    protected override EnemyBaseState GetState()
    {
        return new EnemyMoveState(controler, currentDirection);
    }
}
