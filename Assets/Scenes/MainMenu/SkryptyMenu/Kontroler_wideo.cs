using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.IO;



public class Kontroler_wideo : MonoBehaviour
{
    public VideoClip[] clipy_VideoClip;
    private int obecny_index_Int = 0;
    private VideoPlayer odtwazacz_VideoPlayer;


    void Awake()
    {
        Time.timeScale = 1f; // anty prze³adowanie
        odtwazacz_VideoPlayer = GetComponent<VideoPlayer>();
        odtwazacz_VideoPlayer.SetDirectAudioMute(0, true);

        if (clipy_VideoClip.Length > 0)
        {
            PlayNextVideo();
        }
        else
        {
            Debug.LogError("Lista Video Clip jest pusta. Dodaj co najmniej jeden Video Clip.");
        }

    }



    void PlayNextVideo()
    {


        odtwazacz_VideoPlayer.clip = clipy_VideoClip[obecny_index_Int];
        odtwazacz_VideoPlayer.Play();
        odtwazacz_VideoPlayer.GetComponent<VideoPlayer>().Prepare();
    }
}