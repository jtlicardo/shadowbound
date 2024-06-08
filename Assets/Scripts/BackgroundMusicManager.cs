using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    private bool wasPlayingBeforePause;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found!");
        }
        wasPlayingBeforePause = false;
    }

    void Update()
    {
        if (audioSource == null)
            return;

        if (Time.timeScale == 0 && audioSource.isPlaying)
        {
            wasPlayingBeforePause = true;
            audioSource.Pause();
        }
        else if (Time.timeScale == 1 && !audioSource.isPlaying && wasPlayingBeforePause)
        {
            audioSource.Play();
            wasPlayingBeforePause = false;
        }
    }
}
