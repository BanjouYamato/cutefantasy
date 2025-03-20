using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHaveShield : EnemyConditional
{   
    public override TaskStatus OnUpdate()
    {
        return !data.Shield ? TaskStatus.Success : TaskStatus.Failure;
    }
}
