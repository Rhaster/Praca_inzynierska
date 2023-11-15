using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class MusicManager : MonoBehaviour
{

    public static MusicManager Instance { get; private set; }

    public Slider volumeMusicSlider;
    public Slider volumeSoundSlider;
    public AudioMixerGroup musicAudioGroup;
    public AudioMixerGroup soundEffectsGroup;
    public Color lowVolumeColor = Color.green;
    public Color mediumVolumeColor = Color.yellow;
    public Color highVolumeColor = Color.red;
    //const string MIXER_MUSIC = "MusicVolume";
    //const string MIXER_SOUND = "SOUNDVolume";
    private Image sliderFill;
    private Image sliderSoundFill;

    [SerializeField] private float soundVolume;
    [SerializeField] private float musicVolume;

    private void Awake()
    {
        Instance = this;
        volumeSoundSlider.value = 1;
        volumeMusicSlider.value = 1;
    }
    

    void Start()
    {
        volumeSoundSlider.value = soundVolume;
        volumeSoundSlider.onValueChanged.AddListener(SetSoundVolume);
        sliderFill = volumeSoundSlider.fillRect.GetComponent<Image>();

        volumeMusicSlider.value = musicVolume;
        volumeMusicSlider.onValueChanged.AddListener(SetMusicVolume);
        sliderSoundFill = volumeMusicSlider.fillRect.GetComponent<Image>();
        sliderFill.color = highVolumeColor;
        sliderSoundFill.color = highVolumeColor;
    }

    private void SetMusicVolume(float volume)
    {
        volume = Mathf.Clamp(volume, (float)0.001, 1);
        musicAudioGroup.audioMixer.SetFloat("Volume1", Mathf.Log10(volume) * 20);
        musicVolume= volume;
        ChangeColorByVolume1(volume, volumeMusicSlider);
    }
    private void SetSoundVolume(float volume)
    {
        volume = Mathf.Clamp(volume, (float)0.001, 1);
        soundEffectsGroup.audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        soundVolume = volume;
        ChangeColorByVolume(volume, volumeSoundSlider);
    }

    void ChangeColorByVolume(float volume1, Slider x)
    {
        float normalizedVolume = Mathf.InverseLerp(x.minValue, x.maxValue, volume1);
        Color newColor = Color.Lerp(lowVolumeColor, highVolumeColor, normalizedVolume);
        sliderFill.color = newColor;
    }
    void ChangeColorByVolume1(float volume, Slider x)
    {
        float normalizedVolume = Mathf.InverseLerp(x.minValue, x.maxValue, volume);
        Color newColor = Color.Lerp(lowVolumeColor, highVolumeColor, normalizedVolume);
        sliderSoundFill.color = newColor;
    }
    public float GetSoundVolume()
    {
        return 1;//return SoundVolume;
    }
    public float GetMusicVolume()
    {
        return 1;//MusicVolume;
    }
}
 