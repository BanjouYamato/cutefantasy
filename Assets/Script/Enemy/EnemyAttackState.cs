using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyControler controler, Direction direction)
        : base(controler, direction) { }

    public override void EnterState()
    {   
        controler.fsm.PlayAnimation($"{controler.EnemyInfo.animName}_attack_{currentDirection}");
        count = 0;
    }

    public override void ExitState()
    {
        base.ExitState();
        controler.behavior.IsAttacking = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        count += Time.deltaTime;
        if (count
            > controler.fsm.anim.GetCurrentAnimatorStateInfo(0).length)
        {
            controler.fsm.ChangeEnemyState(new EnemyIdleState(controler, currentDirection));
        }
    }

    protected override EnemyBaseState GetState()
    {
        return new EnemyAttackState(controler, currentDirection);
    }
}
