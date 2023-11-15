using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.IO;

public class VideoPlayerManager : MonoBehaviour
{

    public VideoClip[] videoClips;  // Lista obiektów Video Clip
    private int currentClipIndex = 0;
    private VideoPlayer videoPlayer;
    private float timer;
    [SerializeField]private float timermax;
    void Start()
    {
        timer = timermax;
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.SetDirectAudioMute(0, true); // mute dzwieku 
        // SprawdŸ, czy istniej¹ jakiekolwiek Video Clip w liœcie
        if (videoClips.Length > 0)
        {
            PlayNextVideo();
        }
        else
        {
            Debug.LogError("Lista Video Clip jest pusta. Dodaj co najmniej jeden Video Clip.");
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        // SprawdŸ, czy odtwarzacz wideo zakoñczy³ odtwarzanie aktualnego klipu
        if(timer<=0f)
        {
            PlayNextVideo();
        }
    }

    void PlayNextVideo()
    {
        videoPlayer.clip = videoClips[currentClipIndex];
        videoPlayer.Play();
        
        // PrzejdŸ do nastêpnego klipu w liœcie
        currentClipIndex = (currentClipIndex + 1) % videoClips.Length;
        timer= timermax;
    }
}