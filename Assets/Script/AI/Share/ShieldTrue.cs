using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTrue : EnemyActions
{
    public override TaskStatus OnUpdate()
    {
        bossData.GetShield();
        return TaskStatus.Success;
    }
}
