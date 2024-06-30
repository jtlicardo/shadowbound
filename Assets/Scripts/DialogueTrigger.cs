﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public Canvas dialogueCanvas;  // Reference to the Canvas
    public TextMeshProUGUI dialogueText;  // Reference to the Text component on the canvas
    public string textToDisplay = "Text to display";
    public bool triggerOnce = true;

    private AudioSource audioSource;
    private bool hasTriggered = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on " + gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        if (!triggerOnce || !hasTriggered)
        {
            StartCoroutine(PlayDialogueCoroutine());
            hasTriggered = true;
        }
    }

    private IEnumerator PlayDialogueCoroutine()
    {
        // Display the text
        dialogueText.text = textToDisplay;

        // Play the audio if available
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();

            // Wait for the audio to finish
            yield return new WaitForSeconds(audioSource.clip.length);
        }

        // Keep the text displayed for 1 second after the audio has finished
        yield return new WaitForSeconds(1f);

        // Clear the text
        dialogueText.text = "";
    }
}
