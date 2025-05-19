using UnityEngine;

public class EnemyHitState : EnemyBaseState
{
    public EnemyHitState(EnemyControler controler, Direction direction) 
        : base(controler, direction) { }

    public override void EnterState()
    {
        controler.fsm.PlayAnimation($"{controler.EnemyInfo.animName}_hit_{currentDirection}");
        count = 0;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        count += Time.deltaTime;
        if (count > 0.34f && !controler.data.Death)
        {
            controler.fsm.ChangeEnemyState(new EnemyIdleState(controler, currentDirection));
        }
        else if (count > 0.34f && controler.data.Death)
        {
            controler.fsm.ChangeEnemyState(new EnemyDeathState(controler, currentDirection));
        }
    }

    protected override EnemyBaseState GetState()
    {
        return new EnemyHitState(controler, currentDirection);
    }
}
