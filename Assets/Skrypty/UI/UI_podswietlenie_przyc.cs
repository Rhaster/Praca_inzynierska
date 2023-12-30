using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class UI_podswietlenie_przyc : MonoBehaviour ,IPointerEnterHandler, IPointerExitHandler
{
    private AudioSource zrodlo_AudioSource;

    private Color normalny_Color = Color.green;
    private Color podswietlenie_Color = Color.red;
    private Image przycisk_Image;
    void Awake()
    {
        przycisk_Image = GetComponent<Image>();
        normalny_Color = przycisk_Image.color;
        if (normalny_Color == podswietlenie_Color)
        {
            normalny_Color = Color.green;
        }
        zrodlo_AudioSource = GetComponent<AudioSource>();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        przycisk_Image.color = podswietlenie_Color;
        zrodlo_AudioSource.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        przycisk_Image.color = normalny_Color;
    }
    public void Reset()
    {
        przycisk_Image.color = normalny_Color;
    }
    private void OnDisable()
    {
        przycisk_Image.color = normalny_Color;
    }
}

