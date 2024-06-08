﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SkipCutscene : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Subscribe to the loopPointReached event which is triggered when the video finishes playing
        videoPlayer.loopPointReached += EndReached;
    }

    void Update()
    {
        // Check if the Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SkipScene();
        }
    }

    void SkipScene()
    {
        // Stop the video if it's still playing
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }

        LoadNextScene();
    }

    void EndReached(VideoPlayer vp)
    {
        LoadNextScene();
    }

    void LoadNextScene()
    {
        // Load the next scene; ensure you have added scenes in the Build settings
        // File -> Build Settings -> Add Open Scenes
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
