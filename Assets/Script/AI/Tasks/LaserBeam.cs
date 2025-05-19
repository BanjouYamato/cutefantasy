using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class LaserBeam : EnemyActions
{
    float laserTime = 5f;
    float timer;


    public override void OnStart()
    {
        timer = 0f;
        observer.Notify(ObserverCostant.BOSS_LASERBEAM_ON);
    }
    public override TaskStatus OnUpdate()
    {   
        timer += Time.deltaTime;
        if (timer >= laserTime)
        {
            rb.velocity = Vector2.zero;
            observer.Notify(ObserverCostant.BOSS_LASERBEAM_OFF);
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}
