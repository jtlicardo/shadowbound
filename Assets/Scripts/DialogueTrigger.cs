using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public Canvas dialogueCanvas;  // Reference to the Canvas
    public TextMeshProUGUI dialogueText;  // Reference to the Text component on the canvas
    public string textToDisplay = "Text to display";
    public bool triggerOnce = true;
    public float triggerDelay = 0f;  // Optional delay before the dialogue is triggered

    private AudioSource audioSource;
    private bool hasTriggered = false;

    // Static variables to keep track of the dialogue queue
    private static bool isDialoguePlaying = false;
    private static Queue<DialogueTrigger> dialogueQueue = new Queue<DialogueTrigger>();

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
            StartCoroutine(QueueDialogueCoroutine());
            hasTriggered = true;
        }
    }

    private IEnumerator QueueDialogueCoroutine()
    {
        dialogueQueue.Enqueue(this);

        while (isDialoguePlaying || dialogueQueue.Peek() != this)
        {
            yield return null;
        }

        yield return StartCoroutine(PlayDialogueCoroutine());

        dialogueQueue.Dequeue();

        if (dialogueQueue.Count > 0)
        {
            dialogueQueue.Peek().TriggerDialogue();
        }
    }

    private IEnumerator PlayDialogueCoroutine()
    {
        isDialoguePlaying = true;

        // Wait for the delay (if any)
        yield return new WaitForSeconds(triggerDelay);

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

        isDialoguePlaying = false;
    }
}
