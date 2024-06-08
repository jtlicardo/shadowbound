using UnityEngine;

public class AudioControlOnPause : MonoBehaviour
{
    private AudioSource audioSource;
    private bool wasPlayingBeforePause;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
                wasPlayingBeforePause = true;
            }
        }
        else if (Time.timeScale == 1)
        {
            if (wasPlayingBeforePause)
            {
                audioSource.UnPause();
                wasPlayingBeforePause = false;
            }
        }
    }
}
