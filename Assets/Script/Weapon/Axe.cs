using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : WeaponBase
{
    protected override void Update()
    {
        base.Update();
        SpriteRenderer child = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (GameControler.Instance.runTimeData.stats.dir == Direction.up)
        {
            child.sortingOrder = 9;
        }
        else
            child.sortingOrder = 11;
    }
}
