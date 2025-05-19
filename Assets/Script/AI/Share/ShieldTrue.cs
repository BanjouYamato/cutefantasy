using BehaviorDesigner.Runtime.Tasks;

public class ShieldTrue : EnemyActions
{
    public override TaskStatus OnUpdate()
    {
        bossData.GetShield();
        return TaskStatus.Success;
    }
}
