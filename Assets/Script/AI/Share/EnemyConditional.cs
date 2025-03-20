using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyConditional : Conditional
{
    protected Rigidbody2D rb;
    protected Animator anim;
    protected PlayerControler player;
    protected BossData data;

    public override void OnAwake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        player = PlayerControler.instance;
        data = GetComponent<BossData>();
    }
}
