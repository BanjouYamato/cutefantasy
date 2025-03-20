using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundFX : EnemyActions
{
    [SerializeField]
    AudioClip soundClip;
    
    public override void OnStart()
    {
        if (soundClip != null)
        {
            SoundManager.Instance.PlayOS(soundClip);    
        }
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
