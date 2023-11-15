using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    public AudioMixerGroup soundEffectsGroup;

    void Start()
    {
        // Przyk³adowe ustawienie g³oœnoœci grupy dŸwiêkowej na pocz¹tku gry
        SetGroupVolume(0.5f);
    }

    void Update()
    {
        // Przyk³adowa zmiana g³oœnoœci grupy dŸwiêkowej w czasie rzeczywistym
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
