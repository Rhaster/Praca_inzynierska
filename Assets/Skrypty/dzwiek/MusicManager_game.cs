using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class MusicManager_game : MonoBehaviour
{

    public static MusicManager_game Instance { get; private set; }

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
        DisplayAllPlayerPrefs();
        Instance = this;
        volumeSoundSlider.value = 1;
        volumeMusicSlider.value = 1;
        if (PlayerPrefs.HasKey("Volume"))
        {Debug.Log("pobraie sound z gry");
            soundVolume = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            soundVolume = GetVolumeFromAudioMixerGroup(soundEffectsGroup, true);
        }
        if(PlayerPrefs.HasKey("Volume1"))
        {Debug.Log("pobraie sound z gry");
            musicVolume = PlayerPrefs.GetFloat("Volume1");
        }
        else
        {
            musicVolume = GetVolumeFromAudioMixerGroup(musicAudioGroup, false);
        }
    }
    float GetVolumeFromAudioMixerGroup(AudioMixerGroup audioMixerGroup,bool x)
    {
        float volumeValue = 0f;

        if (audioMixerGroup != null && x == true)
        {
            audioMixerGroup.audioMixer.GetFloat("Volume", out volumeValue); // "Volume" to nazwa parametru g�o�no�ci w mikserze audio
            volumeValue = Mathf.Pow(10f, volumeValue / 20f); // Konwersja z decybeli do warto�ci normalizowanej (0-1)
        }
        else if(audioMixerGroup != null && x != true)
        {
            audioMixerGroup.audioMixer.GetFloat("Volume1", out volumeValue); // "Volume" to nazwa parametru g�o�no�ci w mikserze audio
            volumeValue = Mathf.Pow(10f, volumeValue / 20f); // Konwersja z decybeli do warto�ci normalizowanej (0-1)
        }

        return volumeValue;
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

        ChangeColorByVolume(musicVolume, volumeMusicSlider);
        ChangeColorByVolume1(soundVolume, volumeSoundSlider);
    }

    private void SetMusicVolume(float volume)
    {
        volume = Mathf.Clamp(volume, (float)0.001, 1);
        musicAudioGroup.audioMixer.SetFloat("Volume1", Mathf.Log10(volume) * 20);
        musicVolume = volume;
        ChangeColorByVolume1(volume, volumeMusicSlider);
        PlayerPrefs.SetFloat("Volume1", musicVolume);
        PlayerPrefs.Save();
    }
    private void SetSoundVolume(float volume)
    {
        volume = Mathf.Clamp(volume, (float)0.001, 1);
        soundEffectsGroup.audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        soundVolume = volume;
        ChangeColorByVolume(volume, volumeSoundSlider);
        PlayerPrefs.SetFloat("Volume", soundVolume);
        PlayerPrefs.Save();
    }

    void ChangeColorByVolume(float volume1, Slider x)
    {
        float normalizedVolume = Mathf.InverseLerp(x.minValue, x.maxValue, volume1);
        Color newColor = Color.Lerp(lowVolumeColor, highVolumeColor, normalizedVolume);
        x.value = volume1;
        sliderFill.color = newColor;
    }
    void ChangeColorByVolume1(float volume, Slider x)
    {
        float normalizedVolume = Mathf.InverseLerp(x.minValue, x.maxValue, volume);
        Color newColor = Color.Lerp(lowVolumeColor, highVolumeColor, normalizedVolume);
        x.value = volume;
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
    void DisplayAllPlayerPrefs()
    {
        // Pobierz wszystkie klucze
        string[] allKeys = PlayerPrefs.GetString("allKeys", "").Split(';');

        // Przejd� przez klucze i wy�wietl ich warto�ci
        foreach (var key in allKeys)
        {
            if (!string.IsNullOrEmpty(key))
            {
                string value = PlayerPrefs.GetString(key);
                Debug.Log("Klucz: " + key + ", Warto��: " + value);
            }
        }
    }
}
 