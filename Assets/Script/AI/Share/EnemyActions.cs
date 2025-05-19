using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyActions : Action
{
    protected Rigidbody2D rb;
    protected Animator anim;
    protected PlayerControler player; 
    protected Observer observer;
    protected BossData bossData;

    public override void OnAwake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        player = PlayerControler.instance;
        observer = Observer.Instance;
        bossData = GetComponent<BossData>();
    }
}
