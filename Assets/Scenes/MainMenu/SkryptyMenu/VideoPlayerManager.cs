using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.IO;



public class VideoPlayerManager : MonoBehaviour
{
    public VideoClip[] videoClips;
    private int currentClipIndex = 0;
    private VideoPlayer videoPlayer;
    [SerializeField]private float timer;
    [SerializeField] private float timermax;

    void Awake()
    {
        Time.timeScale = 1f; // anty prze�adowanie
        timer = timermax;
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.SetDirectAudioMute(0, true);

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
        if (timer <= 0f)
        {
            PlayNextVideo();
        }
    }

    void PlayNextVideo()
    {
        // Zatrzymaj odtwarzanie poprzedniego klipu
        videoPlayer.Stop();

        // Ustaw nowy klip i rozpocznij odtwarzanie
        videoPlayer.clip = videoClips[currentClipIndex];
        videoPlayer.Play();

        // Przejd� do nast�pnego klipu w li�cie
        currentClipIndex = (currentClipIndex + 1) % videoClips.Length;
        timer = timermax;

        // Zwolnij pami�� podr�czn�
        videoPlayer.GetComponent<VideoPlayer>().Prepare();
    }
}