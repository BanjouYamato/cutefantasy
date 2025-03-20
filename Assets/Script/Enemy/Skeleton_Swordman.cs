using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Swordman : EnemyCanAttack
{
    protected override Vector3 SetChaseTarget()
    {
        return player.position;
    }
}
