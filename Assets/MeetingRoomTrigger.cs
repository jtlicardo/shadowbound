using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetingRoomTrigger : MonoBehaviour
{
    public AudioClip triggerSound;
    private AudioSource audioSource;
    private bool hasTriggered = false;

    public SmallGate gate; // Reference to the SmallGate script


    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = triggerSound;
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            audioSource.Play();
            hasTriggered = true;

            StartCoroutine(WaitForAudioAndRiseGate());
        }
    }

    private IEnumerator WaitForAudioAndRiseGate()
    {
        // Wait for the audio to finish playing
        yield return new WaitForSeconds(audioSource.clip.length);

        // Trigger the gate to rise
        if (gate != null)
        {
            gate.RiseGate();
        }
        else
        {
            Debug.LogWarning("SmallGate component is not assigned in the MeetingRoomTrigger script!");
        }
    }
}
