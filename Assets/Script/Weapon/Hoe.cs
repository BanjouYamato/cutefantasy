using UnityEngine;

public class Hoe : WeaponBase
{
    protected override void Update()
    {
        base.Update();
        SpriteRenderer child = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (GameControler.Instance.runTimeData.stats.dir == Direction.up)
        {
            child.sortingOrder = 4;
        }
        else
            child.sortingOrder = 5;
    }
}
