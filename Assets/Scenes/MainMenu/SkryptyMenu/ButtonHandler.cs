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
    void Start()
    {
        buttonImage = GetComponent<Image>();
        normalColor = buttonImage.color;
        if (normalColor == highlightColor)
        {
            normalColor = Color.green;
        }
        audioSource = GetComponent<AudioSource>();
        // Przypisz dŸwiêk do komponentu AudioSource
        
        
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
       // Debug.Log(gameObject.name + " "+ "res" + normalColor.ToString() +" " +  highlightColor.ToString());
        buttonImage.color = normalColor;
    }
}

