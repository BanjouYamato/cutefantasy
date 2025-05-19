using BehaviorDesigner.Runtime.Tasks;
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
