using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTheme : MonoBehaviour
{
    [SerializeField]
    AudioClip musicTheme;
    private void Start()
    {   
        if (SoundManager.Instance.GetAudioPlaying(musicTheme))
            SoundManager.Instance.BackGroundMusic(musicTheme);
    }
}
