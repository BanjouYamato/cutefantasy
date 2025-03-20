using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingleTon<SoundManager>
{
    [SerializeField]   
    
    AudioSource m_AudioMusicSource, m_AudioSoundSource;

    [SerializeField]
    AudioClip uiSFX;

    private void Start()
    {
        m_AudioMusicSource.volume = PlayerPrefs.GetFloat(GameConstant.VOLUME_MUSIC, 0.5f);
        m_AudioSoundSource.volume = PlayerPrefs.GetFloat(GameConstant.VOLUME_SOUND, 0.5f);
    }
    public void BackGroundMusic(AudioClip clip)
    {   
        if (m_AudioMusicSource.isPlaying)
        {
            m_AudioMusicSource.Stop();
        }
        m_AudioMusicSource.clip = clip;
        m_AudioMusicSource.Play();
    }

    public void PlayOS()
    {
        m_AudioSoundSource.PlayOneShot(uiSFX);
    }

    public void PlayOS(AudioClip clip)
    {
        m_AudioSoundSource.PlayOneShot(clip);
    }
    public bool GetAudioPlaying(AudioClip clip)
    {
        return m_AudioMusicSource.clip != clip ? true : false;
    }
    public void SetMusicVolume(float volume)
    {
        m_AudioMusicSource.volume = volume;
    }
    public float GetMusicVolume()
    {
        return m_AudioMusicSource.volume;
    }
    public void SetSoundVolume(float volume)
    {
        m_AudioSoundSource.volume = volume;
    }
    public float GetSoundVolume()
    {
        return m_AudioSoundSource.volume;
    }
}
