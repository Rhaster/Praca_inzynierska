using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager_game : MonoBehaviour
{
   
    public static SoundManager_game Instance { get; private set; }

    public AudioMixerGroup musicAudioGroup;
    [SerializeField] private AudioClip[] audioClips;
    public enum Sound
    {
        ZmianaElektryki,
        DodanieEnergi,
        BudynekPostawienie,
        WrogGinie,
        WrogTrafiony,
        GameOver,
    }

    private AudioSource audioSource;
    private Dictionary<Sound, AudioClip> soundAudioClipDictionary;
    private float Volume;

    private void Awake()
    {
        audioClips = new AudioClip[10];
        Instance = this;
        float volumeValue = 1;
        if (PlayerPrefs.HasKey("Volume"))
        {
            musicAudioGroup.audioMixer.GetFloat("Volume", out volumeValue);
        }
        Volume= volumeValue;
        audioSource = GetComponent<AudioSource>();

        soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();
        int i = 0;
        foreach (Sound sound in System.Enum.GetValues(typeof(Sound)))
        {
            soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
            if (soundAudioClipDictionary[sound]!=null)
            {
                audioClips[i]= soundAudioClipDictionary[sound];
                i += 1;
            }
        }
    }

    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(soundAudioClipDictionary[sound]);
        //audioSource.Play();
    }

}

