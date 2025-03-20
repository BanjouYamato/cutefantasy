using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    int curHP;
    [SerializeField] RockScriptable RockScriptable;
    [SerializeField] GameObject particle;
    [SerializeField]
    AudioClip performAxeSFX, breakRockSFX;
    private void OnEnable()
    {
        curHP = RockScriptable.MaxHP;
    }
    public void BreakRock(int damage)
    {
        curHP -= damage;
        SoundManager.Instance.PlayOS(performAxeSFX);
        if (curHP <= 0)
        {   
            SoundManager.Instance.PlayOS(breakRockSFX);
            DropItem();
            EffectManager.Instance.GetEffect(particle, transform.position);
            gameObject.SetActive(false);
        }
    }
    void DropItem()
    {
        for (int i = 0; i < RockScriptable.DropCount; i++)
        {
            GameObject item = Instantiate(RockScriptable.DropResourcePrefab);
            item.transform.position = transform.position;
        }
    }
}
