using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class Menadzer_dzwieku_scena_gry : MonoBehaviour
{
   
    public static Menadzer_dzwieku_scena_gry Instance { get; private set; }
    [FormerlySerializedAs("grupa_muzyki_AMG")]
    public AudioMixerGroup musicAudioGroup;
    [SerializeField] private AudioClip[] klipy_AudioClips;
    public enum Sound
    {
        ZmianaElektryki,
        DodanieEnergi,
        Strzal,
        Wybuch,
    }

    private AudioSource audioSource;
    private Dictionary<Sound, AudioClip> dzwieki_Slownik;


    private void Awake()
    {
        klipy_AudioClips = new AudioClip[10];
        Instance = this;
        float glosnosc_Float = 0.4f;
        if (PlayerPrefs.HasKey("Volume"))
        {
            musicAudioGroup.audioMixer.GetFloat("Volume", out glosnosc_Float);
        }
        
        audioSource = GetComponent<AudioSource>();
       

        dzwieki_Slownik = new Dictionary<Sound, AudioClip>();
        int i = 0;
        foreach (Sound sound in System.Enum.GetValues(typeof(Sound)))
        {
            dzwieki_Slownik[sound] = Resources.Load<AudioClip>(sound.ToString());
            if (dzwieki_Slownik[sound]!=null)
            {
                klipy_AudioClips[i]= dzwieki_Slownik[sound];
                i += 1;
            }
        }
    }

    public void Zagraj_Dzwiek(Sound dzwiek)
    {
        audioSource.PlayOneShot(dzwieki_Slownik[dzwiek]);
 
    }
    public void Zagraj_Dzwiek_z_inna_glosnoscia(Sound dzwiek, float glosnosc)
    {
        float temp = audioSource.volume;
        audioSource.volume = glosnosc; 
        audioSource.PlayOneShot(dzwieki_Slownik[dzwiek]);
        audioSource.volume = temp;
    }

}

