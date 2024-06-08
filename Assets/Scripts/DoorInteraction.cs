﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorInteraction : MonoBehaviour
{
    public Text interactionText;
    public Animator fadeAnimator; // animator component of the fade panel
    public string nextSceneName;
    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(EnterBuilding());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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