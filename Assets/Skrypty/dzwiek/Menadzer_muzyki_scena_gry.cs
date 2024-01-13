using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class Menadzer_muzyki_scena_gry : MonoBehaviour
{

    public static Menadzer_muzyki_scena_gry Instance { get; private set; }

    public Slider wartosc_slidera_muzyki_Slider;
    public Slider wartosc_slidera_dzwieku_Slider;
    public AudioMixerGroup grupa_muzyki_AMG;
    public AudioMixerGroup efekty_grupy_AMG;
    public Color niska_wart_koloru_Color = Color.green;
    public Color srednia_wart_koloru_Color = Color.yellow;
    public Color wysoka_wart_Koloru_Color = Color.red;

    private Image napelnienie_Slidera;
    private Image napelnienie_muzyka_slider;

    [SerializeField] private float dzwiek_gloscnosc_Float;
    [SerializeField] private float muzyka_wartosc_Float;
    [FormerlySerializedAs("klipy_AudioClips")]
    [SerializeField] private AudioClip[] klipy_audio_AudioClip;

    private AudioSource audioSource;
    public AudioClip muzyka_Boss_AudioClip;


    private void Awake()
    {

        audioSource = GetComponent<AudioSource>();
        if (klipy_audio_AudioClip.Length > 0)
        {
            Graj_Muzyke_w_petli();
        }
        else
        {
            Debug.LogWarning("Pusta lista muzyki");
        }
        Instance = this;
        wartosc_slidera_dzwieku_Slider.value = 0.4f;
        wartosc_slidera_muzyki_Slider.value = 0.4f;
        if (PlayerPrefs.HasKey("Volume"))
        {
            dzwiek_gloscnosc_Float = PlayerPrefs.GetFloat("Volume");
            wartosc_slidera_dzwieku_Slider.value = dzwiek_gloscnosc_Float;
            efekty_grupy_AMG.audioMixer.SetFloat("Volume", Mathf.Log10(dzwiek_gloscnosc_Float) * 20);
        }
        else
        {
            dzwiek_gloscnosc_Float = GetDzwiekZGrrupyDzwieku(grupa_muzyki_AMG,true);
            wartosc_slidera_dzwieku_Slider.value = dzwiek_gloscnosc_Float;
            efekty_grupy_AMG.audioMixer.SetFloat("Volume", Mathf.Log10(dzwiek_gloscnosc_Float) * 20);
        }
        if (PlayerPrefs.HasKey("Volume1"))
        {
            muzyka_wartosc_Float = PlayerPrefs.GetFloat("Volume1");
            wartosc_slidera_muzyki_Slider.value = muzyka_wartosc_Float;
            grupa_muzyki_AMG.audioMixer.SetFloat("Volume1", Mathf.Log10(muzyka_wartosc_Float) * 20);
        }
        else
        {
            muzyka_wartosc_Float = GetDzwiekZGrrupyDzwieku(grupa_muzyki_AMG, false);
            wartosc_slidera_muzyki_Slider.value = muzyka_wartosc_Float;
            grupa_muzyki_AMG.audioMixer.SetFloat("Volume1", Mathf.Log10(muzyka_wartosc_Float) * 20);
        }
        audioSource.volume = muzyka_wartosc_Float;
    }
    float GetDzwiekZGrrupyDzwieku(AudioMixerGroup audioMixerGroup, bool x)
    {
        float volumeValue = 0f;

        if (audioMixerGroup != null && x == true)
        {
            audioMixerGroup.audioMixer.GetFloat("Volume", out volumeValue); // "Volume" to nazwa parametru g³oœnoœci w mikserze audio
            volumeValue = Mathf.Pow(10f, volumeValue / 20f); // Konwersja z decybeli do wartoœci normalizowanej (0-1)
        }
        else if (audioMixerGroup != null && x != true)
        {
            audioMixerGroup.audioMixer.GetFloat("Volume1", out volumeValue); // "Volume" to nazwa parametru g³oœnoœci w mikserze audio
            volumeValue = Mathf.Pow(10f, volumeValue / 20f); // Konwersja z decybeli do wartoœci normalizowanej (0-1)
        }

        return volumeValue;
    }

    void Start()
    {
        wartosc_slidera_dzwieku_Slider.value = dzwiek_gloscnosc_Float;
        wartosc_slidera_dzwieku_Slider.onValueChanged.AddListener(Ustaw_wartosc_dzwieku);
        napelnienie_Slidera = wartosc_slidera_dzwieku_Slider.fillRect.GetComponent<Image>();

        wartosc_slidera_muzyki_Slider.value = muzyka_wartosc_Float;
        wartosc_slidera_muzyki_Slider.onValueChanged.AddListener(Ustaw_wartosc_muzyki);
        napelnienie_muzyka_slider = wartosc_slidera_muzyki_Slider.fillRect.GetComponent<Image>();
        napelnienie_Slidera.color = wysoka_wart_Koloru_Color;
        napelnienie_muzyka_slider.color = wysoka_wart_Koloru_Color;

        Zmien_kolor_przez_wartosc_muzyki(muzyka_wartosc_Float, wartosc_slidera_muzyki_Slider);
        Zmien_kolor_przez_wartosc_dzwieku(dzwiek_gloscnosc_Float, wartosc_slidera_dzwieku_Slider);
    }

    private void Ustaw_wartosc_muzyki(float volume)
    {
        volume = Mathf.Clamp(volume, (float)0.001, 1);
        grupa_muzyki_AMG.audioMixer.SetFloat("Volume1", Mathf.Log10(volume) * 20);
        muzyka_wartosc_Float = volume;
        Zmien_kolor_przez_wartosc_muzyki(volume, wartosc_slidera_muzyki_Slider);
        audioSource.volume = muzyka_wartosc_Float;
        PlayerPrefs.SetFloat("Volume1", muzyka_wartosc_Float);
        PlayerPrefs.Save();
    }
    private void Ustaw_wartosc_dzwieku(float volume)
    {
        volume = Mathf.Clamp(volume, (float)0.001, 1);
        efekty_grupy_AMG.audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        dzwiek_gloscnosc_Float = volume;

        Zmien_kolor_przez_wartosc_dzwieku(volume, wartosc_slidera_dzwieku_Slider);
        PlayerPrefs.SetFloat("Volume", dzwiek_gloscnosc_Float);
        PlayerPrefs.Save();
    }

    void Zmien_kolor_przez_wartosc_dzwieku(float volume1, Slider x)
    {
        float normalizedVolume = Mathf.InverseLerp(x.minValue, x.maxValue, volume1);
        Color newColor = Color.Lerp(niska_wart_koloru_Color, wysoka_wart_Koloru_Color, normalizedVolume);
        x.value = volume1;
        napelnienie_Slidera.color = newColor;
    }
    void Zmien_kolor_przez_wartosc_muzyki(float volume, Slider x)
    {
        float normalizedVolume = Mathf.InverseLerp(x.minValue, x.maxValue, volume);
        Color newColor = Color.Lerp(niska_wart_koloru_Color, wysoka_wart_Koloru_Color, normalizedVolume);
        x.value = volume;
        napelnienie_muzyka_slider.color = newColor;
    }




    #region Muzyka
    private bool stopCoroutine = false;
    public void Wlacz_musyzke_bossa()
    {
        Zatrzymaj_muzyke();
        audioSource.clip = muzyka_Boss_AudioClip;
        audioSource.Play();
    }
    private void Zatrzymaj_muzyke()
    {
        stopCoroutine = true;
    }
    private void Graj_Muzyke_w_petli()
    {

        StartCoroutine(Graj_audio_corutine());
    }


    private System.Collections.IEnumerator Graj_audio_corutine()
    {
        while (!stopCoroutine)
        {
            for (int i = 0; i < klipy_audio_AudioClip.Length; i++)
            {
                if (!stopCoroutine)
                {
                    audioSource.clip = klipy_audio_AudioClip[i];
                    audioSource.Play();
                }
                yield return new WaitForSeconds(audioSource.clip.length);
            }
        }
    }
    #endregion
}