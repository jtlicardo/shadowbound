using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogueEntry
{
    public string text;
    public AudioClip audioClip;
}

public class DialogueTrigger : MonoBehaviour
{
    public Canvas dialogueCanvas;
    public TextMeshProUGUI dialogueText; // Text component on the canvas
    public List<DialogueEntry> dialogueEntries = new List<DialogueEntry>();
    public bool triggerOnce = true;
    public float triggerDelay = 0f;
    public float textDisplayDuration = 1f; // How much to display text after audio clip ends

    private AudioSource audioSource;
    private bool hasTriggered = false;

    private static bool isDialoguePlaying = false;
    private static Queue<DialogueTrigger> dialogueQueue = new Queue<DialogueTrigger>();

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
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
        yield return StartCoroutine(PlayDialogueSequenceCoroutine());
        dialogueQueue.Dequeue();
        if (dialogueQueue.Count > 0)
        {
            dialogueQueue.Peek().TriggerDialogue();
        }
    }

    private IEnumerator PlayDialogueSequenceCoroutine()
    {
        isDialoguePlaying = true;

        yield return new WaitForSeconds(triggerDelay);

        foreach (DialogueEntry entry in dialogueEntries)
        {
            dialogueText.text = entry.text;

            if (entry.audioClip != null)
            {
                audioSource.clip = entry.audioClip;
                audioSource.Play();
                yield return new WaitForSeconds(entry.audioClip.length);
            }

            yield return new WaitForSeconds(textDisplayDuration);
        }

        dialogueText.text = "";
        isDialoguePlaying = false;
    }
}
