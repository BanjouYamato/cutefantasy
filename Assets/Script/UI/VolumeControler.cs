using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VolumeControler : MonoBehaviour
{
    [SerializeField]
    Slider musicSlider, soundSlider;

    [SerializeField]
    Sprite lowVolume, mediumVolume, highVolume;

    [SerializeField]
    Image musicFill, soundFill;

    private void Start()
    {
        musicSlider.value = SoundManager.Instance.GetMusicVolume();
        ChangeFillColor(musicSlider.value, musicFill);
        soundSlider.value = SoundManager.Instance.GetSoundVolume();
        ChangeFillColor(soundSlider.value, soundFill);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        soundSlider.onValueChanged.AddListener(SetSoundVolume);
    }
    void SetMusicVolume(float volume)
    {
        SoundManager.Instance.SetMusicVolume(volume);
        ChangeFillColor(volume, musicFill);
        PlayerPrefs.SetFloat(GameConstant.VOLUME_MUSIC, volume);
        PlayerPrefs.Save();
    }
    void SetSoundVolume(float volume)
    {
        SoundManager.Instance.SetSoundVolume(volume);
        PlayerPrefs.SetFloat(GameConstant.VOLUME_SOUND, volume);
        PlayerPrefs.Save();
    }
    void ChangeFillColor(float volume, Image fill)
    {
        if (volume < 0.33)
            fill.sprite = lowVolume;
        else if (volume < 0.66)
            fill.sprite = mediumVolume;
        else 
            fill.sprite = highVolume;
    }
}
