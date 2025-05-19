using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlameAttack : EnemyActions
{
    [SerializeField]
    GameObject FireBall;
    [SerializeField]
    float spawnTime;
    [SerializeField]
    int spawnCount;
    [SerializeField]
    Transform skillStorage, skillPos;
    [SerializeField]
    AudioClip flameSFX;
    float angleRange = 90f;
    Vector3 directionToPlayer;
    public override void OnStart()
    {
        directionToPlayer = (PlayerControler.instance.transform.position - transform.position).normalized;
    }
    public override TaskStatus OnUpdate()
    {
        var sequence = DOTween.Sequence();
        for (int i = 0; i < spawnCount; i++)
        {
            int index = i;
            sequence.AppendCallback(() => FlameShoot(index));
            sequence.AppendInterval(spawnTime);
        }
        return TaskStatus.Success;
    }

    private void FlameShoot(int index)
    {
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        float startAngle = angleToPlayer - (angleRange / 2);
        float angleStep = angleRange / (spawnCount - 1);
        float currentAngle = startAngle + (index * angleStep);
        float radianAngle = currentAngle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle)).normalized;
        GameObject fireBalllClone = GameObject.Instantiate(FireBall, skillPos.position, Quaternion.identity);
        fireBalllClone.transform.SetParent(skillStorage);
        FireBallState state = fireBalllClone.GetComponent<FireBallState>();
        state.ResetState();
        state.FireBallFly(direction);
        SoundManager.Instance.PlayOS(flameSFX);
    }
    float RandomValue()
    {
        return Random.value;
    }
}
