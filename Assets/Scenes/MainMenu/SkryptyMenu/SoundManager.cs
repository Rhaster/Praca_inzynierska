using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    public AudioMixerGroup soundEffectsGroup;

    void Start()
    {
        // Przyk�adowe ustawienie g�o�no�ci grupy d�wi�kowej na pocz�tku gry
        SetGroupVolume(0.5f);
    }

    void Update()
    {
        // Przyk�adowa zmiana g�o�no�ci grupy d�wi�kowej w czasie rzeczywistym
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetGroupVolume(0.2f);
        }
    }

    void SetGroupVolume(float volume)
    {
        soundEffectsGroup.audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }
}
