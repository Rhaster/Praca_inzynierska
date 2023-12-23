using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ButtonHandler : MonoBehaviour ,IPointerEnterHandler, IPointerExitHandler
{
    private AudioSource audioSource;

    private Color normalColor = Color.green;
    private Color highlightColor = Color.red;
    private Image buttonImage;
    void Awake()
    {
        buttonImage = GetComponent<Image>();
        normalColor = buttonImage.color;
        if (normalColor == highlightColor)
        {
            normalColor = Color.green;
        }
        audioSource = GetComponent<AudioSource>();
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.color = highlightColor;
        audioSource.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.color = normalColor;
    }
    public void Reset()
    {
        buttonImage.color = normalColor;
    }
    private void OnDisable()
    {
        buttonImage.color = normalColor;
    }
}

