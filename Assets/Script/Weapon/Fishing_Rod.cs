using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing_Rod : WeaponBase
{
    [SerializeField] 
    PlayerFishing fishing;

    bool throwRod;
    bool isFishing;
    public override void PlayAnimation()
    {
        if (manager == null)
            manager = FindObjectOfType<WeaponManager>();
        if (!throwRod)
            manager.PlayAnimation($"ThrowRod");
        else
        {
            manager.PlayAnimation($"EndFish");
            isFishing = false;
        }
            
        count = 0;
    }

    protected override void Update()
    {
        if (!throwRod)
        {
            count += Time.deltaTime;

            if (count >= manager.animator.GetCurrentAnimatorStateInfo(0).length && fishing.IsInWater)
            {
                manager.PlayAnimation("IsFishing");
                throwRod = true;
                isFishing = true;
                count = 0;
            }
            else if (count >= manager.animator.GetCurrentAnimatorStateInfo(0).length && !fishing.IsInWater)
            {
                gameObject.SetActive(false);
                count = 0;
            }
        }
        if (throwRod && !isFishing)
        {
            count += Time.deltaTime;
            
            if (count >= manager.animator.GetCurrentAnimatorStateInfo(0).length )
            {
                throwRod = false;
                gameObject.SetActive(false);
                count = 0;
            }
        }
    }

}
