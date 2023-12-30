using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class Kontroler_muzyki : MonoBehaviour
{


    #region Ustawienia muzyki
    public Slider myzyka_Slider;
    public Slider dzwiek_Slider;
    public AudioMixerGroup grupa_muzyki_AMG;
    public AudioMixerGroup grupa_efekow_AMG;
    public Color niski_dzwiek_Color = Color.green;
    public Color sredni_dzwiek_Color = Color.yellow;
    public Color wysoki_dzwiek_Color = Color.red;

    private Image napelnienie_slidera_Image;
    private Image napelnienie_slidera_dzwieku_Image;

    [SerializeField] private float wartosc_dzwieku_Float;
    [SerializeField] private float wartosc_myzuki_Float;
    #endregion


    private void Awake()
    {

        #region Ustawienia muzyki
        dzwiek_Slider.value = 1;
        myzyka_Slider.value = 1;
        if (PlayerPrefs.HasKey("Volume"))
        {
            wartosc_dzwieku_Float = PlayerPrefs.GetFloat("Volume");
            dzwiek_Slider.value = wartosc_dzwieku_Float;
            grupa_efekow_AMG.audioMixer.SetFloat("Volume", Mathf.Log10(wartosc_dzwieku_Float) * 20);
            //ChangeColorByVolume1(soundVolume, volumeSoundSlider);
        }
        else
        {
            wartosc_dzwieku_Float = 1;
            dzwiek_Slider.value = wartosc_dzwieku_Float;
            grupa_efekow_AMG.audioMixer.SetFloat("Volume", Mathf.Log10(wartosc_dzwieku_Float) * 20);
        }
        if (PlayerPrefs.HasKey("Volume1"))
        {
            wartosc_myzuki_Float = PlayerPrefs.GetFloat("Volume1");
            myzyka_Slider.value = wartosc_myzuki_Float;
            grupa_efekow_AMG.audioMixer.SetFloat("Volume1", Mathf.Log10(wartosc_myzuki_Float) * 20);
            //ChangeColorByVolume1(musicVolume, volumeSoundSlider);
        }
        else
        {
            wartosc_myzuki_Float = 1;
            myzyka_Slider.value = wartosc_myzuki_Float;
            grupa_efekow_AMG.audioMixer.SetFloat("Volume1", Mathf.Log10(wartosc_myzuki_Float) * 20);
        }
        #endregion
    }


    void Start()
    {
        dzwiek_Slider.value = wartosc_dzwieku_Float;
        dzwiek_Slider.onValueChanged.AddListener(Set_wartosc_dzwieku);
        napelnienie_slidera_Image = dzwiek_Slider.fillRect.GetComponent<Image>();

        myzyka_Slider.value = wartosc_myzuki_Float;
        myzyka_Slider.onValueChanged.AddListener(Set_Wartosc_muzyki);
        napelnienie_slidera_dzwieku_Image = myzyka_Slider.fillRect.GetComponent<Image>();
        napelnienie_slidera_Image.color = wysoki_dzwiek_Color;
        napelnienie_slidera_dzwieku_Image.color = wysoki_dzwiek_Color;
        Set_wartosc_dzwieku(wartosc_dzwieku_Float);
            Set_Wartosc_muzyki(wartosc_myzuki_Float);
        ZmienKolorPrzezWartosc1(wartosc_myzuki_Float, myzyka_Slider);
        ZmienKolorPrzezWartosc(wartosc_dzwieku_Float, dzwiek_Slider);
    }
    
    private void Set_Wartosc_muzyki(float volume)
    {
        volume = Mathf.Clamp(volume, (float)0.001, 1);
        grupa_muzyki_AMG.audioMixer.SetFloat("Volume1", Mathf.Log10(volume) * 20);
        wartosc_myzuki_Float = volume;
        ZmienKolorPrzezWartosc1(volume, myzyka_Slider);
        PlayerPrefs.SetFloat("Volume1", wartosc_myzuki_Float);
        PlayerPrefs.Save();
    }
    private void Set_wartosc_dzwieku(float volume)
    {
        volume = Mathf.Clamp(volume, (float)0.001, 1);
        grupa_efekow_AMG.audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        wartosc_dzwieku_Float = volume;
        ZmienKolorPrzezWartosc(volume, dzwiek_Slider);
        PlayerPrefs.SetFloat("Volume", wartosc_dzwieku_Float);
        PlayerPrefs.Save();
    }

    void ZmienKolorPrzezWartosc(float volume1, Slider x)
    {
        float znormalizowana_wartosc_float = Mathf.InverseLerp(x.minValue, x.maxValue, volume1);
        Color nowy_Color = Color.Lerp(niski_dzwiek_Color, wysoki_dzwiek_Color, znormalizowana_wartosc_float);
        x.value = volume1;
        napelnienie_slidera_Image.color = nowy_Color;
    }
    void ZmienKolorPrzezWartosc1(float volume, Slider x)
    {
        float znormalizowana_wartosc_float = Mathf.InverseLerp(x.minValue, x.maxValue, volume);
        Color nowy_Color = Color.Lerp(niski_dzwiek_Color, wysoki_dzwiek_Color, znormalizowana_wartosc_float);
        x.value = volume;
        napelnienie_slidera_dzwieku_Image.color = nowy_Color;
    }

}