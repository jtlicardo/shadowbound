using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class MeetingRoomTrigger : MonoBehaviour
{
    public AudioClip triggerSound;
    private AudioSource audioSource;
    private bool hasTriggered = false;

    public SmallGate gate; // Reference to the SmallGate script
    public BoxCollider invisibleBarrier; // Reference to the BoxCollider component
    public float barrierRemovalDelay = 20f; // Delay before removing the barrier
    public CameraFollow mainCamera; // Reference to the Camera component

    public Transform player; // Reference to the player GameObject


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
            invisibleBarrier.enabled = true;

            Transform newCameraTargetTarget = new GameObject().transform;
            newCameraTargetTarget.position = new Vector3(35f, 51f, -20f);
            newCameraTargetTarget.rotation = Quaternion.Euler(0, 28, 0);

            mainCamera.target = newCameraTargetTarget;

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

        // Wait for the specified delay before removing the barrier
        yield return new WaitForSeconds(barrierRemovalDelay);

        // Remove the invisible barrier
        if (invisibleBarrier != null)
        {
            invisibleBarrier.enabled = false;
            // Set the camera target back to the player
            mainCamera.target = player;
            mainCamera.CalculatePosition();
        }
        else
        {
            Debug.LogWarning("Invisible barrier is not assigned in the MeetingRoomTrigger script!");
        }
    }
}
