using BehaviorDesigner.Runtime.Tasks;

public class IsHaveShield : EnemyConditional
{   
    public override TaskStatus OnUpdate()
    {
        return !data.Shield ? TaskStatus.Success : TaskStatus.Failure;
    }
}
