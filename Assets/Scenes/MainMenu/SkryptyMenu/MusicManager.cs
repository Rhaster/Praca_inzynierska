using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicManager : MonoBehaviour
{
    public Slider volumeSlider; 
    public AudioSource musicAudioSource; 

    public Color lowVolumeColor = Color.green;
    public Color mediumVolumeColor = Color.yellow;
    public Color highVolumeColor = Color.red;
    private Image sliderFill;

    void Start()
    {
        // Ustaw warto�� suwaka na pocz�tkow� g�o�no��
        volumeSlider.value = musicAudioSource.volume;

        // Dodaj obs�ug� zdarzenia na zmian� warto�ci suwaka
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
        sliderFill = volumeSlider.fillRect.GetComponent<Image>();
    }

    void ChangeVolume(float volume)
    {
        // Ustaw g�o�no�� Audio Source na podstawie warto�ci suwaka (0.0 do 1.0)
        musicAudioSource.volume = volume;
        ChangeColorByVolume(volume);
    }
    void ChangeColorByVolume(float volume)
    {
        // Przelicz warto�� suwaka na zakres 0-1
        float normalizedVolume = Mathf.InverseLerp(volumeSlider.minValue, volumeSlider.maxValue, volume);

        // Interpoluj kolor na podstawie gradientu
        Color newColor = Color.Lerp(lowVolumeColor, highVolumeColor, normalizedVolume);

        // Ustaw nowy kolor w komponencie Image suwaka
        sliderFill.color = newColor;
    }
}
