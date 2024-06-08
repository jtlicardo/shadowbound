using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorInteraction : MonoBehaviour
{
    public Text interactionText;
    public Animator fadeAnimator; // animator component of the fade panel
    public string nextSceneName;
    private bool playerInRange = false;
    public VideoPlayer videoPlayer;
    private bool videoStarted = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.X) && Time.timeScale != 0)
        {
            if (videoPlayer == null) {
                LoadNextScene();
            } else {
                // StartCoroutine(EnterBuilding());
                videoPlayer.loopPointReached += EndReached;
                videoPlayer.Play();
                videoStarted = true;
                interactionText.enabled = false;
                Time.timeScale = 0f;
            }
        }
        else if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.X)) {
            Time.timeScale = 1f;
            SkipVideo();
        }
    }

    void EndReached(VideoPlayer vp)
    {
        LoadNextScene();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    void SkipVideo()
    {
        videoPlayer.Stop();
        LoadNextScene();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !videoStarted)
        {
            interactionText.enabled = true;
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionText.enabled = false;
            playerInRange = false;
        }
    }

    private IEnumerator EnterBuilding()
    {
        fadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);

        // Wait until the scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.LoadScene(nextSceneName);
    }
}
