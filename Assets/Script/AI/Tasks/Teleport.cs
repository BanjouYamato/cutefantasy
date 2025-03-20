using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : EnemyActions
{
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField] 
    Collider2D collider;
    [SerializeField]
    Collider2D teleRange;
    float timer;
    float teleTime = 1f;

    public override void OnStart()
    {
        Observer.Instance.Notify(ObserverCostant.BOSS_TELEPORT, false);
        spriteRenderer.enabled = false;
        collider.enabled = false;
        timer = 0f;
    }
    public override TaskStatus OnUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= teleTime)
        {
            TeleportPos();
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
    void TeleportPos()
    {
        float posX = Random.Range(teleRange.bounds.min.x, teleRange.bounds.max.x);
        float posY = Random.Range(teleRange.bounds.min.y, teleRange.bounds.max.y);
        transform.position = new Vector3(posX, posY, transform.position.z);
        spriteRenderer.enabled = true;
        collider.enabled = true;
        Observer.Instance.Notify(ObserverCostant.BOSS_TELEPORT, true);
    }
}
