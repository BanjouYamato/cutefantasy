using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class WaitAnim : EnemyConditional
{
    public Animator animator;
    float count;
    public override void OnStart()
    {
        count = 0;
    }
    public override TaskStatus OnUpdate()
    {
        count += Time.deltaTime;
        if (count > animator.GetCurrentAnimatorStateInfo(0).length)
            return TaskStatus.Success;
        return TaskStatus.Running;
    }
}
